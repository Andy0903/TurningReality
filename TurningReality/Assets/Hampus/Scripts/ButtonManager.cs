using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    GameObject[] buttons;
    RotateOnTrigger currentTrig, prevTrig;

	// Use this for initialization
	void Start () {
        buttons = GameObject.FindGameObjectsWithTag("Buttons");

    }
	
	// Update is called once per frame
	void Update () {

	}
}
