using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DpadManager : MonoBehaviour
{
    public int CountX { get; private set; }
    public int CountY { get; private set; }

    bool XInUse = false;
    bool YInUse = false;

    private void Start()
    {
        CountX = 0;
        CountY = 0;
    } 

    void Update()
    {
        if (Input.GetAxisRaw("DpadX") != 0)
        {
            if (XInUse == false)
            {
                if (Input.GetAxisRaw("DpadX") == +1)
                {
                    CountX = 1;
                }
                else if (Input.GetAxisRaw("DpadX") == -1)
                {
                    CountX = -1;
                }
                XInUse = true;
            }
        }
        if (Input.GetAxisRaw("DpadX") == 0)
        {
            XInUse = false;
            CountX = 0;
        }
        //----------------------------------------
        if (Input.GetAxisRaw("DpadY") != 0)
        {
            if (YInUse == false)
            {
                if (Input.GetAxisRaw("DpadY") == +1)
                {
                    CountY = 1;
                }
                else if (Input.GetAxisRaw("DpadY") == -1)
                {
                    CountY = -1;
                }
                YInUse = true;
            }
        }
        if (Input.GetAxisRaw("DpadY") == 0)
        {
            YInUse = false;
            CountY = 0;
        }
    }
}