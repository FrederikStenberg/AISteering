﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour {
    
    private float dist;

    public float alignmentDistance = 100;
    public float seperationRadius = 50;
    public float seperationForce = -1f;

    public Vector3 velocity, sepVel;
    public Vector3 acceleration;

    GameObject[] agentArray;

    GameObject leader;
    public float maxSpeed, seperationSpeed = 1;
    public float maxForce;
    public float approachRadius;

    public List<GameObject> entityList;
    public List<GameObject> neighbourList;

    private void Awake()
    {
        velocity = GetComponent<Rigidbody>().velocity;
        agentArray = GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array;
    }

    private void Start()
    {
        leader = GameObject.FindGameObjectWithTag("Leader");
        entityList = new List<GameObject>();
        neighbourList = new List<GameObject>();

        GameObject[] entities = GameObject.FindGameObjectsWithTag("Follower");
        for (int i = 0; i < entities.Length; i++)
        {
            entityList.Add(entities[i]);
        }
        entityList.Add(leader);
        Debug.Log(gameObject.name + ": I have " + entityList.Count + " friends, and we are following " + leader.name);
    }

    private void Update()
    {
        //transform.position += Seek(leader);
        //velocity += Cohesion() + Seperation();
        //velocity = GetComponent<Rigidbody>().velocity.normalized;

        //acceleration = CalculateSteering();

        //velocity += acceleration * Time.deltaTime;

        //if (velocity.magnitude > maxSpeed)
        //{
        //    velocity = velocity.normalized * maxSpeed;
        //}

        //transform.position += velocity * Time.deltaTime;

        //Debug.DrawLine(transform.position, transform.position + acceleration, Color.red);
        //Debug.DrawLine(transform.position, transform.position + velocity, Color.green);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        
        velocity = (Seperation(agentArray) * seperationSpeed) + Seek(leader);
        this.transform.position += velocity * Time.deltaTime;
    }

    Vector3 CalculateSteering()
    {
        Vector3 steer = Vector3.zero;
        steer += Seek(leader);
        steer += Seperation(agentArray);
        steer += Cohesion();

        steer /= 1; // If we want to add weights to above methods
        steer.y = 0;

        return steer;
    }

    Vector3 Seek(GameObject _leader)
    {
        Vector3 desiredPos = _leader.transform.position - transform.position;
        float dist = desiredPos.magnitude;
        desiredPos = desiredPos.normalized;
        if (dist < approachRadius)
        {
            desiredPos *= dist / approachRadius * maxSpeed;
        }
        else
        {
            desiredPos *= maxSpeed;
        }

        Vector3 steer = desiredPos - velocity;
        if (steer.magnitude > maxForce)
        {
            steer.Scale(new Vector3(maxForce, maxForce, maxForce));
        }

        Debug.DrawLine(transform.position, transform.position + steer, Color.blue);
        return steer * Time.deltaTime;
    }

    private Vector3 Steering()
    {
        Vector3 steeringVector = Vector3.zero;


        return steeringVector;
    }

    //private Vector3 Alignment()
    //{
    //    int neighbourCount = 0;
    //    Vector3 desiredPos = Vector3.zero;
    //    foreach (GameObject agent in GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array)
    //    {
    //        if (agent != this.gameObject)
    //        {
    //            dist = transform.position.x + transform.position.z - agent.transform.position.x - agent.transform.position.z;
    //            if ((agent.transform.position - transform.position).magnitude < alignmentDistance)
    //            {
    //                desiredPos.x += agent.GetComponent<Rigidbody>().velocity.x;
    //                desiredPos.z += agent.GetComponent<Rigidbody>().position.z;
    //                neighbourCount++;
    //            }
    //        }
    //    }

    //    if (neighbourCount == 0)
    //    {
    //        return desiredPos;
    //    }
    //    else
    //    {
    //        desiredPos.x /= neighbourCount;
    //        desiredPos.z /= neighbourCount;
    //        desiredPos = desiredPos.normalized;
    //        desiredPos *= Time.deltaTime;
    //        return desiredPos;
    //    }
    //}

    private Vector3 Cohesion()
    {
        int neighbourCount = 0;
        Vector3 center = Vector3.zero;
        Vector3 desiredPos = Vector3.zero;
        Vector3 steer = Vector3.zero;
        foreach (GameObject agent in GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array)
        {
            if (agent != this.gameObject)
            {
                dist = transform.position.x + transform.position.z - agent.transform.position.x - agent.transform.position.z;

                center += agent.transform.position;
                neighbourCount++;
            }
        }

        if (neighbourCount > 0)
        {
            center += transform.position;
            center /= neighbourCount + 1;

            desiredPos = center - transform.position;
            steer = desiredPos - velocity;
        }

        Debug.DrawLine(transform.position, transform.position + steer, Color.magenta);
        Debug.DrawLine(Vector3.zero, center, Color.white);
        return steer;
    }

    private Vector3 Seperation(GameObject[] agentArray)
    {
        int neighbourCount = 0;
        Vector3 force = Vector3.zero;
        Vector3 steer = Vector3.zero;
        foreach (GameObject agent in agentArray)
        {
            if (agent != this.gameObject)
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, agent.transform.position);
                if (distance < seperationRadius && distance > 0)
                {
                    Vector3 pushForce = this.transform.position - agent.transform.position;
                    float sepDist = force.magnitude;
                    force += this.transform.position - agent.transform.position;
                    neighbourCount++;

                }
            }
        }

        if (neighbourCount > 0)
        {
            force = force / neighbourCount;
            force = force.normalized;
        }
        return force * Time.deltaTime;
    }
}
