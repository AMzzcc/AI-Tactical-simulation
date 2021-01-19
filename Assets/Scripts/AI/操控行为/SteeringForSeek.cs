using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeek : Steering
{
    public GameObject target;
    private Vector3 desiredVelocity;//预期速度
    private Vehicle m_vehicle;
    private float maxSpeed;
    private bool isPlanar;

    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPlanar = m_vehicle.isPlanar;
    }

    public override Vector3 Force() //计算操控向量=预期速度-当前速度
    {
        desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
        if(isPlanar)
        {
            desiredVelocity.z = 0;
        }
        //Debug.Log("seek");
        return (desiredVelocity-m_vehicle.velocity);
    }

}
