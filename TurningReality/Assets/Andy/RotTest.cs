using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RotTest : MonoBehaviour {
      
	void Update ()
    {
        if (CrossPlatformInputManager.GetAxis("RotX") > 0)
        {
            transform.RotateAround(transform.position, Camera.main.transform.up, -1);
        }
        else if (CrossPlatformInputManager.GetAxis("RotX") < 0)
        {
            transform.RotateAround(transform.position, Camera.main.transform.up, 1);
        }
    }
}
