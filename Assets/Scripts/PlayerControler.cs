using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{
    public float rotationSpeed = 540f;
    public float speed = 10;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Gun gun;

    private CharacterController controller;
    private Vector3 velocity;
    private bool grounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        doShoot();

        doJump();

        //doRotation();
        doMovement();
    }

    void doShoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("SHOOT");
            gun.shoot();
        }
    }

    void doRotation()
    {
        Vector3 rotationDirection = locateMousePosition();
        transform.LookAt(rotationDirection * rotationSpeed);
    }

    void doMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void doJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // h = 1/2g * t^2
    }


    Vector3 locateMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        return new Vector3(0f, 0f , 0f);
    }
}
