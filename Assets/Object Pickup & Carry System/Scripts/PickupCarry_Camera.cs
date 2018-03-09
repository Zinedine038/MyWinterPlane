using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PickupCarry_Camera : MonoBehaviour
{
    public bool isCarrying;
    public bool allowRotation;
    public bool setObjectRotation; 

    public enum CarryModes
    {
        toggle,
        hold
    };
    public CarryModes carryMode;

    public enum ObjectModes
    {
        script,
        tag
    };
    public ObjectModes objectMode;

    public string objectTag;

    public KeyCode pickupButton;
    public KeyCode rotateL;
    public KeyCode rotateR;

    public GameObject currentObject;
    public GameObject target;

    public float followSpeed;
    public float rotateSpeed;
    public float distanceToDrop;
    public float distance;


    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target = null; Please assign a target");
        }
    }

    private void LateUpdate()
    {
    }

    void Update()
    {
        if (isCarrying)
        {
            currentObject.GetComponent<Rigidbody>().velocity = ((target.transform.position - currentObject.transform.position) * Time.deltaTime * followSpeed);
        }

        if (carryMode == CarryModes.toggle)
        {
            if (Input.GetKeyDown(pickupButton))
            {
                if (!isCarrying)
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        GameObject hitObj = hit.transform.gameObject;

                       if (hitObj.GetComponent<PickupCarry_Object>() != null && hitObj != null)
                       {
                            if(hitObj.GetComponent<PickupCarry_Object>().pickupable==true)
                            {
                                PickUpObject(hitObj);
                                print(hitObj.transform.name);
                            }
                       }
                    }
                }
                else
                {
                    if(currentObject.GetComponent<Interactable>().interactable == Interactables.AirplanePart && CheckMark.instance.Status()==true)
                    {
                        GameObject current = currentObject;
                        DropCurrentObject();
                        current.GetComponent<AirplanePart>().Attach();
                    }
                    else
                    {
                        DropCurrentObject();
                    }
                }
            }
        }
        else if (carryMode == CarryModes.hold)
        {
            if (Input.GetKey(pickupButton))
            {
                if (!isCarrying)
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Vector3.Distance(target.transform.position, transform.position)))
                    {
                        GameObject hitObj = hit.transform.gameObject;

                        if (hitObj.GetComponent<PickupCarry_Object>() != null && hitObj != null)
                        {
                            PickUpObject(hitObj);
                        }
                    }
                }
            }
            else if (isCarrying)
            {
                DropCurrentObject();
            }
        }


        if (isCarrying  && allowRotation)
        {
            if(Input.GetAxis(("Mouse ScrollWheel"))>0)
            {
                currentObject.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime,0, 0));
            }

            if (Input.GetAxis(("Mouse ScrollWheel")) < 0)
            {
                currentObject.transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime, 0, 0));
            }
        }
    }


    private void FixedUpdate()
    {
        if (isCarrying)
        {
            if (currentObject.GetComponent<PickupCarry_Object>().collided == true)
            {
                float dist = Vector3.Distance(target.transform.position, currentObject.transform.position);
                if (dist > distanceToDrop)
                {
                    DropCurrentObject();
                }
            }
        }
    }


    void PickUpObject(GameObject obj)
    {
        currentObject = obj;
        currentObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (setObjectRotation)
        {
            float yRot = currentObject.transform.rotation.eulerAngles.y;
            float direction = Mathf.RoundToInt(yRot / 90.0f) * 90.0f;
            //currentObject.transform.rotation = Quaternion.Euler(0, direction, 0);
            currentObject.GetComponent<Rigidbody>().useGravity = false;
        }

        isCarrying = true;
    }


    void DropCurrentObject()
    {
        currentObject.GetComponent<Rigidbody>().useGravity = true;
        currentObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        currentObject = null;
        isCarrying = false;
    }
}
