using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class works together with a transparent diffuse material
public class UnRenderMe : MonoBehaviour
{
    Color startColor;

    void Start()
    {
        // Stores the initial color
        startColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // get scalar to check angle between object's local x-axis (right) and world's z-axis (forward)
        float dot = Vector3.Dot(new Vector3(0, 0, 1), transform.right);
        if (dot > 0.5)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, 0.1f + (1 - dot));
            //GetComponent<MeshRenderer>().enabled = false;
        }
        else
            gameObject.GetComponent<Renderer>().material.color = startColor;
    }
}
