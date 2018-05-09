using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour {
    public float normalH;
    public float crouchH;
    public float speed;
    public bool crouch;
    public float forcedCrouchH;
    public bool forcedCrouch;
    public CharacterController cc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(forcedCrouch || crouch)
        {
            if(crouch)
            {
                if (cc.height != crouchH)
                {
                    cc.height -= speed * Time.deltaTime;
                    if (cc.height < crouchH)
                    {
                        cc.height = crouchH;
                    }
                }
            }
            if(forcedCrouch)
            {
                if (cc.height != forcedCrouchH)
                {
                    if(cc.height > forcedCrouchH)
                    {
                        cc.height -= speed * Time.deltaTime;
                        if (cc.height < forcedCrouchH)
                        {
                            cc.height = forcedCrouchH;
                        }
                    }
                    if (cc.height < forcedCrouchH)
                    {
                        cc.height += speed * Time.deltaTime;
                        if (cc.height > forcedCrouchH)
                        {
                            cc.height = forcedCrouchH;
                        }
                    }
                }
            }

        }
        else
        {
            if(cc.height != normalH)
            {
                cc.height += speed * Time.deltaTime;
                if (cc.height > normalH)
                {
                    cc.height = normalH;
                }
            }
        }
        crouch = Input.GetButton("Crouch");


        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="CrouchZone")
        {
            forcedCrouch=true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "CrouchZone")
        {
            forcedCrouch = true;
            print(other);
            forcedCrouchH=other.transform.GetComponent<CrouchZone>().height;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "CrouchZone")
        {
            forcedCrouch = false;
        }
    }
}
