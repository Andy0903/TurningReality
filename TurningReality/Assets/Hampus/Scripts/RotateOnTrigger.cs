using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTrigger : MonoBehaviour
{
    private Vector3 startAngle, currentAngle, targetAngle;
    private float tiltAngle = 90;
    private bool rotating = false;
    private Transform worldTrans;

    private List<GameObject> objectsToCorrectRotation = new List<GameObject>();
    private float currentCoolDown = 5, startCoolDown = 5;
    private bool isActivated = false, isCooledDown = true;

    public Vector3 Axis;
    public float Direction;

    private void OnTriggerEnter(Collider other)
    {
        if (/*Input.GetAxis("Jump") > 0 &&*/ !isActivated && currentCoolDown == startCoolDown)
        {
            foreach (GameObject obj in objectsToCorrectRotation)
            {
                if(other == obj.GetComponent<Collider>())
                {
                    isActivated = true;
                }
            }
        }
    }

    public void Start()
    {
        worldTrans = GameObject.FindGameObjectWithTag("WorldOrigin").transform;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            objectsToCorrectRotation.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }

        startAngle = worldTrans.eulerAngles;
        currentAngle = startAngle;
        targetAngle = startAngle;
    }

    public void Update()
    {
        if (!rotating)
        {
            rotating = true;
            currentAngle = SetRotationToInput(Axis, Direction);
            targetAngle = currentAngle * tiltAngle;
        }
        else if (isActivated)
        {
            //currentAngle = Vector3.Lerp(currentAngle, Vector3.zero, Time.deltaTime);
            startAngle += currentAngle;
            worldTrans.Rotate(currentAngle, Space.World);

            if (Vector3.Distance(startAngle, targetAngle) <= 1)
            {
                foreach (GameObject obj in objectsToCorrectRotation)
                {
                    obj.transform.eulerAngles = Vector3.zero;
                }

                rotating = false;
                startAngle = targetAngle;
                isActivated = false;
            }
        }
        CoolDown();
    }

    private void CoolDown()
    {
        if (!isCooledDown)
        {
            currentCoolDown -= Time.deltaTime;
            if (currentCoolDown <= 0)
            {
                isCooledDown = true;
                currentCoolDown = startCoolDown;
            }
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
