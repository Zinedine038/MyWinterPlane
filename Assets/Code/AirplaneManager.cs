using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneManager : MonoBehaviour {
    public float maxThrust;
    public float thrust;
    public GameObject propellor;
    public bool engineOn;
    public GameObject rearLift, leftWingThing, rightWingThing;  
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().mute = !engineOn;
        GetComponent<RearWheelDrive>().enabled = engineOn;
    }

    // Update is called once per frame
    void Update () {
        if(engineOn)
        {
            if (thrust > 30)
            {
                GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
        if(Input.GetButton("Jump")|| Input.GetKey("joystick button 2"))
        {
            Brake();
        }
        Ignition();
        Propell();
        Throttle();
        rearLift.transform.localEulerAngles = new Vector3(0,0,Input.GetAxis("Vertical")*40);
        if(Input.GetAxis("Horizontal")<0)
        {
            print("lol");
            leftWingThing.transform.localEulerAngles = new Vector3(0, 0, -Input.GetAxis("Horizontal") * 40);
            rightWingThing.transform.localEulerAngles = new Vector3(0, 0, Input.GetAxis("Horizontal") * 40);
        }
        else if(Input.GetAxis("Horizontal")>0)
        {
            print("lol2");
            leftWingThing.transform.localEulerAngles = new Vector3(0, 0, -Input.GetAxis("Horizontal") * 40);
            rightWingThing.transform.localEulerAngles = new Vector3(0, 0, Input.GetAxis("Horizontal") * 40);
        }

    }

    void Brake()
    {
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime);
    }

    public void Ignition()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            engineOn = !engineOn;
            GetComponent<AudioSource>().mute = !engineOn;
            GetComponent<RearWheelDrive>().enabled = engineOn;
            if(!engineOn)
            {
                GetComponent<Rigidbody>().useGravity=true;
                GetComponent<Rigidbody>().velocity=transform.forward*thrust/2;
            }
        }
    }

    public void Propell()
    {
        if (engineOn)
        {
            GetComponent<AudioSource>().pitch = 1 + (thrust / 50f);
            propellor.transform.Rotate(180 * thrust * Time.deltaTime / 6.66f + 200, 0, 0);
        }
        else
        {
            if (thrust > 0)
            {
                thrust -= 10 * Time.deltaTime;
                if (thrust < 0)
                {
                    thrust = 0;
                }
            }
            propellor.transform.Rotate(180 * thrust * Time.deltaTime / 6.66f, 0, 0);
        }
        transform.Rotate(Input.GetAxis("Vertical") * 0.75f, Input.GetAxis("Horizontal") * 0.33f, -Input.GetAxis("Horizontal") * 1F);
        transform.position+=transform.forward*Time.deltaTime*thrust;
        
    }

    public void Throttle()
    {
        if (Input.GetKey("joystick button 5") && engineOn)
        {
            if (thrust < maxThrust)
            {
                thrust += 10 * Time.deltaTime;
                if (thrust > maxThrust)
                {
                    thrust = 100;
                }
            }
        }
        if (Input.GetKey("joystick button 3") && engineOn)
        {
            if (thrust > 0)
            {
                thrust -= 10 * Time.deltaTime;
                if (thrust < 0)
                {
                    thrust = 0;
                }
            }
        }

    }
}
