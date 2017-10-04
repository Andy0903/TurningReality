using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnRenderMe : MonoBehaviour
{
    Transform cameraTransform;
    Color startColor;

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        float dot = Vector3.Dot(new Vector3(0, 0, 1), transform.right);
        if (dot > 0.5)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, 0.3f + (1 - dot));
            //GetComponent<MeshRenderer>().enabled = false;
        }
        else
            gameObject.GetComponent<Renderer>().material.color = startColor;
    }
}
