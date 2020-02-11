using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float bulletSpeed = 7f;
    public float maxTimeInAir = 5f;
    public float damage = 10f;

    float currentTimeInAir = 0;
    GameObject triggeringEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        currentTimeInAir += 1.0f * Time.deltaTime;

        if(currentTimeInAir >= maxTimeInAir)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<SimpleMob>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
