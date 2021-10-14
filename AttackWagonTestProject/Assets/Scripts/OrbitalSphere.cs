using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OrbitalSphere : MonoBehaviour
{
    private float orbitRadius = 1.0f;
    private float angleRelativeToBaseSphere = 0.0f;
    private bool colorSwapped = false;
    public Texture texture1;
    public Texture texture2;
    
    private void Start() {
        SwapColor();
    }
    public void SetOrbitRadius(Vector3 baseSpherePosition)
    {
        orbitRadius = Vector3.Distance(baseSpherePosition, transform.position);
        //Find angle between starting position and base sphere
        Vector3 targetDir = baseSpherePosition - transform.position;
        angleRelativeToBaseSphere = Vector3.Angle(targetDir, transform.forward);
    }

    public void SwapColor(){
        
         Color randomColor = new Color(
            Random.Range(0f, 1f), //Red
            Random.Range(0f, 1f), //Green
            Random.Range(0f, 1f), //Blue
            1 //Alpha
        );
        gameObject.GetComponent<Renderer>().material.color = randomColor;
        
        if(Random.Range(0f,1f) > 0.5f)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = texture1;
        }
        else{
            gameObject.GetComponent<Renderer>().material.mainTexture = texture2;
        }
    }

    public void AdjustOrbit(float angle){
        
        //this could be expande to dynamically determine which functions should adjust x/y/z based on starting position relative to base sphere
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        float z = 0;
        Vector3 orbitPath = new Vector3(x,y,z);

        //keep orbit radius consistent
        transform.position = orbitPath * orbitRadius;

        //Rotate the object to align orbit path with original position relative base sphere
        //transform.rotation = Quaternion.Euler(angleRelativeToBaseSphere);

    }

}
