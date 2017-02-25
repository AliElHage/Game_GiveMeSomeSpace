using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

	public float pushStrength = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		print("RIPy)");
		if (other.gameObject.GetComponent<BallController>())
		{
			Vector3 pushDirection;
			pushDirection = (other.transform.position - transform.parent.position).normalized;

			other.gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * pushStrength, ForceMode.Impulse);
		}
	}
}
