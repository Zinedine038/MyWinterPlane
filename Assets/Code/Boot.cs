using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour {

    public GameObject boot;
	// Use this for initialization
	void Start () {
        StartCoroutine("ReEnableBoot");
    }

    IEnumerator ReEnableBoot()
    {
        yield return null;
        boot.SetActive(true);
    }


}
