using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTrigger : MonoBehaviour {

    private bool triggered;
    private Vector3 push;
    private Rigidbody rig;

	// Use this for initialization
	void Start () {
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Stage"))
        {
            Debug.Log("COLLIDED!!!");
            triggered = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Stage"))
        {
            //Debug.Log("workin");
            if (this.transform.parent.GetComponentInParent<CharacterController>().isPushing())
            {
                rig = other.transform.GetComponentInParent<Rigidbody>();
                //push = this.transform.position;
                push = this.transform.parent.GetComponentInParent<CharacterController>().pushDir();
                Debug.Log(push);
                if (other.GetType() == typeof(BoxCollider))
                {
                    rig.AddForce(push * 1.5F, ForceMode.Impulse);
                    Debug.Log("BOX");
                }
                else if (other.GetType() == typeof(CapsuleCollider))
                {
                    rig.AddForce(push * 2F, ForceMode.Impulse);
                    Debug.Log("Capsule");
                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Stage"))
        {
            Debug.Log("DECOLLIDED!!!");
            triggered = false;
        }
    }
}

