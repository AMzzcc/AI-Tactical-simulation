using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensory : MonoBehaviour
{
    /// <summary>
    /// 记忆实例
    /// </summary>
    public SensoryMemory memory;

    /// <summary>
    /// 每段记忆保持的时间
    /// </summary>
    public float MemoryStayTime = 20f;

    public LayerMask ViewLayer;

    /// <summary>
    /// 视野刷新间隔
    /// </summary>
    public float ViewRefreshTime = 0.2f;

    /// <summary>
    /// 当前视野刷新时间
    /// </summary>
    [SerializeField]
    private float currentViewRefreshTime = 0;

    /// <summary>
    /// 视野角度范围，以x轴为对称轴的扇形
    /// </summary>
    public float ViewAngleRange = 90f;

    /// <summary>
    /// 视野范围，是一个trigger
    /// </summary>
    public CircleCollider2D ViewRange;

    /// <summary>
    /// 听觉脚本，用于接受听觉
    /// </summary>
    public SensoryHearing HearingRange;

    /// <summary>
    /// 检测角度，检测次数 = 范围 / 检测角度
    /// </summary>
    public float ViewCheckDegrees = 4f;

    /// <summary>
    /// 测试的百分比
    /// </summary>
    [Range(0.01f, 1.0f)]
    public float ViewCheckOffset = 0.96f;


    private void Start()
    {
        memory = new SensoryMemory(MemoryStayTime);     //为记忆创建实例
        HearingRange.sensoryObject = this;
    }



    /// <summary>
    /// 视野和听觉的Update
    /// </summary>
    private void ViewAndHearUpdate()
    {

        //按帧积累时间，时间到就执行一次
        if (currentViewRefreshTime >= ViewRefreshTime)
        {
            View();
            currentViewRefreshTime = 0f;
        }
        currentViewRefreshTime += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Vector2 thisPos = this.transform.position;      //自己的位置
        Vector2 dir = this.transform.up;             //自己的方向
        #region 绘制范围debug
        Quaternion r = new Quaternion();
        r.eulerAngles = new Vector3(0, 0, ViewAngleRange / 2f);
        Debug.DrawLine(thisPos, thisPos + (Vector2)(r * dir * ViewRange.radius), Color.yellow);
        r.eulerAngles = new Vector3(0, 0, -ViewAngleRange / 2f);
        Debug.DrawLine(thisPos, thisPos + (Vector2)(r * dir * ViewRange.radius), Color.yellow);
        
        #endregion 绘制范围debug
    }

    private void Update()
    {
        ViewAndHearUpdate();
        memory.Update();
    }

    private void View()
    {
        //获得在ViewRange的物体
        List<Collider2D> targets = new List<Collider2D>();    
        ContactFilter2D vf = new ContactFilter2D().NoFilter();      //过滤器
        vf.SetLayerMask(ViewLayer);
        vf.useLayerMask = true;
        vf.useTriggers = true;
        //Debug.Log(LayerMask.LayerToName(vm));
        ViewRange.OverlapCollider(vf, targets);

        //Debug.Log(ViewRange.OverlapCollider(vf, targets));

        //遍历
        foreach(Collider2D c in targets)
        {
            if (c == null)
                continue;
            SensoryVisual sv;
            //检测到SensoryVisual脚本的才是可视对象
            if ((sv = c.gameObject.GetComponent<SensoryVisual>()) == null)
            {
                //Debug.Log(c.gameObject.name + "不可视");
                continue;
            }
                
            Vector2 thisPos = this.transform.position;      //自己的位置
            Vector2 targetPos = c.transform.position;       //检测目标的位置
            Vector2 dir = this.transform.up;             //自己的方向
            Vector2 targetDir = targetPos - thisPos;        //检测目标的相对位置
            ////坐标cos公式
            //float cos = Vector2.Dot(targetDir, dir) / targetDir.magnitude / dir.magnitude;
            float angle = Vector3.Angle(dir, targetDir) / 180 * Mathf.PI;                   //相对角度角度制

            //Debug.Log(angle);

            float viewAngleRangeArc = ViewAngleRange / 180f * Mathf.PI;                     //探测范围弧度制




            //超出角度判定
            if (angle > viewAngleRangeArc / 2f || angle < -viewAngleRangeArc / 2f)
            {
                //Debug.Log(c.gameObject.name + "超出角度判定");
                continue;
            }
                
            
            float checkRange = Mathf.Asin(sv.VisualRange.radius / targetDir.magnitude) * 2;                         //需要检测的角度范围
            int checkTimes = (int)(checkRange / (ViewCheckDegrees / 180 * Mathf.PI)) + 1;                           //计算检测次数
            float checkRange_r = ViewCheckOffset * checkRange;                                                      //方便边缘检测
            float anglePerCheck = checkRange_r / (checkTimes + 1);                                                  //每次增加的角度    
            float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x);                                              //目标方向的绝对角
            float startAngle = targetAngle - checkRange / 2f + checkRange * (1 - ViewCheckOffset) / 2f;           //起始角度
            float currentAngle = startAngle;                                                //当前检测的角度
            
            //检测
            for (int i = 0; i < checkTimes + 2; ++i)
            {
                
                //射线超出角度剔除
                if (currentAngle - targetAngle > viewAngleRangeArc / 2f || angle < -viewAngleRangeArc / 2f)
                    continue;
                //计算射线的方向向量
                Vector3 v0 = new Vector3(1f, 0f, 0f);
                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(0, 0, currentAngle / Mathf.PI * 180);
                Vector2 v1 = rot * v0;          //方向向量
                currentAngle += anglePerCheck;  //递增角度

                //进行射线判断
                RaycastHit2D[] hits = new RaycastHit2D[10];
                int length = ViewRange.Raycast(v1, vf, hits, ViewRange.radius);
                //什么都没碰到
                if (length <= 0)
                {
                    Debug.DrawLine(thisPos, thisPos + v1 * ViewRange.radius, Color.red);
                    continue;
                }

                //第一个命中
                if (hits[0].collider.gameObject.GetComponent<SensoryVisual>() == sv)
                {
                    memory.UpdateViewRecord(hits[0].collider.gameObject);
                    Debug.DrawLine(thisPos, hits[0].point, Color.green);
                }
                //没命中
                else
                {
                    Debug.DrawLine(thisPos, hits[0].point, Color.red);
                }
            }
        }


    }


    /// <summary>
    /// 听觉接收
    /// </summary>
    public void Hearing(GameObject target)
    {
        memory.UpdateHearRecord(target);
        Debug.Log(this.gameObject.name + "收到" + target.name + "的声音");
    }
}
