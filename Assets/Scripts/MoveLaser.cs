using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaser : MonoBehaviour
{
    public float width = 0.5f;
    public float lineDrawSpeed = 6f;

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public GameObject originProjectile;
    public GameObject destinationEnemy;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = width;

    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(originProjectile.transform.position, destinationEnemy.transform.position);

        if (counter < dist)
        {
            counter += 0.1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = originProjectile.transform.position;
            Vector3 pointB = destinationEnemy.transform.position;

            Vector3 pointsAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRenderer.SetPosition(0, pointA);
            lineRenderer.SetPosition(1, pointsAlongLine);
        }
    }
}
