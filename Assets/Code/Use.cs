using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour {

	public float maxRayDistance;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Use"))
        {
            print("lol");
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), direction, out hit, maxRayDistance))
            {
                print("lol2");
                if (hit.transform.GetComponent<Money>())
                {
                    print("lol3");
                    hit.transform.GetComponent<Money>().Take();
                }

            }
        }

    }
}
