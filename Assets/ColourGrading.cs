using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGrading : MonoBehaviour
{
    public Material material; // material with shader on it
    //[SerializeField] RenderTexture renderTexture;

    private void Start()
    {
       // renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            // apply shader to rendertexture
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}

