using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMob : MonoBehaviour
{
    public float health = 100f;
    public int pointsToGive = 1;

    public float speed = 5f;
    public float visibilityRange = 1000f;

    CharacterController characterController;
    GameObject player;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkHealth();
        doMovement();
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {
        player.GetComponent<PlayerHolder>().points += pointsToGive;
        Destroy(this.gameObject);
    }

    void doMovement()
    {
        if (inRange())
        {
            chase();
        }
    }

    bool inRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) < visibilityRange;
    }

    bool inAir()
    {
        return transform.position.y > 1.5f;
    }

    void chase()
    {
        transform.LookAt(playerTransform.position);
        characterController.SimpleMove(transform.forward * speed);
    }
}
