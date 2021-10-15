using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BouncingSphere : MonoBehaviour
{
    public Controller controller;
    private AudioSource bounceNoiseEffect;
    private Animator animationController;
    public GameObject groundPlane;
    public float thrust = 10f;
    private Vector3 groundPosition;
    private float radius;
    private Rigidbody physicsRB;
    // Start is called before the first frame update
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
            //apply force to bounce ball
            physicsRB.AddForce(transform.up * thrust);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "groundSurface" && animationController.GetBool("InAir"))
        {
            bounceNoiseEffect.Play(0);
            //Play animation
            animationController.SetBool("InAir", false);
            StartCoroutine(PlayBounceAnimation());
              
        }
    }

    private IEnumerator PlayBounceAnimation(){
        yield return new WaitForSeconds(animationController.GetCurrentAnimatorStateInfo(0).length);
        animationController.SetBool("InAir", true); 
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
