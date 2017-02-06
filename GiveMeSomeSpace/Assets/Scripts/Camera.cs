using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.realtimeSinceStartup * 5) * 70 / 4, -100, 0);
    }
}
