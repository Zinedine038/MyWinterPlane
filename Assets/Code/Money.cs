using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    [HideInInspector]
	public int amount;

    public void Take()
    {
        PlayerManager.instance.money+=amount;
        transform.root.GetComponent<JobGiver>().mySpot.DeleteItems();
        transform.root.GetComponent<JobGiver>().GoBackToIdle();
        Destroy(this.gameObject);
    }
}
