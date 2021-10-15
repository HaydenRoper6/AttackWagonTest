using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BouncingSphere : MonoBehaviour
{
    //Input controller
    public Controller controller;
    //Bounce noise
    private AudioSource bounceNoiseEffect;
    //Animation controller
    private Animator animationController;
    //Plane it will bounce on
    public GameObject groundPlane;
    //Force multiplier when clicking on ball
    public float thrust = 10f;
    
    private Vector3 groundPosition;
    private float radius;
    private Rigidbody physicsRB;
    private bool hasBallBeenHit = false;

    void Start()
    {
        animationController = GetComponent<Animator>();
        bounceNoiseEffect = GetComponent<AudioSource>();
        radius = GetComponent<SphereCollider>().radius;
        physicsRB = GetComponent<Rigidbody>();
        groundPosition = groundPlane.transform.position;
        transform.position = new Vector3(-0.5f,(radius+groundPosition.y),2f);
        controller.PrimaryMouseButtonClickedEvent.AddListener(BounceBall);
        animationController.SetBool("InAir", true); 
        
    }

    //If the ball is clicked, apply an upwards force on it
    private void BounceBall(Vector3 mousePosition)
    {
        if(DidMouseHitMe(mousePosition))
        {
            animationController.SetBool("InAir", true);
            hasBallBeenHit = true;
            //apply force to bounce ball
            physicsRB.AddForce(transform.up * thrust);
        }
    }

    //When ball makes contact with the ground from falling, animate the ball
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "groundSurface" && hasBallBeenHit)
        {
            bounceNoiseEffect.Play(0);
            animationController.SetBool("InAir", false);
            StartCoroutine(PlayBounceAnimation());   
        }
    }

    //play bounce animation then change state of animator back to in air
    private IEnumerator PlayBounceAnimation(){
        yield return new WaitForSeconds(animationController.GetCurrentAnimatorStateInfo(0).length);
        animationController.SetBool("InAir", true); 
    }

    //Use raycast to find target of mouse click
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
    }
}
