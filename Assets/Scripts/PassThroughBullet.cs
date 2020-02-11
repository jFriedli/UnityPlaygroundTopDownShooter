using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughBullet : MonoBehaviour
{
    public float bulletSpeed = 7f;
    public float maxTimeInAir = 5f;
    public float damage = 10f;

    public float lineDrawSpeed = 6f;

    public GameObject lineRenderer;

    float currentTimeInAir = 0;

    List<GameObject> passedEnemies;
    List<GameObject> lasers;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        passedEnemies = new List<GameObject>();
        lasers = new List<GameObject>();
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerHolder>().projectileInAir = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        currentTimeInAir += 1.0f * Time.deltaTime;

        if (currentTimeInAir >= maxTimeInAir)
        {
            die();
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            die();
        }

        if (other.tag == "Enemy")
        {
            passedEnemies.Add(other.gameObject);

            Transform laser = Instantiate(lineRenderer.transform, transform.position, transform.rotation);

            MoveLaser moveLaser = laser.GetComponent<MoveLaser>();
            moveLaser.originProjectile = transform.gameObject;
            moveLaser.destinationEnemy = other.gameObject;

            lasers.Add(moveLaser.gameObject);
        }

        if(other.tag == "Player" && currentTimeInAir > 0.5)
        {
            foreach(GameObject enemy in passedEnemies)
            {
                enemy.GetComponent<SimpleMob>().health -= damage;
            }

            die();
        }
    }

    void die()
    {
        player.GetComponent<PlayerHolder>().projectileInAir = false;

        foreach (GameObject laser in lasers)
        {
            Destroy(laser);
        }

        Destroy(this.gameObject);
    }
}
