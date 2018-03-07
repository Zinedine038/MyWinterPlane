using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePart : Interactable
{
    public string partName;
    private GameObject lastObjectPassed;
    bool attached = false;
    public void Update()
    {
        if(attached)
        {
            transform.localPosition=Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale=new Vector3(1,1,1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<AttachableSpot>())
        {
            if(other.transform.GetComponent<AttachableSpot>().CanAttach(partName))
            {
                print("canAttach");
                lastObjectPassed=other.gameObject;
                CheckMark.instance.Set(true);
            }
        }
    }

    private void OnTriggerExit()
    {
        lastObjectPassed = null;
        CheckMark.instance.Set(false);
    }

    public void Attach()
    {
        attached=true;
        GetComponent<PickupCarry_Object>().pickupable=false;
        GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
        transform.SetParent(lastObjectPassed.transform);
        transform.localPosition=Vector3.zero;
        transform.eulerAngles=Vector3.zero;
        lastObjectPassed = null;
        CheckMark.instance.Set(false);
    }

    public void Detach()
    {
        attached = false;
        GetComponent<PickupCarry_Object>().pickupable = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.parent=null;
    }


}
