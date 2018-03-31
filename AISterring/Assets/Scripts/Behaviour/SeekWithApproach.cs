using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekWithApproach : MonoBehaviour {

    public GameObject leader;
    public float maxSpeed;
    public float maxForce;
    public float approachRadius;

    float dist;
    Vector3 velocity;

    private void Awake()
    {
        velocity = GetComponent<Rigidbody>().velocity;
    }

    private void Update()
    {
        transform.position += Seek(leader);
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
}
