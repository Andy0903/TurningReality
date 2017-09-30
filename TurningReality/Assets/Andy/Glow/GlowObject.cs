using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObject : MonoBehaviour
{
    [SerializeField]
    Color myGlowColor;
    [SerializeField]
    float myLerpFactor = 10f;

    private List<Material> myMaterials = new List<Material>();
    private Color myCurrentColor;
    private Color myTargetColor;

    public bool IsClickable { get; private set; }

    private void Awake()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            myMaterials.AddRange(renderer.materials);
        }
    }

    private void OnMouseEnter()
    {
        myTargetColor = myGlowColor;
        IsClickable = true;
    }

    private void OnMouseExit()
    {
        myTargetColor = Color.black;
        IsClickable = false;
    }

    private void OnDisable()
    {
        IsClickable = false;
        myCurrentColor = Color.black;
        myTargetColor = Color.black;
    }

    private void Update()
    {
        myCurrentColor = Color.Lerp(myCurrentColor, myTargetColor, Time.deltaTime * myLerpFactor);

        for (int i = 0; i < myMaterials.Count; i++)
        {
            myMaterials[i].SetColor("_GlowColor", myCurrentColor);
        }
    }
}
