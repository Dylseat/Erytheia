using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for put a shader on the camera screen
[ExecuteInEditMode]
public class ScreenShader : MonoBehaviour
{
    // Material with shader for camera screen
    public Material TransitionMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if(TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }
}
