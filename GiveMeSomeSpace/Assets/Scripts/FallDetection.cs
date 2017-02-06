using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour {

    private bool fallDetected;

	// Use this for initialization
	void Start () {
        fallDetected = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if(this.GetComponent<MeshCollider>().isTrigger)
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("WORKING");
        fallDetected = true;
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(delayItemDestruction(other));
    }

    IEnumerator delayItemDestruction(Collider other)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(other.GetComponent<Collider>().transform.gameObject);
        fallDetected = false;
        Debug.Log("YEP.");
    }

    public bool fallTriggered
    {
        get { return fallDetected; }
        set { fallDetected = value; }
    }
}
