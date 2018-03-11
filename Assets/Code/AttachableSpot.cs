using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableSpot : MonoBehaviour {
    public string attachablePartName;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger=true;
    }

    public bool CanAttach(string part)
    {
        if(part==attachablePartName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
