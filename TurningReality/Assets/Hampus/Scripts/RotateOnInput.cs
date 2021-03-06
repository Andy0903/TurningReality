﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class RotateOnInput : MonoBehaviour
{
    //private Vector3 targetAngle;
    //private Vector3 currentAngle;
    //private Vector3 previousTargetAngle;

    //private float tiltAngle = 90;
    //private bool rotating = false;

    //public void Start()
    //{
    //    currentAngle = transform.eulerAngles;
    //    targetAngle = currentAngle;
    //}

    //public void Update()
    //{
    //    if (!rotating)
    //    {
    //        float inputLeftRight = Input.GetAxis("RotY"),
    //            inputUpDown = Input.GetAxis("RotX");

    //        if (inputUpDown != 0 || inputLeftRight != 0)
    //        {
    //            Vector3 axis = Vector3.zero;
    //            // We want to rotate the object to the left or right, and we care about which vector points forward (objects Z or Y)
    //            if (inputUpDown != 0 && inputLeftRight == 0)
    //            {
    //                axis = inputUpDown * new Vector3(0, 0, 1);
    //            }
    //            // We want to rotate the object to the up or down, and we care about which vector points right (objects Y or X)
    //            else if (inputUpDown == 0 && inputLeftRight != 0)
    //            {
    //                axis = inputLeftRight * new Vector3(1, 0, 0);
    //            }

    //            axis.Normalize();
    //            rotating = true;
    //            targetAngle += axis * tiltAngle;
    //        }
    //    }

    //    currentAngle = Vector3.Lerp(currentAngle, targetAngle, Time.deltaTime * 2);

    //    if (Vector3.Distance(currentAngle, targetAngle) <= 2)
    //    {
    //        //Snaps to target angle - remove for complete smoothness or decrease value check in bool
    //        currentAngle = targetAngle;
    //        rotating = false;
    //    }

    //    transform.eulerAngles = currentAngle;
    //    print(transform.eulerAngles + " " + targetAngle);
    //}


    private Transform cameraTransform;
    private Vector3 startAngle, currentAngle, targetAngle;
    private float tiltAngle = 90;
    private bool rotating = false;
    GameObject worldCamera;
    GameObject mainCamera;
    private GameObject player, player2;

    public void Start()
    {
        cameraTransform = Camera.main.transform;
        startAngle = transform.eulerAngles;
        currentAngle = startAngle;
        targetAngle = startAngle;

        worldCamera = GameObject.FindGameObjectWithTag("WorldCamera");
        mainCamera = Camera.main.gameObject;

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        player2 = GameObject.FindGameObjectsWithTag("Player")[1];

    }

    private void SwitchToWorldCamera()
    {
        if (worldCamera.GetComponent<Camera>().enabled == true) return;

        mainCamera.GetComponent<Camera>().enabled = false;
        worldCamera.GetComponent<Camera>().enabled = true;
    }

    private void SwitchToMainCamera()
    {
        if (mainCamera.GetComponent<Camera>().enabled == true) return;

        mainCamera.GetComponent<Camera>().enabled = true;
        worldCamera.GetComponent<Camera>().enabled = false;
    }

    public void Update()
    {
        if (!rotating)
        {
            SwitchToMainCamera();

            float inputLeftRight = 0;
            float inputUpDown = 0;

            // Used to check whether the rotation is positive or negative           
            if (player.GetComponent<ThirdPersonUserControl>().PlayerControls == PlayerSlot.KeyboardDebug)
            {
                inputLeftRight = Input.GetAxis("RotY");
                inputUpDown = Input.GetAxis("RotX");

                if (inputLeftRight == 0 && inputUpDown == 0)
                {
                    inputLeftRight = player2.GetComponent<DpadManager>().CountY;
                    inputUpDown = player2.GetComponent<DpadManager>().CountX;
                }

                // If register input start rotation
                if (inputLeftRight != 0)
                {
                    currentAngle = SetRotationToInput(Vector3.right, inputLeftRight);
                }
                else if (inputUpDown != 0)
                {
                    currentAngle = SetRotationToInput(Vector3.forward, inputUpDown);
                }

                targetAngle = currentAngle * tiltAngle;
            }
            else
            {
                inputLeftRight = player.GetComponent<DpadManager>().CountY;
                inputUpDown = player.GetComponent<DpadManager>().CountX;

                if (inputLeftRight == 0 && inputUpDown == 0)
                {
                    inputLeftRight = player2.GetComponent<DpadManager>().CountY;
                    inputUpDown = player2.GetComponent<DpadManager>().CountX;
                }

                // If register input start rotation
                if (inputLeftRight != 0)
                {
                    currentAngle = SetRotationToInput(Vector3.right, -inputLeftRight);
                }
                else if (inputUpDown != 0)
                {
                    currentAngle = SetRotationToInput(Vector3.forward, inputUpDown);
                }

                targetAngle = currentAngle * tiltAngle;
            }
            //float inputLeftRight = player.GetComponent<DpadManager>().CountY,
            //    inputUpDown = player.GetComponent<DpadManager>().CountX;

            //if (inputLeftRight == 0 && inputUpDown == 0)
            //{
            //    inputLeftRight = player2.GetComponent<DpadManager>().CountY;
            //    inputUpDown = player2.GetComponent<DpadManager>().CountX;
            //}

            //// If register input start rotation
            //if (inputLeftRight != 0)
            //{
            //    currentAngle = SetRotationToInput(Vector3.right, -inputLeftRight);
            //}
            //else if (inputUpDown != 0)
            //{
            //    currentAngle = SetRotationToInput(Vector3.forward, inputUpDown);
            //}

            //targetAngle = currentAngle * tiltAngle;
        }
        else
        {
            SwitchToWorldCamera();
            //currentAngle = Vector3.Lerp(currentAngle, Vector3.zero, Time.deltaTime);
            startAngle += currentAngle;
            transform.Rotate(currentAngle, Space.World);
        }

        if (Vector3.Distance(startAngle, targetAngle) <= tiltAngle && Vector3.Distance(targetAngle, startAngle) >= tiltAngle)
        {
            player.transform.eulerAngles = Vector3.zero;
            player2.transform.eulerAngles = Vector3.zero;
            rotating = false;
            startAngle = targetAngle;
        }

    }

    private Vector3 SetRotationToInput(Vector3 normal, float input)
    {
        Vector3 direction = Vector3.zero;
        rotating = true;
        direction = normal * input;
        direction.Normalize();
        return direction;
    }
}
