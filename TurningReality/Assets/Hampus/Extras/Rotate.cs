using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Vector3 targetAngle;
    private Vector3 currentAngle;
    private float tiltAngle = 90;

    public void Start()
    {
        currentAngle = transform.eulerAngles;
        targetAngle = currentAngle;
    }

    public void Update()
    {
        if (Vector3.Distance(currentAngle, targetAngle) <= 1)
        {
            Vector3 rotation = Vector3.zero;

            if (Input.GetAxis("RotX") != 0)
                rotation = (Input.GetAxis("RotX") * Camera.main.transform.forward);
            else if (Input.GetAxis("RotY") != 0)
                rotation = (Input.GetAxis("RotY") * Camera.main.transform.right);

            targetAngle += rotation * tiltAngle;
        }
        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

        transform.eulerAngles = currentAngle;
    }

}
