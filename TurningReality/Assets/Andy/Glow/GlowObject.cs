using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObject : MonoBehaviour
{
    [SerializeField]
    Color glowColor;
    [SerializeField]
    float lerpFactor = 10f;

    private List<Material> materials = new List<Material>();
    private Color currentColor;
    private Color targetColor;
    
    private void Awake()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(renderer.materials);
        }
    }

    private void OnMouseEnter()
    {
        targetColor = glowColor;
    }

    private void OnMouseExit()
    {
        targetColor = Color.black;
    }

    private void OnDisable()
    {
        currentColor = Color.black;
        targetColor = Color.black;
    }

    private void Update()
    {
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpFactor);

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_GlowColor", currentColor);
        }
    }
}
