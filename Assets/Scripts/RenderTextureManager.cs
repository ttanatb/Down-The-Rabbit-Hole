using UnityEngine;
using System.Collections;

public class RenderTextureManager : MonoBehaviour
{
    private RenderTexture renderTexture; //render texture to use
    public int cameraHeight; //resolution for verticle, horizontal will be calculated;
    public GameObject screen; //quad to display render texture on
    public Material textureMat; //material to add texture too
    public Camera textureCamera; //camera to look at the quad
    public Camera gameCamera; //camera to look at the game

    private float camAspect;
    private float oldCamAspect;

    void Start()
    {
        camAspect = textureCamera.aspect;
        oldCamAspect = camAspect;

        SetupTexture();
    }
    void Update()
    {
        //if the camera has changed aspect ratios, update the render texture
        camAspect = textureCamera.aspect;
        if (camAspect != oldCamAspect)
        {
            SetupTexture();
        }
        oldCamAspect = camAspect;
    }
    void SetupTexture()
    {
        float renderWidth = textureCamera.aspect * cameraHeight; // calculate the correct width
        renderTexture = new RenderTexture((int)renderWidth, cameraHeight, 24);//create the render texture
        renderTexture.name = "Programmatically created texture"; //name it for ease of use
        renderTexture.filterMode = FilterMode.Point; //set the filter mode for sharp pixels
        renderTexture.useMipMap = false;
        renderTexture.Create(); //create the texture
        gameCamera.targetTexture = renderTexture; //set the camera to render to texture
        textureMat.mainTexture = renderTexture; //put the texture in the material
        screen.transform.localScale = new Vector3(textureCamera.aspect * (textureCamera.orthographicSize * 2), textureCamera.orthographicSize * 2, 1);//resize the quad to fit the camera
    }
}