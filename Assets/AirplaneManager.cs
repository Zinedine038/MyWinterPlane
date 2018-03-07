using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneManager : MonoBehaviour {
    public float maxThrust;
    public float thrust;
    public GameObject propellor;
    public bool engineOn;
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().mute = !engineOn;
        GetComponent<RearWheelDrive>().enabled = engineOn;
    }

    // Update is called once per frame
    void Update () {
        print(GetComponent<Rigidbody>().velocity);
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
        if(Input.GetButton("Jump"))
        {
            Brake();
        }
        Ignition();
        Propell();
        Throttle();
    }

    void Brake()
    {
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime);
    }

    public void Ignition()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        transform.Rotate(Input.GetAxis("Vertical") * 0.33F, 0.0f, -Input.GetAxis("Horizontal") * 0.75F);
        transform.position+=transform.forward*Time.deltaTime*thrust;
    }

    public void Throttle()
    {
        if (Input.GetKey(KeyCode.LeftShift) && engineOn)
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
        if (Input.GetKey(KeyCode.LeftControl) && engineOn)
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
