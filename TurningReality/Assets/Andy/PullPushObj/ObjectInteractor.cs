using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ObjectInteractor : MonoBehaviour
{
    Transform target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //if (target == null && CrossPlatformInputManager)
        Ray interactRay = new Ray(transform.position, transform.forward);
        int interactableLayer = LayerMask.GetMask("Interactable");

        int interactableMask = 1 << interactableLayer;

        RaycastHit hit;
        if (Physics.Raycast(interactRay, out hit, 20, interactableMask))
        {
           target = hit.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay()
    }

}
