using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float speed;
    public float visibilityRange;
    public CharacterController characterController;

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
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

    void chase()
    {
        transform.LookAt(playerTransform.position);
        characterController.SimpleMove(transform.forward * speed);
    }
}
