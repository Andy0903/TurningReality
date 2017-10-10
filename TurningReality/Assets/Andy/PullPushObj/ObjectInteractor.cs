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
    float minDistance = 0.3f;
    [SerializeField]
    float maxDistance = 50f;
    [SerializeField]
    Vector3 holdOffset = new Vector3(0f, -0.2f, 0f);

    private void Start()
    {
        slot = gameObject.GetComponent<ThirdPersonUserControl>().PlayerControls;
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

            target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            FixedJoint joint = target.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = gameObject.GetComponent<Rigidbody>();
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
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
                Debug.Log("!");
            }
        }
        else if ((slot == PlayerSlot.First && CrossPlatformInputManager.GetButtonDown("Interact_P1"))
                || slot == PlayerSlot.Second && CrossPlatformInputManager.GetButtonDown("Interact_P2"))
        {
            DropObject();
        }
        else
        {
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
        // target.SetParent(targetParent);
        // targetParent = null;
        target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(target.gameObject.GetComponent<FixedJoint>());
        target = null;
    }
}
