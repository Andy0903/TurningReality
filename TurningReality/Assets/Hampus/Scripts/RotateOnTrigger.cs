using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTrigger : MonoBehaviour
{
    private Vector3 startAngle, currentAngle, targetAngle;
    private float tiltAngle = 90;
    private bool rotating = false;
    private Transform worldTrans;

    private GameObject player, player2;
    private float coolDown = 5, startCoolDownTime = 5;

    public Vector3 Axis;
    public float Direction;
    public bool isActivated = false, isCooledDown = true;

    private void OnTriggerEnter(Collider other)
    {
        if (/*Input.GetAxis("Jump") > 0 &&*/ !isActivated && isCooledDown)
        {
            if (other == player.GetComponent<Collider>() || other == player2.GetComponent<Collider>())
            {
                isActivated = true;
            }
        }
    }

    public void Start()
    {
        worldTrans = GameObject.FindGameObjectWithTag("WorldOrigin").transform;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        player2 = GameObject.FindGameObjectsWithTag("Player")[1];
        startAngle = worldTrans.eulerAngles;
        currentAngle = startAngle;
        targetAngle = startAngle;
    }

    public void Update()
    {
        if (!rotating)
        {
            currentAngle = SetRotationToInput(Axis, Direction);
            targetAngle = currentAngle * tiltAngle;
        }
        else if (isActivated)
        {
            //currentAngle = Vector3.Lerp(currentAngle, Vector3.zero, Time.deltaTime);
            startAngle += currentAngle;
            worldTrans.Rotate(currentAngle, Space.World);
        }

        if (Vector3.Distance(startAngle, targetAngle) <= tiltAngle && Vector3.Distance(targetAngle, startAngle) >= tiltAngle)
        {
            player.transform.eulerAngles = Vector3.zero;
            player2.transform.eulerAngles = Vector3.zero;
            rotating = false;
            isActivated = false;
            startAngle = targetAngle;
        }

        if (!isCooledDown)
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                isCooledDown = true;
                coolDown = startCoolDownTime;
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
