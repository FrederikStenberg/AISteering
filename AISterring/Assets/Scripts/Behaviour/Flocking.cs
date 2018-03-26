using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperation : MonoBehaviour {

    private Vector3 velocity = new Vector3(0,0,0);
    private Vector3 alignmentVector;
    private float dist;
    private int neighbourCount = 0;
    public GameObject[] agents = new GameObject[10];

    public Vector3 ComputeAlignment()
    {
        foreach (GameObject agent in agents)
        {
            if (agent != this.gameObject)
            {
                dist = transform.position.x + transform.position.z - agent.transform.position.x - agent.transform.position.z;
                if (dist < 0)
                {
                    dist *= -1;
                }

                if (dist < 300)
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
            alignmentVector = alignmentVector.normalized;
            alignmentVector *= Time.deltaTime;
            return alignmentVector;
        }
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity += ComputeAlignment();
    }
}
