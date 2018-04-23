using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle {
    public float peakTorque;
    public AnimationCurve torque;
    float currentTorque;
    public float[] gearRatio;
    public int currentGear;
    public float currentRpm=1;
    public float idleRPM;
    public float maxRPM;
    RearWheelDrive rwd;
    AudioSource engineSound;
    bool engineOn;
    bool shifting = false;
    public AudioClip rev,idle,deaccelerate,ignitionSound;
	// Use this for initialization
	void Start () {
        rwd = GetComponent<RearWheelDrive>();
        engineSound=GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(engineOn)
        {
            if (Input.GetKey(KeyCode.W) && !shifting)
            {
                if(currentGear!=0)
                {
                    currentRpm += 1000 * (gearRatio[currentGear] * 0.5f) * Time.deltaTime;
                }
                else
                {
                    currentRpm += 7000 * Time.deltaTime;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) && !shifting)
            {
                ShiftUp();
            }
            else
            {
                currentRpm -= 250 *Time.deltaTime;
            }
            EngineOperate();
            DecideEngineSound();
            
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            StopCoroutine("Ignition");
            StartCoroutine("Ignition");
        }

    }

    IEnumerator Ignition()
    {
        if(engineOn)
        {
            currentRpm=0;
            engineOn=false;
        }
        else if(!engineOn)
        {
            engineSound.PlayOneShot(ignitionSound);
            while (currentRpm<idleRPM)
            {
                idleRPM+=700*Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            engineSound.pitch=1;
            engineOn = true;
            engineSound.Play();
            engineSound.volume=0.1f;
        }
        yield return null;

    }

    void EngineOperate()
    {
        CalculateCurrentTorque();
    }

    void CalculateCurrentTorque()
    {
        currentTorque = torque.Evaluate(currentRpm / maxRPM) * peakTorque;
        rwd.maxTorque = currentTorque*5f*gearRatio[currentGear];
        print(currentTorque);
    }

    void ShiftUp()
    {
        currentRpm =  currentRpm* 0.6f;
        currentGear++;
    }

    void DecideEngineSound()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            engineSound.clip=rev;
            engineSound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            engineSound.clip = deaccelerate;
            engineSound.Play();
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            engineSound.clip = deaccelerate;
            engineSound.Play();
        }
        engineSound.pitch=(currentRpm/maxRPM)+0.5f;
    }
}
