using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobGiver : MonoBehaviour {
    public JobTurnInSpot mySpot;
    public Animator anim;
    public int reward;
    bool ready;

    private void Start()
    {
        anim=GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(mySpot.complete)
        {
            ReadyForTurnIn();
        }
        else
        {
            GoBackToIdle();
        }
    }

    public void ReadyForTurnIn()
    {
        anim.SetTrigger("GiveMoney");
        ready=true;
    }

    public void GoBackToIdle()
    {
        anim.SetTrigger("Idle");
        ready = false;
    }

    public void TurnIn()
    {
        PlayerManager.instance.money+=reward;

    }

    public void GiveMoney()
    {
        PlayerManager.instance.money+=reward;
    }
}
