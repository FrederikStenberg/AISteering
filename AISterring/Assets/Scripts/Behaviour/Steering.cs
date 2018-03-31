using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour {
<<<<<<< HEAD
        
    public float seperationRadius = 1.5f;
    public float seperationForce = -1f;
    private Vector3 velocity, sepVel, seekVel, cohVel;

    public float seperationWeight = 3f;
    public float seekWeight = 1f;
    public float cohesionWeight = 0.4f;

    public float maxSpeed = 5f;
    public float maxForce = 2f;
    public float approachRadius = 10f;

    GameObject[] agentArray;
    GameObject leader;

    private void Awake()
    {
        agentArray = GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array;
        leader = GameObject.FindGameObjectWithTag("Leader");
=======

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
 
>>>>>>> 38ff3b6a50c5693d5b4959e09f2fdb8443d27c3e
    }

    private void Update()
    {
<<<<<<< HEAD
        //Avoid movement in Z direction (Y in 3D space)
        this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        seekVel += Seek(leader) * seekWeight;
        if (seekVel.magnitude >= maxSpeed)
        {
            velocity = Vector3.ClampMagnitude(seekVel, maxSpeed);
        }

        sepVel += Seperation(agentArray) * seperationWeight;
        cohVel += Cohesion(agentArray) * cohesionWeight;

        velocity = seekVel + sepVel + cohVel;
        velocity /= (seekWeight + seperationWeight + cohesionWeight);

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        transform.position += velocity * Time.deltaTime;
=======
        transform.position += Seek(leader);
        avoidObstacle(leader);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "wall")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
>>>>>>> 38ff3b6a50c5693d5b4959e09f2fdb8443d27c3e
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

<<<<<<< HEAD
    private Vector3 Seperation(GameObject[] agentArray)
    {
        int neighbourCount = 0;
        Vector3 steer = Vector3.zero;
        foreach (GameObject agent in agentArray)
=======
    void avoidObstacle(GameObject _leader)
    {
        Vector3 target = _leader.transform.position - transform.position;

        target = target.normalized;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 5f, obstacles))
>>>>>>> 38ff3b6a50c5693d5b4959e09f2fdb8443d27c3e
        {
            if(hit.transform != transform)
            {
<<<<<<< HEAD
                float distance = Vector3.Distance(this.gameObject.transform.position, agent.transform.position);
                if (distance < seperationRadius && distance > 0)
                {
                    Vector3 pushForce = transform.position - agent.transform.position;
                    steer += pushForce;
                    neighbourCount++;
                }
            }
        }

        if (neighbourCount > 0)
        {
            steer /= neighbourCount;
            steer = steer.normalized;
        }
        return steer;
    }

    private Vector3 Cohesion(GameObject[] agentArray)
    {
        Vector3 center = Vector3.zero;
        Vector3 steer = Vector3.zero;

        int neighbourCount = 0;
        foreach (GameObject agent in agentArray)
=======
                Debug.DrawLine(transform.position, hit.point, Color.red);
                target += hit.normal * 400f;
            }
        }

        Vector3 leftR = transform.position;
        Vector3 rightR = transform.position;

        leftR.x -= 2;
        rightR.x += 2;

        if(Physics.Raycast(leftR, transform.forward, out hit, 5f, obstacles))
>>>>>>> 38ff3b6a50c5693d5b4959e09f2fdb8443d27c3e
        {
            if(hit.transform != transform)
            {
<<<<<<< HEAD
                center += agent.transform.position;
                neighbourCount++;
            }
        }

        if (neighbourCount == 0)
        {
            Debug.Log("No neighbours for Cohesion found!");
            return Vector3.zero;
        }

        center += transform.position;
        center /= neighbourCount + 1;


        steer = center - transform.position;

        Debug.DrawLine(transform.position, transform.position + steer, Color.magenta);
        return steer;
    }
=======
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
>>>>>>> 38ff3b6a50c5693d5b4959e09f2fdb8443d27c3e
}
