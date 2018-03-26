using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToTarget : MonoBehaviour {

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                transform.position = hit.transform.position;
            }
        }       
    }  
}
