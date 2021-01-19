using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle
{

    private Rigidbody2D theRigidbody2D;
    private Vector3 moveDistance;

    protected override void Start()
    {
        theRigidbody2D = GetComponent<Rigidbody2D>();
        moveDistance = new Vector3(0, 0, 0);
        base.Start();
    }

    void FixedUpdate()
    {
        //if (steeringForce == moveDistance)
        //    Debug.Log("f=0");
        //if (acceleration == moveDistance)
        //    Debug.Log("a=0");
        velocity += acceleration * Time.fixedDeltaTime;
        if(velocity.sqrMagnitude>sqrMaxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        moveDistance = velocity * Time.fixedDeltaTime;
        if(isPlanar)
        {
            velocity.z = 0;
            moveDistance.z = 0;
        }
        if(theRigidbody2D==null||theRigidbody2D.isKinematic)
        {
            transform.position += moveDistance;
        }
        else
        {

            theRigidbody2D.MovePosition(theRigidbody2D.position + new Vector2(moveDistance.x,moveDistance.y));
        }
        //if (velocity.sqrMagnitude > 0.1)//计算新朝向
        //{
        //    Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
        //    if (isPlanar)
        //    {
        //        newForward.z = 0;
        //    }
        //    transform.forward = newForward;
        //}

        //播放行走动画
        //gameObject.animation.Play("walk");
    }
    
}
