using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Animator))]
public class BouncingSphere : MonoBehaviour
{
    public Controller controller;
    private Animator animationController;
    public GameObject groundPlane;
    public float thrust = 10f;
    private Vector3 groundPosition;
    private float radius;
    private Rigidbody physicsRB;
    private bool allowCollisionWithFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        animationController = GetComponent<Animator>();
        radius = GetComponent<SphereCollider>().radius;
        physicsRB = GetComponent<Rigidbody>();
        groundPosition = groundPlane.transform.position;
        transform.position = new Vector3(-0.5f,(radius+groundPosition.y),2f);
        controller.PrimaryMouseButtonClickedEvent.AddListener(BounceBall);
    }

    //If the ball is clicked, apply an upwards force on it
    private void BounceBall(Vector3 mousePosition)
    {
        if(DidMouseHitMe(mousePosition))
        {
            //apply force to bounce ball
            physicsRB.AddForce(transform.up * thrust);
            allowCollisionWithFloor = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "groundSurface" && allowCollisionWithFloor)
        {
            //Play sound
            //Play animation
            animationController.SetBool("InAir", false);
        
        }
        allowCollisionWithFloor = false;
    }

    private void DistortBall()
    {
        //transform.localScale = new Vector3(radius*2f,radius/2f,radius*2f);
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
