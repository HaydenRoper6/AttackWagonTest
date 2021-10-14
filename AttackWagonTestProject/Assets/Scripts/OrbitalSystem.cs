using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSystem : MonoBehaviour
{
    public Controller controller;
    public GameObject baseSphere;
    public List<OrbitalSphere> orbitingSpheres;
    public float speed = 1f;
    public bool flipDirection = false;

    private Vector3 baseSphereCenter;
    [SerializeField]
    private float angle = 0f;
    [SerializeField]
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        baseSphereCenter = baseSphere.transform.position;
        radius = Vector3.Distance(baseSphereCenter, orbitingSpheres[0].transform.position);

        foreach(OrbitalSphere sphere in orbitingSpheres)
        {
            sphere.SetOrbitRadius(baseSphereCenter);
        }
        if(controller != null)
        {
            controller.PrimaryMouseButtonClickedEvent.AddListener(SwapColors);
        }
        
    }

    private bool DidMouseClickObject()
    {
        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        return Physics.Raycast(ray,out hit,500.0f); 
    }

    private void AdjustOrbits(float angle)
    {
        foreach(OrbitalSphere sphere in  orbitingSpheres)
        {
            sphere.AdjustOrbit(angle);
        }
    }

    private void SwapColors()
    {
        if(!DidMouseClickObject())
        {
            foreach(OrbitalSphere sphere in  orbitingSpheres)
            {
                sphere.SwapColor();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if ( ! Physics.Raycast (ray,out hit,500.0f)) {
                SwapColors();
            }
        }
        */

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
