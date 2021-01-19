using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    
    [SerializeField]private Steering[] steerings;   //该AI角色包含的操控行为列表
    public float maxSpeed = 10;
    public float maxForce = 100;
    protected float sqrMaxSpeed;

    public float mass = 1;
    public Vector3 velocity;
    public float damping = 0.9f;   //控制转向的速度
    public float computeInterval = 0.01f;   //操控力计算间隔时间
    public bool isPlanar = true;

    public Vector3 steeringForce;
    public Vector3 acceleration;
    private float timer;
    protected virtual void Start()
    {
        steeringForce = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, 0);
        sqrMaxSpeed = maxSpeed * maxSpeed;
        timer = 0;
        steerings = GetComponents<Steering>();
    }

    // Update is called once per frame
    protected void Update()
    {
        timer += Time.deltaTime;
        steeringForce = new Vector3(0, 0, 0);
        if(timer>computeInterval)
        {
            foreach(Steering s in steerings)
            {
                if(s.enabled)
                {
                    steeringForce += s.Force() * s.weight;
                }
            }
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
            acceleration = steeringForce / mass;
            timer = 0;
        }
       
    }
}
