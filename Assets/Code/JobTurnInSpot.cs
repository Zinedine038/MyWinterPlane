using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobTurnInSpot : MonoBehaviour
{

    public List<string> requiredObjects = new List<string>();
    public List<string> currentObjects = new List<string>();
    public List<GameObject> goList = new List<GameObject>();
    public bool complete;

    public void AddObject(string name)
    {
        currentObjects.Add(name);
        complete = CheckCompletion();
    }

    public void RemoveObject(string name)
    {
        currentObjects.Remove(name);
        complete = CheckCompletion();
    }

    bool CheckCompletion()
    {
        foreach (string obj in requiredObjects)
        {
            if (!currentObjects.Contains(obj))
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteItems()
    {
        foreach (GameObject go in goList)
        {
            Destroy(go);
        }
        currentObjects.Clear();
        goList.Clear();
        complete = false;
    }
}
