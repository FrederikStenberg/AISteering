using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToTarget : MonoBehaviour {

    Vector3 point;

    public bool newTarget = false;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                point = hit.point;
                transform.position = point;
                newTarget = true;
            }

            transform.position = point;
        }       
    }  
}
