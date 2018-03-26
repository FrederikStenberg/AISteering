using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour {

    private Vector3 alignmentVector = new Vector3(0,0,0);
    private float dist;
    private int neighbourCount = 0;

    public float alignmentDistance = 100;
    public float cohestionDistance = 100;
    public float seperationDistance = 50;

    private Vector3 Alignment()
    {
        foreach (GameObject agent in GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array)
        {
            if (agent != this.gameObject)
            {
                dist = transform.position.x + transform.position.z - agent.transform.position.x - agent.transform.position.z;
                if ((agent.transform.position - transform.position).magnitude < alignmentDistance)
                {
                    alignmentVector.x += agent.GetComponent<Rigidbody>().velocity.x;
                    alignmentVector.z += agent.GetComponent<Rigidbody>().position.z;
                    neighbourCount++;
                }
            }
        }

        if (neighbourCount == 0)
        {
            return alignmentVector;
        }
        else
        {
            alignmentVector.x /= neighbourCount;
            alignmentVector.z /= neighbourCount;
            alignmentVector = alignmentVector.normalized;
            alignmentVector *= Time.deltaTime;
            return alignmentVector;
        }
    }

    private Vector3 Cohesion()
    {
        foreach (GameObject agent in GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array)
        {
            if (agent != this.gameObject)
            {
                dist = transform.position.x + transform.position.z - agent.transform.position.x - agent.transform.position.z;
                if ((agent.transform.position - transform.position).magnitude < cohestionDistance)
                {
                    alignmentVector.x += agent.transform.position.x;
                    alignmentVector.z += agent.transform.position.z;
                    neighbourCount++;
                }
            }
        }

        if (neighbourCount == 0)
        {
            return alignmentVector;
        }
        else
        {
            alignmentVector.x /= neighbourCount;
            alignmentVector.z /= neighbourCount;
            alignmentVector = new Vector3(alignmentVector.x - transform.position.x, alignmentVector.z - transform.position.z);
            alignmentVector = alignmentVector.normalized;
            alignmentVector *= Time.deltaTime;
            return alignmentVector;
        }
    }

    private Vector3 Seperation()
    {
        foreach (GameObject agent in GameObject.Find("AgentArray").GetComponent<AgentArrayScript>().array)
        {
            if (agent != this.gameObject)
            {
                dist = transform.position.x + transform.position.z - agent.transform.position.x - agent.transform.position.z;

                if ((agent.transform.position - transform.position).magnitude < seperationDistance)
                {
                    alignmentVector.x += agent.transform.position.x - transform.position.x;
                    alignmentVector.z += agent.transform.position.z - transform.position.z;
                    neighbourCount++;
                }
            }
        }

        if (neighbourCount == 0)
        {
            return alignmentVector;
        }
        else
        {
            alignmentVector.x /= neighbourCount;
            alignmentVector.z /= neighbourCount;
            alignmentVector.x *= -1;
            alignmentVector.z *= -1;
            alignmentVector = alignmentVector.normalized;
            alignmentVector *= Time.deltaTime;
            return alignmentVector;
        }
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity += Alignment() + Cohesion() + Seperation();

        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized;

    }
}
