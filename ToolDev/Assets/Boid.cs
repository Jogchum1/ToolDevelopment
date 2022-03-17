using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity { get; set; }

    public Vector3 position { get { return transform.position; } set { transform.position = value; } }

    public Quaternion rotation { get { return transform.rotation; } set { transform.rotation = value; } }

    Rigidbody rb;
    // public GameObject child;
}
