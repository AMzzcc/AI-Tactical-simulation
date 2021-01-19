using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensoryHearing : MonoBehaviour
{
    //感觉的主体
    [HideInInspector]
    public Sensory sensoryObject;

    /// <summary>
    /// 用于接受信息
    /// </summary>
    /// <param name="target">发出对象</param>
    public void Hearing(GameObject target)
    {
        sensoryObject.Hearing(target);
    }

}
