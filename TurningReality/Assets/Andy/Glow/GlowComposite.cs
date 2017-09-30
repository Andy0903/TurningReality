using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowComposite : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField]
    float myIntensity = 2;

    private Material myCompositeMaterial;

    private void OnEnable()
    {
        myCompositeMaterial = new Material(Shader.Find("Hidden/GlowComposite"));
    }

    private void OnRenderImage(RenderTexture aSource, RenderTexture aDestination)
    {
        myCompositeMaterial.SetFloat("_Intensity", myIntensity);
        Graphics.Blit(aSource, myCompositeMaterial, 0);
    }
}
