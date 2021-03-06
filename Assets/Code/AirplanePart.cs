﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Engine,
    Chassis
}

public class AirplanePart : Interactable
{
    public string partName;
    private GameObject lastObjectPassed;
    public string[] requiredOtherParts;

    [HideInInspector]
    public bool attached = false;
    public bool beingcarried;

    public float horsePowerIncrease;

    public bool isEngineBlock;
    private void Update()
    {
        if(attached)
        {
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    /// <summary>
    /// Checks if we've entered a zone where we can attach this part to
    /// </summary>
    /// <param name="other">Object that we triggered</param>
    private void OnTriggerEnter(Collider other)
    {
        if(!attached && beingcarried)
        {
            if (other.transform.GetComponent<AttachableSpot>())
            {
                //If the part name matches with the attachablespot name and the parts required are installed show the checkmark to
                //let the player know the part can be attached
                if (other.transform.GetComponent<AttachableSpot>().CanAttach(partName) && EngineManager.instance.PartsIncluded(requiredOtherParts))
                {
                    lastObjectPassed = other.gameObject;
                    CheckMark.instance.Set(true);
                }
            }
        }
    }

    /// <summary>
    /// Same as the enter function but some parts have smaller colliders so this helps the player actually fit the part
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Removes the checkmark if the part is not in the area of a attachablespot
    /// </summary>
    private void OnTriggerExit()
    {
        if(!attached)
        {
            lastObjectPassed = null;
            CheckMark.instance.Set(false);
            if (isEngineBlock)
            {
                StartCoroutine(ReEnableCollider());
            }
        }


    }

    /// <summary>
    /// Attaches the current part to the engine
    /// </summary>
    public IEnumerator Attach()
    {
        if (isEngineBlock)
        {
            foreach(BoxCollider collider in GetComponentsInChildren<BoxCollider>())
            {
                collider.enabled=false;
            }
            foreach (MeshCollider collider in GetComponentsInChildren<MeshCollider>())
            {
                collider.enabled = false;
            }
            GetComponent<MeshCollider>().enabled=true;
        }
        attached =true;
        GetComponent<PickupCarry_Object>().pickupable=false;
        if(!isEngineBlock)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        transform.SetParent(lastObjectPassed.transform);
        lastObjectPassed.GetComponent<AttachableSpot>().filled=true;
        lastObjectPassed = null;
        CheckMark.instance.Set(false);
        EngineManager.instance.AddPart(partName,horsePowerIncrease);
        StartCoroutine("FixPosition");
        yield return null;
    }

    /// <summary>
    /// Makes sure the position is propery set
    /// </summary>
    /// <returns></returns>
    IEnumerator FixPosition()
    {
        yield return null;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Detaches the part from the engine
    /// </summary>
    public IEnumerator Detach()
    {
        if (!isEngineBlock)
        {
            GetComponent<Rigidbody>().constraints= RigidbodyConstraints.None;
        }
        attached = false;
        GetComponent<PickupCarry_Object>().pickupable = true;
        transform.parent.GetComponent<AttachableSpot>().filled = false;
        transform.parent=null;
        GetComponent<Collider>().enabled = true;
        EngineManager.instance.RemovePart(partName, horsePowerIncrease);
        yield return null;
    }

    private IEnumerator ReEnableCollider()
    {
        foreach (BoxCollider collider in GetComponentsInChildren<BoxCollider>())
        {
            collider.enabled = true;
            yield return null;
        }
        foreach (MeshCollider collider in GetComponentsInChildren<MeshCollider>())
        {
            collider.enabled = true;
            yield return null;
        }
    }


}
