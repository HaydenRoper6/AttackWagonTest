using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CustomCameraController : MonoBehaviour
{

    public float sensitivity = 1f;
    public float zoomSpeed = 1f;
    public float fovMax = 90f;
    public float fovMin = 1f;

    [SerializeField]
    private float currentFov;
    [SerializeField]
    private float scrollValue = 0f;

    private Camera mainCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        currentFov = fovMax;
        UpdateCameraZoom();  
    }

    //Changes field of view by lerping between current fov and fov + mouse wheel change
    //There are other solutions where you physically move the camera but I chose to go with FOV manipulation 
    void UpdateCameraZoom(){
        currentFov = Mathf.Lerp(currentFov, currentFov + scrollValue,zoomSpeed);
        currentFov = Mathf.Clamp(currentFov, fovMin, fovMax);
        mainCamera.fieldOfView = currentFov;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y != scrollValue)
        {
            //The mouse has been scrolled, so update accordingly
            scrollValue = Input.mouseScrollDelta.y * sensitivity;
            UpdateCameraZoom();
        }       
    }

}
