using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRange : MonoBehaviour
{
    //玩家的火力范围
    public float fireRange;
    //在火力范围内的路点需要增加的代价值
    public int penalty;
    //火力攻击的控制角度
    public float fieldOfAttack;
    //显示出火力线
    private void OnDrawGizmos()
    {
        Vector3 forward = new Vector3(0, 0, transform.rotation.z);
        Vector3 frontRayPoint = transform.position + (forward * fireRange);
        float fieldOfAttackinRadians = fieldOfAttack * 3.14f / 180.0f;
        for(int i=0;i<11;i++)
        {
            RaycastHit hit;
            float angle = -fieldOfAttackinRadians + fieldOfAttackinRadians * 0.2f * (float)i;
            Vector3 rayPoint = transform.TransformPoint(new Vector3(fireRange * Mathf.Sin(angle), 0, fireRange * Mathf.Cos(angle)));
            Vector3 rayDirection = rayPoint - transform.position;
            //当遇到障碍物时，终止火力线
            if (Physics.Raycast(transform.position, rayDirection, out hit, fireRange))
            {
                if(hit.transform.gameObject.layer==9)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    continue;
                }
            }
            Debug.DrawLine(transform.position, rayPoint, Color.red);
        }
    }
    
}
