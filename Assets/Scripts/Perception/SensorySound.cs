using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorySound : MonoBehaviour
{
    /// <summary>
    /// 当前声音的发出对象
    /// </summary>
    [HideInInspector]
    public GameObject soundMaker;

    public LayerMask SoundLayer;

    /// <summary>
    /// 声音的持续时间
    /// </summary>
    public float SoundStayTime = 0.3f;

    /// <summary>
    /// 当前持续时间
    /// </summary>
    private float currentStayTime = 0f;

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <param name="maker">发出声音的对象</param>
    public void SoundInit(GameObject maker)
    {
        soundMaker = maker;
        currentStayTime = 0f;
    }

    private void Update()
    {
        currentStayTime += Time.deltaTime;
        CircleCollider2D cld = this.GetComponent<CircleCollider2D>();   //获得范围
        //过滤器
        ContactFilter2D filter = new ContactFilter2D
        {
            layerMask = SoundLayer,
            useTriggers = true,
            useLayerMask = true
        };
        //用于接收回传
        List<Collider2D> hs = new List<Collider2D>();

        cld.OverlapCollider(filter, hs);        //获取收听者

        foreach (Collider2D c in hs)
        {
            SensoryHearing h = c.gameObject.GetComponent<SensoryHearing>();
            if (h)
            {
                h.Hearing(soundMaker);
            }
        }


        if (currentStayTime >= SoundStayTime)
        {
            SensorySoundSystem.Instance.Recycle(this.gameObject);
            Debug.Log("执行回收");
        }
            
    }
}
