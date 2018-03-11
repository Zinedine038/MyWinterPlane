using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detacher : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("looool");
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5f))
            {
                GameObject hitObj = hit.transform.gameObject;
                print(hitObj.transform.name);
                if (hitObj.GetComponent<AirplanePart>() != null)
                {
                    print("PART IS HERE");
                    if (hitObj.GetComponent<AirplanePart>().attached)
                    {
                        print("D E T A C H");
                        hitObj.GetComponent<AirplanePart>().Detach();
                    }
                }
            }
        }
    }
}
