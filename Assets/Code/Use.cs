using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Use : MonoBehaviour {

	public float maxRayDistance;
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Use"))
        {
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), direction, out hit, maxRayDistance))
            {
                print("prewow");
                print(hit.collider.transform.name);
                if (hit.collider.transform.GetComponent<Money>())
                {
                    hit.collider.transform.GetComponent<Money>().Take();
                }
                if (hit.collider.transform.GetComponent<Door>())
                {
                    print("wow");
                    hit.collider.transform.GetComponent<Door>().Switch();
                }

            }
        }

    }

}
