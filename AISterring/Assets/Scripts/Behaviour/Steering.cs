using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour {

    public GameObject leader;
    public float maxSpeed;
    public float maxForce;
    public float approachRadius;


    float dist;
    Vector3 velocity;

    Vector3 ahead;
    Vector3 ahead2;
    public Vector3 maxSeeAhead;
    public Vector3 maxAvoidForce;
    public LayerMask obstacles;

    private void Awake()
    {
        //For Seek
        velocity = GetComponent<Rigidbody>().velocity;
 
    }

    private void Update()
    {
        transform.position += Seek(leader);
        avoidObstacle(leader);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "wall")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    Vector3 Seek(GameObject _leader)
    {
        Vector3 desiredPos = _leader.transform.position - transform.position;
        float dist = desiredPos.magnitude;
        desiredPos = desiredPos.normalized;
        if(dist < approachRadius)
        {
            desiredPos *= dist / approachRadius * maxSpeed;
        } else
        {
            desiredPos *= maxSpeed;
        }
        Vector3 steer = desiredPos - velocity;
        if(steer.magnitude > maxForce)
        {
            steer.Scale(new Vector3(maxForce, maxForce, maxForce));
        }
        return steer * Time.deltaTime;
    }

    void avoidObstacle(GameObject _leader)
    {
        Vector3 target = _leader.transform.position - transform.position;

        target = target.normalized;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 5f, obstacles))
        {
            if(hit.transform != transform)
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                target += hit.normal * 400f;
            }
        }

        Vector3 leftR = transform.position;
        Vector3 rightR = transform.position;

        leftR.x -= 2;
        rightR.x += 2;

        if(Physics.Raycast(leftR, transform.forward, out hit, 5f, obstacles))
        {
            if(hit.transform != transform)
            {
                Debug.DrawLine(leftR, hit.point, Color.red);
                target += hit.normal * 400f;
            }
        }

        if(Physics.Raycast(rightR, transform.forward, out hit, 5f, obstacles))
        {
            if(hit.transform != transform)
            {
                Debug.DrawLine(rightR, hit.point, Color.red);
                target += hit.normal * 400f;
            }           
        }

        Quaternion torotation = Quaternion.LookRotation(target);

        transform.rotation = Quaternion.Slerp(transform.rotation, torotation, Time.deltaTime * 10f);

        transform.position += transform.forward * 1f * Time.deltaTime;
    }    
}
