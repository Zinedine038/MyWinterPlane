using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlaneStatus
{
    Starts,
    Cranks,
    TotalLoss
}

public class AirplaneManager : MonoBehaviour {
    public static AirplaneManager instance;
    public float maxThrust;
    public float thrust;
    public GameObject propellor;
    public bool engineOn;
    public bool ignitionPressed;
    public AudioSource source;
    public AudioSource propellorSource;
    public AudioClip crankstart,crankloop,crankend, key;
    public bool mayStart;
    public bool mayCrank;

    [Header("Dashboard")]
    public Image checkEngineLight;
    public Image oilPressureLight;
    public Image batteryLight;
    public Image fuelLight;
    public Image waterTemperatureLight;

    public AttachableSpot engineSpot;
    private float engineTemperature;
    // Use this for initialization

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        source=GetComponent<AudioSource>();
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
            source.PlayOneShot(key);
            if(!engineOn)
            {
                checkEngineLight.enabled = true;
                batteryLight.enabled = true;
                oilPressureLight.enabled = true;
                switch (EngineDiagnose())
                {
                    case PlaneStatus.Cranks:
                        mayStart=false;
                        mayCrank=true;
                        break;
                    case PlaneStatus.Starts:
                        mayCrank=true;
                        mayStart = true;
                        break;
                    case PlaneStatus.TotalLoss:
                        mayCrank = false;
                        break;

                }
                source.mute = false;
                if(mayCrank)
                {
                    StartCoroutine("IgnitionLoop");
                    StartCoroutine("CheckIgnition");
                }
                StopCoroutine("KillEngine");
                
            }
            else
            {
                TurnOffEngine();
            }

        }
        if (Input.GetKeyUp("joystick button 1"))
        {
            if(!engineSpot.filled)
            {
                checkEngineLight.enabled = false;
                batteryLight.enabled = false;
                oilPressureLight.enabled = false;
            }
            source.Stop();
            source.PlayOneShot(key);
        }
    }

    public void TurnOffEngine()
    {
        engineOn = !engineOn;
        GetComponent<RearWheelDrive>().enabled = engineOn;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = transform.forward * thrust / 2;
        StartCoroutine("KillEngine");
    }

    IEnumerator CheckIgnition()
    {
        float speed = 101;
        while (ignitionPressed)
        {
            if(!engineOn)
            {
                if(speed<1500)
                {
                    speed += 1000 * Time.deltaTime; 
                }
                propellor.transform.Rotate(0, 0, speed * Time.deltaTime);
            }
            yield return null;
        }
        StopCoroutine("IgnitionLoop");
        if(engineOn)
        {
            if (EngineContains("Dynamo") && EngineContains("Fanbelt"))
            {
                batteryLight.enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            if (EngineContains("Carterpan"))
            {
                oilPressureLight.enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
            checkEngineLight.enabled = false;
        }

    }

    IEnumerator IgnitionLoop()
    {

        source.PlayOneShot(crankstart);
        yield return new WaitForSeconds(crankstart.length);
        int crank =0;
        while(crank!=2)
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
        yield return new WaitForSeconds(2.25f);
        propellorSource.volume=2;
        engineOn = !engineOn;
        GetComponent<RearWheelDrive>().enabled = engineOn;
        if (!engineOn)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = transform.forward * thrust / 2;
        }
        thrust = 0;
        propellorSource.mute = !engineOn;
        propellorSource.pitch=1;
    }

    IEnumerator KillEngine()
    {
        float tempThrust = thrust;
        float tempThrustDecrease = thrust;
        float curPitch = propellorSource.pitch;
        while(propellorSource.pitch>0)
        {
            tempThrust-=tempThrustDecrease*Time.deltaTime;
            propellorSource.pitch-=curPitch*Time.deltaTime;
            propellor.transform.Rotate(0, 0, 180 * tempThrust * Time.deltaTime / 6.66f + 200);
            yield return null;
        }
        propellorSource.mute = !engineOn;
    }

    public void Propell()
    {
        if (engineOn)
        {
            propellorSource.pitch = 1 + (thrust / 50f);
            propellor.transform.Rotate(0, 0, 180 * thrust * Time.deltaTime / 6.66f + 200);
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

    public PlaneStatus EngineDiagnose()
    {
        if(!engineSpot.filled)
        {
            return PlaneStatus.TotalLoss;
        }
        maxThrust=0;
        for (int i = 1; i <= 8; i++)
        {
            if (EngineContains("Piston" + i))
            {
                maxThrust += 6.25f;
            }
        }
        foreach (string part in EngineManager.instance.attachedParts)
        {
            if (part == "Sparkplug")
            {
                maxThrust += 6.25f;
            }
        }
        if (!EngineContains("Crankshaft"))
        {
            print("Missing Crankshaft");
            return PlaneStatus.TotalLoss;
        }
        if (!EngineContains("TimingBelt"))
        {
            print("Missing TimingBelt");
            return PlaneStatus.TotalLoss;
        }
        if (!EngineContains("CylinderHead"))
        {
            print("Missing CylinderHead");
            return PlaneStatus.TotalLoss;
        }
        if (!EngineContains("Electronics1-2") || !EngineContains("Electronics2-2"))
        {
            print("Missing Electronics1");
            return PlaneStatus.TotalLoss;
        }
        if (!EngineContains("FuelRail"))
        {
            print("Missing FuelRail");
            return PlaneStatus.Cranks;
        }
        if (!EngineContains("IntakeManifold"))
        {
            print("Missing IntakeManifold");
            return PlaneStatus.Cranks;
        }

        return PlaneStatus.Starts;

    }

    bool EngineContains(string part)
    {
        return EngineManager.instance.attachedParts.Contains(part);
    }
}
