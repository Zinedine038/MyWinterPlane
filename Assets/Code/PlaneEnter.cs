using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlaneEnter : MonoBehaviour {
    public FirstPersonController cc;
    public Door airplaneDoor;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "PlaneEnter" && Input.GetButtonDown("Use"))
        {
            FindObjectOfType<AirplaneManager>().enabled = !FindObjectOfType<AirplaneManager>().enabled;
            cc.enabled = !cc.enabled;
            if(!cc.enabled)
            {
                cc.transform.SetParent(other.transform);
                cc.GetComponent<Crouch>().enabled=false;
                cc.transform.GetComponent<CharacterController>().height=1.5f;
                if(airplaneDoor.openClosed==true)
                {
                    airplaneDoor.Switch();
                }
                FindObjectOfType<AirplaneManager>().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            else
            {
                cc.GetComponent<Crouch>().enabled = true;
                cc.transform.SetParent(null);
            }
        }
    }
}
