using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingSphere : MonoBehaviour
{

    public GameObject groundPlane;
    private Vector3 groundPosition;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<Renderer>().bounds.extents.magnitude;
        groundPosition = groundPlane.transform.position;
        transform.position = groundPosition + new Vector3(0f,radius,0f);
    }
    void Update()
    {
        
    }
}
