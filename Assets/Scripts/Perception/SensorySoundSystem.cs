using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorySoundSystem : MonoBehaviour
{
    #region 单例模式
    private static SensorySoundSystem instance;
    public static SensorySoundSystem Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion 单例模式

    /// <summary>
    /// 声音触发器的预制体
    /// </summary>
    public GameObject SoundTriggerObj;

    /// <summary>
    /// 对象池
    /// </summary>
    private List<GameObject> soundPool;

    private void Start()
    {
        soundPool = new List<GameObject>();     //初始化对象池
    }

    /// <summary>
    /// 回收机制
    /// </summary>
    /// <param name="trigger">回收</param>
    public void Recycle(GameObject trigger)
    {
        trigger.SetActive(false);
        soundPool.Add(trigger);
    }

    /// <summary>
    /// 发出声音调用
    /// </summary>
    /// <param name="maker">发出声音者</param>
    /// <param name="radiu">接受半径</param>
    public void MakeSound(GameObject maker, float radiu)
    {
        GameObject sound;
        //如果池子非空，就从池子里面取出
        if (soundPool.Count > 0)
        {
            sound = soundPool[0];
            soundPool.RemoveAt(0);
        }
        //如果池子空，就创建一个新的实例
        else
        {
            sound = Instantiate(SoundTriggerObj);
            sound.SetActive(false);
        }
        SensorySound ss = sound.GetComponent<SensorySound>();
        if (!ss)
        {
            Debug.LogError("生成声音对象错误");
            return;
        }
        //初始化声音存储的目标
        ss.SoundInit(maker);
        CircleCollider2D cld = sound.GetComponent<CircleCollider2D>();  //获得trigger以修改半径
        if (!cld)
        {
            Debug.LogError("生成声音对象缺少trigger");
            return;
        }
        cld.radius = radiu;
        sound.transform.position = maker.transform.position;
        sound.SetActive(true);
    }

}
