using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{
    public float rotationSpeed = 540f;
    public float speed = 10;

    private CharacterController controller;
    private Vector3 clickDestinationPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        doRotation();

        doMovement();
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
