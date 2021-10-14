using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSystem : MonoBehaviour
{
    //Used to respond to input events
    public Controller controller;
    //Base used to determine system origin
    public GameObject baseSphere;
    //orbitlas of the base
    public List<OrbitalSphere> orbitingSpheres;
    public float speed = 1f;
    public bool flipDirection = false;
    
    //System origin
    private Vector3 baseSphereCenter;
    [SerializeField]
    private float angle = 0f;
    
    void Start()
    {
        baseSphereCenter = baseSphere.transform.position;

        //Determine orbit radius for each orbital
        foreach(OrbitalSphere sphere in orbitingSpheres)
        {
            sphere.SetOrbitRadius(baseSphereCenter);
        }
        //Subscribe to input click events for color swaps
        if(controller != null)
        {
            controller.PrimaryMouseButtonClickedEvent.AddListener(SwapColorsAndTextures);
        }
        
    }

    //Determine if an object was clicked by the mouse
    private bool DidMouseClickObject(Vector3 mousePos)
    {
        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        return Physics.Raycast(ray,out hit,500.0f); 
    }

    //Move each orbital to its next position
    private void AdjustOrbits(float angle)
    {
        foreach(OrbitalSphere sphere in  orbitingSpheres)
        {
            sphere.AdjustOrbit(angle);
        }
    }

    //Change each orbitals color and texture
    private void SwapColorsAndTextures(Vector3 mousePos)
    {
        if(!DidMouseClickObject(mousePos))
        {
            foreach(OrbitalSphere sphere in  orbitingSpheres)
            {
                sphere.SwapColorAndTexture();
            }
        }
        
    }

    void Update()
    {
        //Reverse direction if selected
        if(flipDirection)
        {
            angle = angle -Time.deltaTime * speed;
        }
        else{
            angle = angle + Time.deltaTime * speed;
        }

        //Keep angle between 0 and 2PI radians
        angle = angle  % (2.0f * Mathf.PI);
        AdjustOrbits(angle);    
        
    }
}
