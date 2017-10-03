using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Vector3 targetAngle;
    private Vector3 currentAngle;
    private Vector3 accumulateForward;
    private Vector3 accumulateRight;

    private float tiltAngle = 90;
    private bool rotating = false;

    public void Start()
    {
        accumulateForward = transform.forward;
        accumulateRight = transform.right;
        currentAngle = transform.eulerAngles;
        targetAngle = currentAngle;
    }

    public void Update()
    {
        if (!rotating)
        {
            float inputLeftRight = Input.GetAxis("RotY"),
                inputUpDown = Input.GetAxis("RotX");

            if (inputUpDown != 0 || inputLeftRight != 0)
            {
                Vector3 axis = Vector3.zero;
                // We want to rotate the object to the left or right, and we care about which vector points forward (objects Z or Y)
                if (inputUpDown != 0 && inputLeftRight == 0)
                {
                    axis = inputUpDown * Vector3.forward;
                }
                // We want to rotate the object to the up or down, and we care about which vector points right (objects Y or X)
                else if (inputUpDown == 0 && inputLeftRight != 0)
                {
                    axis = inputLeftRight * Vector3.right;
                }

                axis.Normalize();
                rotating = true;
                targetAngle += axis * tiltAngle;
            }
        }

        currentAngle = Vector3.Lerp(currentAngle, targetAngle, Time.deltaTime * 4);

        if (Vector3.Distance(currentAngle, targetAngle) <= 2)
        {
            //Snaps to target angle - remove for complete smoothness or decrease value check in bool
            currentAngle = targetAngle;
            rotating = false;
        }
        transform.eulerAngles = currentAngle;
        print(transform.eulerAngles);
    }


    //private Transform cameraTransform;
    //private Vector3 startEulerAngle, angleAccumulator, targetEulerAngle;
    //private float tiltAngle = 90;
    //private bool rotating = false;

    //public void Start()
    //{
    //    cameraTransform = Camera.main.transform;
    //    startEulerAngle = transform.eulerAngles;
    //    angleAccumulator = startEulerAngle;
    //    targetEulerAngle = startEulerAngle;
    //}

    //public void Update()
    //{
    //    // Used to check whether the rotation is positive or negative
    //    float inputLeftRight = Input.GetAxis("RotY"),
    //        inputUpDown = Input.GetAxis("RotX");

    //    if (!rotating)
    //    {
    //        // If register input start rotation
    //        if (inputLeftRight != 0 || inputUpDown != 0)
    //        {
    //            rotating = true;
    //            // Bring camera's forward(Z)- & right(X) vector to world space
    //            angleAccumulator = (cameraTransform.forward * inputUpDown + cameraTransform.right * inputLeftRight).normalized;
    //            // The Angle we are seeking
    //            targetEulerAngle = angleAccumulator * tiltAngle;
    //        }
    //    }
    //    else
    //    {
    //        startEulerAngle += angleAccumulator;
    //        transform.Rotate(angleAccumulator, Space.World);
    //    }

    //    if (Vector3.Distance(startEulerAngle, targetEulerAngle) <= tiltAngle && Vector3.Distance(targetEulerAngle, startEulerAngle) >= tiltAngle)
    //    {
    //        rotating = false;
    //        startEulerAngle = targetEulerAngle;
    //        angleAccumulator = targetEulerAngle;
    //    }

    //}
}
