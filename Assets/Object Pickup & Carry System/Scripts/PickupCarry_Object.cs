using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PickupCarry_Object : MonoBehaviour
{
    [HideInInspector]
    public bool collided = false;

    private void Start()
    {
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        if (!GetComponent<Collider>())
            Debug.LogError("No Collider Found! " + this.gameObject);
    }

    void OnCollisionEnter()
    {
        collided = true;
    }

    void OnCollisionExit()
    {
        collided = false;
    }
}
