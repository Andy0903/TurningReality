using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float pushForce = 6.0f;
    //private ThirdPersonController tpc;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rgBody = hit.collider.attachedRigidbody;
        if(rgBody == null || rgBody.isKinematic)
        {
            return;
        }

        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }

    //    pushForce = tpc.GetSpeed();
        Vector3 direction = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
        rgBody.velocity = direction * pushForce;
    }
	// Use this for initialization
	void Start ()
    {
     //   tpc = gameObject.GetComponent<ThirdPersonController>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
