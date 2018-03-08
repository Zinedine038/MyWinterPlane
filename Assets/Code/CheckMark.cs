using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMark : MonoBehaviour {
    public static CheckMark instance;
    private bool status;

    private void Awake()
    {
        instance=this;
    }

    public void Set(bool status)
    {
        this.status=status;
        GetComponent<Image>().enabled=status;
    }
    public bool Status()
    {
        return status;
    }
}
