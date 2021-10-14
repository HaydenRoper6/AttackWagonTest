using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CustomCameraController : MonoBehaviour
{
    public Controller controller;
    public float zoomSpeed = 1f;
    public float fovMax = 90f;
    public float fovMin = 1f;

    [SerializeField]
    private float currentFov;
    private Camera mainCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        if(controller != null)
        {
            controller.MouseScrollUpdateEvent.AddListener(UpdateCameraZoom);
        }
        mainCamera = GetComponent<Camera>();
        currentFov = fovMax;
        UpdateCameraZoom(0.0f);  
    }

    //Changes field of view by lerping between current fov and fov + mouse wheel change
    //There are other solutions where you physically move the camera but I chose to go with FOV manipulation 
    void UpdateCameraZoom(float scrollChange){
        currentFov = Mathf.Lerp(currentFov, currentFov + scrollChange,zoomSpeed);
        currentFov = Mathf.Clamp(currentFov, fovMin, fovMax);
        mainCamera.fieldOfView = currentFov;
    }

}
