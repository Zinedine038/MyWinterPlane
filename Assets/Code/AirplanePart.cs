using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePart : Interactable
{
    public string partName;
    private GameObject lastObjectPassed;
    public string[] requiredOtherParts;

    [HideInInspector]
    public bool attached = false;
    public bool beingcarried;
    public void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("button " + i + " was pressed!");
            }
        }
        if (attached)
        {
            transform.localPosition=Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale=new Vector3(1,1,1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!attached && beingcarried)
        {
            if (other.transform.GetComponent<AttachableSpot>())
            {
                if (other.transform.GetComponent<AttachableSpot>().CanAttach(partName) && EngineManager.instance.PartsIncluded(requiredOtherParts))
                {
                    print("canAttach");
                    lastObjectPassed = other.gameObject;
                    CheckMark.instance.Set(true);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!attached && beingcarried)
        {
            if (other.transform.GetComponent<AttachableSpot>())
            {
                if (other.transform.GetComponent<AttachableSpot>().CanAttach(partName) && EngineManager.instance.PartsIncluded(requiredOtherParts))
                {
                    print("canAttach");
                    lastObjectPassed = other.gameObject;
                    CheckMark.instance.Set(true);
                }
            }
        }
    }

    private void OnTriggerExit()
    {
        if(!attached)
        {
            lastObjectPassed = null;
            CheckMark.instance.Set(false);
        }

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
        EngineManager.instance.AddPart(partName);
        //GetComponent<Collider>().enabled=false;
    }

    public void Detach()
    {
        attached = false;
        GetComponent<PickupCarry_Object>().pickupable = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.parent=null;
        GetComponent<Collider>().enabled = true;
        EngineManager.instance.RemovePart(partName);
    }


}
