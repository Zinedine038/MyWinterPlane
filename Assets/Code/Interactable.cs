using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Interactables
{
    Generic,
    AirplanePart,
    JobObject
}

public class Interactable : MonoBehaviour
{
    public Interactables interactable;
    public string name;
    private void OnTriggerEnter(Collider other)
    {
        if(interactable==Interactables.JobObject)
        {
            if(other.gameObject.GetComponent<JobTurnInSpot>()!=null)
            {
                other.gameObject.GetComponent<JobTurnInSpot>().AddObject(name);
                other.gameObject.GetComponent<JobTurnInSpot>().goList.Add(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactable == Interactables.JobObject)
        {
            if (other.gameObject.GetComponent<JobTurnInSpot>() != null)
            {
                other.gameObject.GetComponent<JobTurnInSpot>().RemoveObject(name);
                other.gameObject.GetComponent<JobTurnInSpot>().goList.Remove(this.gameObject);
            }
        }
    }

}
