using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobGiver : MonoBehaviour {
    public JobTurnInSpot mySpot;
    public Animator anim;
    public int reward;
    public Transform moneyRewardSpot;
    public GameObject moneyPrefab;
    bool ready;

    private void Start()
    {
        anim=GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(mySpot.complete && !ready)
        {
            ReadyForTurnIn();
        }
        else if(!mySpot.complete && ready)
        {
            GoBackToIdle();
        }
    }

    public void ReadyForTurnIn()
    {
        anim.SetTrigger("GiveMoney");
        GameObject prefab = Instantiate(moneyPrefab);
        prefab.transform.SetParent(moneyRewardSpot);
        prefab.transform.localPosition=Vector3.zero;
        prefab.transform.localEulerAngles=Vector3.zero;
        prefab.GetComponentInChildren<Money>().amount=reward;
        ready=true;
    }

    public void GoBackToIdle()
    {
        anim.SetTrigger("Idle");
        ready = false;
    }
}
