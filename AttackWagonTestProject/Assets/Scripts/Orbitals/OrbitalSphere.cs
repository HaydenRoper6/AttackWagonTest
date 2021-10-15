using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OrbitalSphere : MonoBehaviour
{
    private float orbitRadius = 0.0f;
    public Texture texture1;
    public Texture texture2;
    private Vector3 basePosition;
    
    private void Start() {
        //initially set the color/texture
        SwapColorAndTexture();
    }

    //determine orbit radius 
    //determined by starting sphere's center's world distance from base sphere's center
    public void SetOrbitRadius(Vector3 baseSpherePosition)
    {
        orbitRadius = Vector3.Distance(baseSpherePosition, transform.position);
        basePosition = baseSpherePosition;
    }

    //Change the color and texture to something random
    public void SwapColorAndTexture()
    {
        //Random color
        Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        gameObject.GetComponent<Renderer>().material.color = randomColor;
        
        //Swap Textures
        if(Random.Range(0f,1f) > 0.5f)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = texture1;
        }
        else{
            gameObject.GetComponent<Renderer>().material.mainTexture = texture2;
        }
    }

    //Adjusts the sphere's position within the orbital system based off circular trig functions
    public void AdjustOrbit(float angle){
        
        //this could be expande to dynamically determine which functions should adjust x/y/z based on starting position relative to base sphere
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        float z = 0;
        Vector3 orbitPath = new Vector3(x,y,z) + basePosition;

        //keep orbit radius consistent
        transform.position = orbitPath * orbitRadius;
    }

}
