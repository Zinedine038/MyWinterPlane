using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "VehicleEnter")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                print("does");
                other.transform.GetComponent<Vehicle>().Enter();
            }
        }
    }
}
