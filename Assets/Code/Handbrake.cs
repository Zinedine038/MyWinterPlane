using UnityEngine;

public class Handbrake : MonoBehaviour {
    public static Handbrake instance;
    public Rigidbody plane;
	public bool engaged;
    public void Awake()
    {
        instance = this;
    }

    public void FixedUpdate()
    {
        if(engaged)
        {
            plane.velocity=new Vector3(0,0,0);
        }
    }
}
