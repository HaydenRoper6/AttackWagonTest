using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BouncingSphere : MonoBehaviour
{
    public Controller controller;
    public GameObject groundPlane;
    public float thrust = 10f;
    private Vector3 groundPosition;
    private float radius;
    private Rigidbody physicsRB;
    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
        physicsRB = GetComponent<Rigidbody>();
        groundPosition = groundPlane.transform.position;
        transform.position = new Vector3(-0.5f,(radius+groundPosition.y),2f);
        controller.PrimaryMouseButtonClickedEvent.AddListener(BounceBall);
    }

    private void BounceBall(Vector3 mousePosition)
    {
        if(DidMouseHitMe(mousePosition))
        {
            //apply force to bounce ball
            physicsRB.AddForce(transform.up * thrust);
            //Distort ball on impact
            //transform.position = transform.position + new Vector3(0f,2f,0f);
        }

    }

    private bool DidMouseHitMe(Vector3 mousePosition)
    {
        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray,out hit,500.0f);
        if(hit.transform != null)
        {
            return hit.transform.gameObject == gameObject;
        }
        return false;
        
        //return  
    }

    void Update()
    {
        
    }
}
