using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWorld : MonoBehaviour
{
    void Update()
    {
        float x = Input.GetAxis("RotX");
        float y = Input.GetAxis("RotY");
        float z = Input.GetAxis("RotZ");

        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime * 10);
    }
}
