using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 感知记忆
/// </summary>
public class SensoryMemory
{
    /// <summary>
    /// 记忆记录
    /// </summary>
    public class MemoryRecord
    {
        
        /// <summary>
        /// 被感知到的目标
        /// </summary>
        public GameObject target;

        /// <summary>
        /// 最后感知记录的位置
        /// </summary>
        public Vector2 lastPos;

        /// <summary>
        /// 上一次感知到的时间
        /// </summary>
        public float lastTime;

        /// <summary>
        /// 进入感觉的时间
        /// </summary>
        public float firstTime;
    }

    /// <summary>
    /// 视觉记录列表
    /// </summary>
    private List<MemoryRecord> viewRecords;

    /// <summary>
    /// 听觉记录列表
    /// </summary>
    private List<MemoryRecord> hearRecords;

    /// <summary>
    /// 记录能保持的时间
    /// </summary>
    public float RecordStayTime;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stayTime">记忆能持续的时间</param>
    public SensoryMemory(float stayTime)
    {
        RecordStayTime = stayTime;
        viewRecords = new List<MemoryRecord>();
        hearRecords = new List<MemoryRecord>();
    }


    /// <summary>
    /// 每帧都要更新数据
    /// </summary>
    public void Update()
    {
        //删除所有超出时间的记录
        viewRecords.RemoveAll(x => Time.time - x.lastTime > RecordStayTime);
        hearRecords.RemoveAll(x => Time.time - x.lastTime > RecordStayTime);
    }

    /// <summary>
    /// 上传一个听觉的数据
    /// </summary>
    public void UpdateViewRecord(GameObject target)
    {
        MemoryRecord r;
        if ((r = viewRecords.Find(x => x.target == target)) == null)    //没找到旧的记录
        {
            r = new MemoryRecord
            {
                firstTime = Time.time,
                lastTime = Time.time,
                lastPos = target.transform.position,
                target = target
            };
            viewRecords.Add(r);
        }
        //找到旧记录
        else
        {
            r.lastPos = target.transform.position;
            r.lastTime = Time.time;
        }
    }

    /// <summary>
    /// 上传一个听觉的数据
    /// </summary>
    public void UpdateHearRecord(GameObject target)
    {
        MemoryRecord r;
        if ((r = hearRecords.Find(x => x.target == target)) == null)    //没找到旧的记录
        {
            r = new MemoryRecord
            {
                firstTime = Time.time,
                lastTime = Time.time,
                lastPos = target.transform.position,
                target = target
            };
            hearRecords.Add(r);
        }
        //找到旧记录
        else
        {
            //更新记录的数据
            r.lastPos = target.transform.position;
            r.lastTime = Time.time;
        }
    }


}
