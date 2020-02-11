using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    public Transform playerModelTransform;

    public Transform bulletSpawnTransform;
    public Transform bulletTransform;

    Rigidbody playerHolderRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerHolderRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        freezeRotation();
        doRotation();
        doMovement();
    }

    // Prevent player from falling over when hitting a wall
    void freezeRotation()
    {
        playerHolderRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        playerHolderRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    void doRotation()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion playerRotation = Quaternion.LookRotation(targetPoint - transform.position);
            playerRotation.x = 0.0f;
            playerRotation.z = 0.0f;
            playerModelTransform.rotation = Quaternion.Slerp(playerModelTransform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void doMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = 0.0f;
        float z = Input.GetAxis("Vertical");

        Vector3 Movement = new Vector3(x, y, z);

        transform.position += Movement * movementSpeed * Time.deltaTime;
    }
}
