using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;

    const float G = 0.00667f;

    public static List<Gravity> gravityObjectList;

    //orbit
    [SerializeField] bool planet = false; //if not a planet => orbit
    [SerializeField] int orbitSpeed = 1000;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjectList == null) 
        { 
            gravityObjectList = new List<Gravity>();
        } 
        
        gravityObjectList.Add(this);

        //orbiting
        if (!planet)
        {
            rb.AddForce(Vector3.right * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            if (obj != this) 
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * otherRb.mass / Mathf.Pow(distance, 2));
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);

    }
}
