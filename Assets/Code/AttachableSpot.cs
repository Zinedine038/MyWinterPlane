using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableSpot : MonoBehaviour {
    public string attachablePartName;
    [HideInInspector]
    public bool filled;
    private void Awake()
    {
        GetComponent<Collider>().isTrigger=true;
    }

    public bool CanAttach(string part)
    {
        if(part==attachablePartName && !filled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
