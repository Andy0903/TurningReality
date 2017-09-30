using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GlowPrePass : MonoBehaviour
{
    private static RenderTexture myPreePass;
    private static RenderTexture myBlurred;

    private Material myBlurMaterial;

    private void OnEnable()
    {
        myPreePass = new RenderTexture(Screen.width, Screen.height, 24);
        myPreePass.antiAliasing = QualitySettings.antiAliasing;
        myBlurred = new RenderTexture(Screen.width / 2, Screen.height / 2, 0);

        Camera camera = GetComponent<Camera>();
        Shader glowShader = Shader.Find("Hidden/GlowReplace");
        camera.targetTexture = myPreePass;
        camera.SetReplacementShader(glowShader, "Glowable");
        Shader.SetGlobalTexture("_GlowPrePassTex", myPreePass);

        Shader.SetGlobalTexture("_GlowBlurredTex", myBlurred);

        myBlurMaterial = new Material(Shader.Find("Hidden/Blur"));
        myBlurMaterial.SetVector("_BlurSize", new Vector2(myBlurred.texelSize.x * 1.5f, myBlurred.texelSize.y * 1.5f));
    }

    private void OnRenderImage(RenderTexture aSource, RenderTexture aDestination)
    {
        Graphics.Blit(aSource, aDestination);

        Graphics.SetRenderTarget(myBlurred);
        GL.Clear(false, true, Color.clear);

        Graphics.Blit(aSource, myBlurred);

        for (int i = 0; i < 4; i++)
        {
            RenderTexture temp = RenderTexture.GetTemporary(myBlurred.width, myBlurred.height);
            Graphics.Blit(myBlurred, temp, myBlurMaterial, 0);
            Graphics.Blit(temp, myBlurred, myBlurMaterial, 1);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
