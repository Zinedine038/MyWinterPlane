using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class PlaneEnter : MonoBehaviour {
    public FirstPersonController fps;
    public CharacterController cc;
    public SimpleMouseRotator mouseLook;
    public Camera eyes;
    public Transform placementPlayer;
    public PlaneExit pe;
    public AirplaneManager apm;
    public void Enter()
    {
        pe.enabled=true;
        cc.enabled=false;
        fps.enabled=false;
        fps.transform.position=placementPlayer.position;
        fps.transform.eulerAngles=placementPlayer.eulerAngles;
        fps.transform.SetParent(placementPlayer);
        eyes.transform.localEulerAngles=new Vector3(0,0,0);
        apm.enabled=true;
        mouseLook.enabled=true;
    }
}
