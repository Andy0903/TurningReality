using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class ObjectInteractor : MonoBehaviour
{
    Transform targetParent;
    Transform target;
    PlayerSlot slot;

    [SerializeField]
    Vector3 rayOffset = new Vector3(0, -1f, 0);
    [SerializeField]
    float interactionRange = 2f;
    [SerializeField]
    float minDistance = 0f;
    [SerializeField]
    float maxDistance = 50f;
    [SerializeField]
    Vector3 holdOffset = new Vector3(0f, -0.2f, 0f);


    Rigidbody rb;
    Vector3 grabRotation;

    private void Start()
    {
        slot = gameObject.GetComponent<ThirdPersonUserControl>().PlayerControls;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void CastRay()
    {
        Ray hitRay = new Ray(transform.position - rayOffset, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(hitRay, out hit, interactionRange, 1 << 8))
        {
            targetParent = hit.transform.parent;
            target = hit.transform;
            //target.transform.position = target.transform.position - holdOffset;

            grabRotation = transform.eulerAngles;
            target.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            target.SetParent(transform);
            FixedJoint joint = target.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = gameObject.GetComponent<Rigidbody>();
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
            Debug.Log("!");
        }
        Debug.Log(target);
    }

    private void Update()
    {
        if (target == null)
        {
            if ((slot == PlayerSlot.First && CrossPlatformInputManager.GetButtonDown("Interact_P1"))
                || slot == PlayerSlot.Second && CrossPlatformInputManager.GetButtonDown("Interact_P2"))
            {
                CastRay();
            }
        }
        else if ((slot == PlayerSlot.First && CrossPlatformInputManager.GetButtonDown("Interact_P1"))
                || slot == PlayerSlot.Second && CrossPlatformInputManager.GetButtonDown("Interact_P2"))
        {
            DropObject();
        }
        else
        {
            transform.eulerAngles = grabRotation;

            Vector3 backwardF = 1 * -transform.forward * Time.deltaTime;
            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
            {
                if (target.GetComponent<Rigidbody>().mass < 20)
                {
                    transform.Translate(backwardF);
                }
            }
            else if (slot == PlayerSlot.First)
            {
                if (CrossPlatformInputManager.GetAxis("Horizontal_P1") < 0 || CrossPlatformInputManager.GetAxis("Vertical_P1") < 0)
                {
                    if (target.GetComponent<Rigidbody>().mass < 20)
                    {
                        transform.Translate(backwardF);
                    }
                }
            }
            else if (slot == PlayerSlot.Second)
            {
                if (CrossPlatformInputManager.GetAxis("Horizontal_P2") < 0 || CrossPlatformInputManager.GetAxis("Vertical_P2") < 0)
                {
                    if (target.GetComponent<Rigidbody>().mass < 20)
                    {
                        transform.Translate(backwardF);
                    }
                }
            }

            Vector3 fromTargetToPlayer = transform.position - target.position;
            float distance = fromTargetToPlayer.magnitude;
            Vector3 direction = fromTargetToPlayer.normalized;

            Debug.Log(distance);

            if (distance > maxDistance || distance < minDistance)
            {
                DropObject();
            }
        }
    }

    private void DropObject()
    {
        // target.GetComponent<Rigidbody>().isKinematic = false;
        target.SetParent(targetParent);
        targetParent = null;

        target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(target.gameObject.GetComponent<FixedJoint>());

        target = null;
    }
}
