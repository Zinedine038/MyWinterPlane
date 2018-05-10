using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour {
    [HideInInspector]
	public bool openClosed = false;
    public Animator anim;
    public string doorName;
    public void Switch()
    {
        openClosed=!openClosed;
        print(openClosed);
        Animate();
    }

    void Animate()
    { 
        anim.SetBool(doorName,openClosed);
    }
}
