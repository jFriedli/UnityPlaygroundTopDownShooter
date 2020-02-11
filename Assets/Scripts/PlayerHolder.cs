using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    public int points = 0;

    public float attackDelay = 1.0f;

    public Transform playerModelTransform;

    public Transform bulletSpawnTransform;
    public GameObject bullet;

    GameManager gameManager;
    CharacterController controller;

    float lastAttacked;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameManager.gameHasEnded)
        {
            doRotation();
            doMovement();
        }

    }

    // Note: Can't do button detection in FixedUpdate
    private void Update()
    {
        if (!gameManager.gameHasEnded)
        {
            doShoot();
        }
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

        Vector3 movement = new Vector3(x, y, z);
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    void doShoot()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > lastAttacked + attackDelay)
        {
            Instantiate(bullet.transform, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
            lastAttacked = Time.time;
        }
    }
}
