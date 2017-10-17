using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DoubleJumpScript : MonoBehaviour
{
    ThirdPersonCharacter tpc1;
    ThirdPersonCharacter tpc2;
    public AudioClip soundFX;
    GameObject spellParticles;
    Collider col;
    ParticleSystem[] particleSystems;
    float timer;
    float pickupTime;
    float powerUpTime = 10;
    float powerUpEndsTime = 10;
    bool powerUpEnds = true;

    void Start()
    {
        tpc1 = GameObject.FindObjectsOfType<ThirdPersonCharacter>()[0];
        tpc2 = GameObject.FindObjectsOfType<ThirdPersonCharacter>()[1];
        col = GetComponent<Collider>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(soundFX);
            pickupTime = Time.realtimeSinceStartup;
            FlipStates();

            if (other.gameObject.GetInstanceID() == tpc1.gameObject.GetInstanceID())
            {
                tpc1.doubleJump = true;
            }
            else if (other.gameObject.GetInstanceID() == tpc2.gameObject.GetInstanceID())
            {
                tpc2.doubleJump = true;
            }
            
            powerUpEnds = false;
        }
    }
    void Update()
    {
       // timer += Time.deltaTime;

        if (Time.realtimeSinceStartup >= pickupTime + powerUpTime && col.enabled == false)
        {
            FlipStates();
        }
        if (Time.realtimeSinceStartup >= pickupTime + powerUpEndsTime && !powerUpEnds)
        {
            tpc1.doubleJump = false;
            tpc2.doubleJump = false;
            powerUpEnds = true;
        }
    }

    void FlipStates()
    {
        col.enabled = !col.enabled;
        foreach (ParticleSystem pSys in particleSystems)
        {
            if (pSys.isPlaying)
            {
                Debug.Log("stop");
                pSys.Stop();
            }
            else if (pSys.isStopped)
            {
                Debug.Log("play");
                pSys.Play();
            }
        }
    }
}
