using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingSphere : MonoBehaviour
{
    public Controller controller;
    public GameObject groundPlane;
    private Vector3 groundPosition;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
        groundPosition = groundPlane.transform.position;
        transform.position = new Vector3(-0.5f,(radius+groundPosition.y),-0.5f);
        controller.PrimaryMouseButtonClickedEvent.AddListener(BounceBall);
    }

    private void BounceBall(Vector3 mousePosition)
    {
        if(DidMouseHitMe(mousePosition))
        {
            //apply force to bounce ball
            //Distort ball on impact
            transform.position = transform.position + new Vector3(0f,2f,0f);
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
