using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float speed = 10;
    private Rigidbody rig;

    // Use this for initialization
    void Start()
    {
        
        rig = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            rig.AddForce(Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rig.AddForce(Vector3.back * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rig.AddForce(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rig.AddForce(Vector3.forward* speed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.GetComponent<SphereCollider>().radius += 1;
        }

    }
}
