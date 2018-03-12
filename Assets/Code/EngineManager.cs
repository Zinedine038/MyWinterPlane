using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour {

    public static EngineManager instance;
    public AudioClip attachClip;
    public List<string> attachedParts = new List<string>();	
    public bool PartsIncluded(string[] parts)
    {
        foreach(string part in parts)
        {
            if(!attachedParts.Contains(part))
            {
                return false;
            }
        }
        return true;
    }


    void Awake()
    {
        instance=this; 
    }

    public void AddPart(string part)
    {
        attachedParts.Add(part);
        GetComponent<AudioSource>().PlayOneShot(attachClip);
    }

    public void RemovePart(string part)
    {
        attachedParts.Remove(part);
    }
}
