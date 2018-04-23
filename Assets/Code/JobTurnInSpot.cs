using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobTurnInSpot : MonoBehaviour {

    public List<string> requiredObjects = new List<string>();
    public List<string> currentObjects = new List<string>();
    public bool complete;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddObject(string name)
    {
        currentObjects.Add(name);
    }

    public void RemoveObject(string name)
    {
        currentObjects.Remove(name);
    }

    void CheckCompletion()
    {

    }
}
