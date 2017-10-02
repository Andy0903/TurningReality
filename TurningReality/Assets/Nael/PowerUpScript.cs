using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PowerUpScript : MonoBehaviour
{
    //public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter controller;
    ThirdPersonCharacter tpc = GameObject.FindObjectOfType<ThirdPersonCharacter>();
    public AudioClip soundFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //tpc.m_JumpForce = 20f;
            GetComponent<AudioSource>().PlayOneShot(soundFX);
        }
    }
}
