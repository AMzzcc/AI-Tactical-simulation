using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AstarAI : MonoBehaviour
{
    static private int targetsMaxNumber = 10;

    public Transform[] targets = new Transform[targetsMaxNumber];  //多目标Transform数组
    private Path[] lastPaths;                       //所有路径数组
    private int numCompleted = 0;                   //目前计算到的路径
    private Path bestPath = null;                   //最好的路径
    [SerializeField] private float[] pathLength=new float[targetsMaxNumber+1];//所有路径长度

    public float Speed = 3f;
    //当角色与一个航点小于该值，则可以转向前往路径上的下一个航点
    public float nextWayPointDistance = 3f;
    //正在朝其前进的航点
    private int currentWayPoint = 0;
    //private bool reachedEndOfPath=false;
    //当前路径
    public Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        //持续更新路径扫描生成
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    void UpdatePath()
    {
        //如果当前正在计算任何路径，则取消它们以避免浪费处理能力
        if (lastPaths != null)
            for (int i = 0; i < lastPaths.Length; i++)
                lastPaths[i].Error();

        //如果需要，创建一个新的lastPaths数组
        if (lastPaths == null || lastPaths.Length != targets.Length) lastPaths = new Path[targets.Length];

        bestPath = null;
        numCompleted = 0;

        //遍历目标点并生成到其路径数组
        for (int i = 0; i < targets.Length; i++)
        {
            //创建一条新的路径到当前目标点
            ABPath p = ABPath.Construct(transform.position, targets[i].position, OnTestPathComplete);

            lastPaths[i] = p;

            //请求要计算的路径，可能需要几帧
            //这将在完成时调用OnTestPathComplete
            AstarPath.StartPath(p);
        }
        //if (seeker.IsDone())
        //  seeker.StartPath(rb.position, Target.position,OnPathComplete);
        
    }

    // 负责移动及转向至path
    //void FixedUpdate()
    //{
    //    if (path == null)
    //    {
    //        return;
    //    }
    //    //当前路径序号是否大于该路径的总航点数
    //    if (currentWayPoint >= path.vectorPath.Count)
    //    {
    //        //reachedEndOfPath = true;
    //        return;
    //    }
    //    else
    //    {
    //        //reachedEndOfPath = false;
    //    }

    //    Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
    //    Vector2 force = direction * Speed * Time.fixedDeltaTime;
    //    rb.AddForce(force);

    //    //转向
    //    float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
    //    transform.Rotate(new Vector3(0, 0, Mathf.Sign(angle) * Speed * Time.fixedDeltaTime));

    //    float distance = Vector2.Distance(rb.position, (Vector2)path.vectorPath[currentWayPoint]);
    //    //如果当前位置与下一个航点的距离小于需要切换下一个航点的距离，则切换
    //    if (distance < nextWayPointDistance)
    //    {
    //        currentWayPoint++;
    //        return;
    //    }
    //}


    public void OnTestPathComplete(Path p)
    {
        if (p.error)
        {
            Debug.LogWarning("One target could not be reached!\n" + p.errorLog);
        }

        //Make sure this path is not an old one
        for (int i = 0; i < lastPaths.Length; i++)
        {
            if (lastPaths[i] == p)
            {
                numCompleted++;

                if (numCompleted >= lastPaths.Length)
                {
                    CompleteSearch();
                }
                return;
            }
        }
    }
    public void OnPathComplete(Path p)//等寻路结束后回调该函数
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;//重置路径点为下一条新路径的起点
        }
    }

    public void CompleteSearch()
    {
        //Find the shortest path
        //Path shortest = null;
        //float shortestLength = float.PositiveInfinity;

        //Loop through the paths
        for (int i = 0; i < lastPaths.Length; i++)
        {
            //获取路径的总长度，如果路径有错误将返回无穷大
            //float length = lastPaths[i].GetTotalLength();

            //记录所有路径的长度
            pathLength[i] = lastPaths[i].GetTotalLength();

            //计算最短路径
            //if (shortest == null || length < shortestLength)
            //{
            //    shortest = lastPaths[i];
            //    shortestLength = length;
            //}
        }

        //Debug.Log("Found a path which was " + shortestLength + " long");
        //bestPath = shortest;
    }

    public void Update()
    {
        //如果有最短路径将其画出
        if (bestPath != null && bestPath.vectorPath != null)
        {
            for (int i = 0; i < bestPath.vectorPath.Count - 1; i++)
            {
                Debug.DrawLine(bestPath.vectorPath[i], bestPath.vectorPath[i + 1], Color.green);
            }

            /* Before version 3.2
             * for (int i=0;i<bestPath.vectorPath.Length-1;i++) {
             */
        }
    }

}
