using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PowerUpScript : MonoBehaviour
{
    //public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter controller;
    ThirdPersonCharacter tpc;
    public AudioClip soundFX;
    GameObject spellParticles;
    Collider col;
    // MeshRenderer rend;
    ParticleSystem[] particleSystems;
    float timer;
    float pickupTime;

    void Start()
    {
        tpc = GameObject.FindObjectOfType<ThirdPersonCharacter>();
        col = GetComponent<Collider>();
        //rend = GetComponent<MeshRenderer>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //tpc.m_JumpForce = 20f;
            GetComponent<AudioSource>().PlayOneShot(soundFX);
            pickupTime = Time.realtimeSinceStartup;
            FlipStates();
            tpc.JumpPowerMultiplier = 2;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= pickupTime + 10 && col.enabled == false)
        {
            FlipStates();
        }
    }

    void FlipStates()
    {
        col.enabled = !col.enabled;
        //rend.enabled = !rend.enabled;
        foreach (ParticleSystem pSys in particleSystems)
        {
            if (pSys.isPlaying)
            {
                pSys.Stop();
            }
            if (pSys.isStopped)
            {
                pSys.Play();
            }
        }
    }
}
