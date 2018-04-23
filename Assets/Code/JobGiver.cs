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

    private void Update()
    {
        if(mySpot.complete)
        {
            ReadyForTurnIn();
        }
    }

    public void ReadyForTurnIn()
    {
        anim.SetTrigger("GiveMoney");
        ready=true;
    }

    public void TurnIn()
    {
        PlayerManager.instance.money+=reward;

    }
}
