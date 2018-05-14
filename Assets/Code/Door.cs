using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour {
    [HideInInspector]
	public bool openClosed = false;
    public Animator anim;
    public string doorName;
    public bool engineCompartment;
    public void Switch()
    {
        openClosed=!openClosed;
        print(openClosed);
        Animate();
        if(engineCompartment)
        {
            AirplaneManager.instance.GetComponent<Rigidbody>().isKinematic=openClosed;
            StartCoroutine("TurnOffEngine");
        }
    }

    IEnumerator TurnOffEngine()
    {
        if(!openClosed)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return null;
        }
        if (AirplaneManager.instance.engineSpot.filled)
        {
            AirplaneManager.instance.engineSpot.transform.GetChild(0).gameObject.SetActive(openClosed);
        }
    }

    void Animate()
    { 
        anim.SetBool(doorName,openClosed);
    }
}
