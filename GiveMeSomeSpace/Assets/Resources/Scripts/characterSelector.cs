using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterSelector : MonoBehaviour {

    public Button P1_C1, P1_C2, P1_C3, P2_C1, P2_C2, P2_C3;

    // Use this for initialization
    void Start () {

        GameObject[] characters = new GameObject[3];
        characters[0] = Resources.Load<GameObject>("Prefabs/Thomas_The_WEED_ENGINE");
        characters[1] = Resources.Load<GameObject>("Prefabs/Thomas_The_WEED_ENGINE");
        characters[2] = Resources.Load<GameObject>("Prefabs/Thomas_The_WEED_ENGINE");

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
