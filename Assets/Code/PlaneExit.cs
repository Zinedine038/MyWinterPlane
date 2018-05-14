using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class PlaneExit : MonoBehaviour
{

    public FirstPersonController fps;
    public CharacterController cc;
    public SimpleMouseRotator mouseLook;
    public Camera eyes;
    public Transform placementPlayer;
    public AirplaneManager apm;

    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            StartCoroutine("ExitPlane");
        }
    }

    IEnumerator ExitPlane()
    {
        if (AirplaneManager.instance.engineSpot.filled)
        {
            //AirplaneManager.instance.engineSpot.transform.GetChild(0).gameObject.SetActive(true);
            if (apm.engineOn)
            {
                apm.TurnOffEngine();
                yield return new WaitForSeconds(2f);
            }
        }
        if(AirplaneManager.instance.thrust==0)
        {
            Handbrake.instance.engaged = true;
        }
        fps.transform.position = placementPlayer.position;
        fps.transform.eulerAngles = placementPlayer.eulerAngles;
        fps.transform.SetParent(null);
        cc.enabled = true;
        fps.enabled = true;
        eyes.transform.localEulerAngles = new Vector3(0, 0, 0);
        apm.enabled = false;
        mouseLook.enabled = false;
        this.enabled = false;
    }
}
