using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detacher : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5f))
            {
                GameObject hitObj = hit.transform.gameObject;
                print(hitObj.transform.name);
                if (hitObj.GetComponent<AirplanePart>() != null)
                {
                    if (hitObj.GetComponent<AirplanePart>().attached)
                    {
                        hitObj.GetComponent<AirplanePart>().StartCoroutine("Detach");
                    }
                }
            }
        }
    }
}
