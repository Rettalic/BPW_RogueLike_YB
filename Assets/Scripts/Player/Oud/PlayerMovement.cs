using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody rb;
    public Camera cam;
    public LayerMask aimLayerMask;
    [SerializeField] Animator animator;


    //Player Movement--------------------------------
    public float speed = 12f;
    Vector3 velocity;
    public float standardSpeed = 12f;


    //collision-------------------------------------
    bool bColliding;
    public Transform colliderCheck;
    public float radiusCollider = 0.4f;

    //dashing---------------------------------------
    public float dashSpeed;
    public float dashTime;


         
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // AimTowardsMouse();


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           // StartCoroutine(Dash());
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0f, z);

          if(movement.magnitude > 0) {
              movement.Normalize();
              movement *= speed * Time.deltaTime;
              transform.Translate(movement, Space.World);              
          }

        //  float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        //  float velocityX = Vector3.Dot(movement.normalized, transform.right);
          
        // animator.SetFloat("velocityZ", velocityZ, 0.1f, Time.deltaTime);
        // animator.SetFloat("velocityX", velocityX, 0.1f, Time.deltaTime);

        
        bColliding = Physics.Raycast(colliderCheck.position, transform.TransformDirection(new Vector3(x, 0, z)), radiusCollider);

       
        if (!bColliding)
        {
            transform.Translate(x * Time.deltaTime * speed, 0, z * Time.deltaTime * speed);
        } 


        //shoot detection
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("fire!");
        }

        

    }

    /*  void AimTowardsMouse()
      {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          if(Physics.Raycast(ray,out var hitInfo, Mathf.Infinity, aimLayerMask))
          {
              var playerDirection = hitInfo.point - transform.position;
              playerDirection.y = 0f;
              playerDirection.Normalize();
              transform.forward = playerDirection;
          }

      }   

      IEnumerator Dash()
      {
          float startTime = Time.time;

          while(Time.time < startTime + dashTime)
          {

          }
      }
      */
}