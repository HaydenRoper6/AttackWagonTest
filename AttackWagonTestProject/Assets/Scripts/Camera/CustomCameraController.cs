using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CustomCameraController : MonoBehaviour
{
    //Used to respond to input events
    public Controller controller;
    //Used to affect lerp as you zoom
    public float zoomSpeed = 1f;

    //Min and max zoom values
    public float fovMax = 90f;
    public float fovMin = 1f;

    [SerializeField]
    private float currentFov;
    private Camera mainCamera;
    
    void Start()
    {
        //subscribe to scroll update events
        if(controller != null)
        {
            controller.MouseScrollUpdateEvent.AddListener(UpdateCameraZoom);
        }
        //initialize camera FOV
        mainCamera = GetComponent<Camera>();
        currentFov = fovMax;
        UpdateCameraZoom(0.0f);  
    }

    //Changes field of view by lerping between current fov and fov + mouse wheel change
    //There are other solutions where you physically move the camera but I chose to go with FOV manipulation 
    void UpdateCameraZoom(float scrollChange){
        zoomSpeed = Mathf.Clamp(zoomSpeed,0.2f,1f);
        currentFov = Mathf.Lerp(currentFov, currentFov + scrollChange,zoomSpeed);
        currentFov = Mathf.Clamp(currentFov, fovMin, fovMax);
        mainCamera.fieldOfView = currentFov;
    }

}
