using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneManager : MonoBehaviour {
    public float maxThrust;
    public float thrust;
    public GameObject propellor;
    public bool engineOn;
    public bool ignitionPressed;
    public AudioSource source;
    public AudioSource propellorSource;
    public AudioClip crankstart,crankloop,crankend;
    public bool mayStart;
	// Use this for initialization
	void Start () {
        source=GetComponent<AudioSource>();
        //GetComponent<AudioSource>().mute = !engineOn;
        //GetComponent<RearWheelDrive>().enabled = engineOn;


        //engineOn = !engineOn;
        //GetComponent<AudioSource>().mute = !engineOn;
        GetComponent<RearWheelDrive>().enabled = engineOn;
        if (!engineOn)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = transform.forward * thrust / 2;
        }
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
        ignitionPressed = Input.GetKey("joystick button 1");
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
        if (Input.GetKeyDown("joystick button 1"))
        {
            source.mute=false;

            StartCoroutine("IgnitionLoop");
            StartCoroutine("CheckIgnition");
        }
    }

    IEnumerator CheckIgnition()
    {
        while(ignitionPressed)
        {
            yield return null;
        }
        StopCoroutine("IgnitionLoop");
    }

    IEnumerator IgnitionLoop()
    {
        source.PlayOneShot(crankstart);
        yield return new WaitForSeconds(crankstart.length);
        int crank =0;
        while(crank!=5)
        {
            source.PlayOneShot(crankloop);
            yield return new WaitForSeconds(crankloop.length);
            crank++;
        }
        while(mayStart==false)
        {
            source.PlayOneShot(crankloop);
            yield return new WaitForSeconds(crankloop.length);
        }
        source.PlayOneShot(crankend);
        propellorSource.volume=2;
        engineOn = !engineOn;
        GetComponent<AudioSource>().mute = !engineOn;
        GetComponent<RearWheelDrive>().enabled = engineOn;
        if (!engineOn)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = transform.forward * thrust / 2;
        }
        propellorSource.mute = !engineOn;

    }

    public void Propell()
    {
        if (engineOn)
        {
            propellorSource.pitch = 1 + (thrust / 50f);
            propellor.transform.Rotate(0,0,180 * thrust * Time.deltaTime / 6.66f + 200);
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
