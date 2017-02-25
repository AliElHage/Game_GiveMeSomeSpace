using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float acceleration = 10, rotationTorque = 5, rotationSpeed = 10,
		pushDuration = 0.5F, pushChargeTime = 1;
	public int playerNumber = 0;

    private Rigidbody rig;
    //private SphereCollider pushCol;
    private BoxCollider pushCol;

    private KeyCode[] controls = new KeyCode[6];

	private bool pushing, recharging;
	private float pushTimer;

    

    // Use this for initialization
    void Start()
    {
        //pushCol = transform.GetChild(1).GetComponent<SphereCollider>();
        //pushCol = transform.GetComponent<BoxCollider>();
        rig = GetComponent<Rigidbody>();

        //pushCol.enabled = false;
		pushTimer = 0;
		pushing = false;
		recharging = false;
        rig.maxAngularVelocity = rotationSpeed;

		if (playerNumber == 0)
		{
			controls[0] = KeyCode.W;
			controls[1] = KeyCode.S;
			controls[2] = KeyCode.D;
			controls[3] = KeyCode.A;
			controls[4] = KeyCode.E;
			controls[5] = KeyCode.Q;
		}

		if (playerNumber == 1)
		{
			controls[0] = KeyCode.I;
			controls[1] = KeyCode.K;
			controls[2] = KeyCode.L;
			controls[3] = KeyCode.J;
			controls[4] = KeyCode.O;
			controls[5] = KeyCode.U;
		}

	}

    // Update is called once per frame
    void Update()
    {
		MovementInput();
        
        if (Input.GetKey(KeyCode.Space) && !pushing && !recharging)
        {
			//pushCol.enabled = true;
			pushing = true;
			pushTimer = 0;
            //gameObject.GetComponent<SphereCollider>().radius += 1;
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
		Vector3 movement = Vector3.zero, rotation = Vector3.zero;

		movement += new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

		rig.AddForce(movement.normalized * acceleration);
		
	}
}
