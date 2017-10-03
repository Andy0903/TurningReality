using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    private static PickUpManager instance = null;
    public static PickUpManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartSpawning(GameObject prefab, int waitTime)
    {
        StartCoroutine(Spawn(prefab, waitTime));
    }

    IEnumerator Spawn(GameObject prefab, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
