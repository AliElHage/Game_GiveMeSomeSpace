using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float acceleration = 10, rotationTorque = 5, rotationSpeed = 10,
        pushDuration = 0.5F, pushChargeTime = 1, maxVelocity = 10;
    public int playerNumber = 0;
    public string vertical = "", horizontal = "", pushButton = "";

    private Rigidbody rig;
    private Vector3 movement, turnVector, push;
    private Quaternion rotation;
    private float xRotation, yRotation, zRotation;
    private KeyCode[] controls = new KeyCode[6];

    private bool pushing, recharging;
    private float pushTimer;

    // Use this for initialization
    void Start()
    {
        push = Vector3.zero;
        rig = GetComponent<Rigidbody>();
        if(playerNumber == 0)
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.up, new Vector3(0, 0, 1));
        }
        else if(playerNumber == 1)
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.up, new Vector3(0, 0, -1));
        }
        
        pushTimer = 0;
        pushing = false;
        recharging = false;
        rig.maxAngularVelocity = rotationSpeed;
        //StartCoroutine(limitSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();

        if (Input.GetAxis(pushButton) != 0F && !pushing && !recharging)
        {
            pushing = true;
            pushTimer = 0;
            transform.GetChild(0).GetChild(0).GetComponent<Animation>().Play();
        }
        else if (pushing)
        {
            pushTimer += Time.deltaTime;

            if (pushTimer >= pushDuration)
            {
                pushing = false;
                //pushCol.enabled = false;
                recharging = true;
            }
        }
        else if (recharging)
        {
            pushTimer += Time.deltaTime;

            if (pushTimer >= pushChargeTime)
            {
                recharging = false;
            }
        }
    }

    void MovementInput()
    {
        turnVector = Vector3.zero;
        movement = Vector3.zero;
        turnVector = new Vector3(Input.GetAxis(horizontal), 0, -Input.GetAxis(vertical));
        movement += new Vector3(Input.GetAxis(vertical), 0, Input.GetAxis(horizontal));
        if (movement != Vector3.zero)
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.up, turnVector);
            rig.AddForce(movement.normalized * acceleration);
            push = movement.normalized;
        }
        if (rig.velocity.magnitude > maxVelocity)
        {
            rig.velocity = rig.velocity.normalized * maxVelocity;
        }

    }

    IEnumerator limitSpeed()
    {
        while (true)
        {
            if(rig.velocity.magnitude > maxVelocity)
            {
                rig.velocity = rig.velocity.normalized * maxVelocity;
            }
        }
    }

    public bool isPushing()
    {
        return pushing;
    }

    public Vector3 pushDir()
    {
        return push;
    }
}
