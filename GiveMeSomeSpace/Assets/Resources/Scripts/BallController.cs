using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float acceleration = 10, rotationTorque = 5, rotationSpeed = 10,
		pushDuration = 0.5F, pushChargeTime = 1;
	public int playerNumber = 0;

    private Rigidbody rig;
    private Quaternion rotation;
    //private SphereCollider pushCol;
    private BoxCollider pushCol;
    private float xRotation, yRotation, zRotation;
    private KeyCode[] controls = new KeyCode[6];

	private bool pushing, recharging;
	private float pushTimer;

    

    // Use this for initialization
    void Start()
    {
        
        xRotation = this.transform.rotation.eulerAngles.x;
        yRotation = this.transform.rotation.eulerAngles.y;
        zRotation = this.transform.rotation.eulerAngles.z;
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
            transform.GetComponent<Animation>().Play();
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
		Vector3 movement = Vector3.zero;
		movement += new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        this.transform.rotation = Quaternion.LookRotation(Vector3.up, movement);
		rig.AddForce(movement.normalized * acceleration);
		
	}
    public bool isPushing()
    {
        return pushing;
    }
}
