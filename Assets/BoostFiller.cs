using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFiller : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHolder player = GameObject.FindWithTag("Player").GetComponent<PlayerHolder>();
            player.boostCurrent = player.boostMax;
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
