using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform spawn;
    public float shotDistance = 2000f;

    public void shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, shotDistance))
        {
            shotDistance = hit.distance;
        }
        Debug.Log(ray);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);
    }
}
