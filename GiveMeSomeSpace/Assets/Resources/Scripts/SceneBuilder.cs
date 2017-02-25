using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour {

    //public GameObject character1Prefab, character2Prefab, stagePrefab;
    private GameObject character1, character2, stage, character1Prefab, character2Prefab;
    public GameObject stagePrefab;

    // Use this for initialization
    void Awake () {

        character1Prefab = (GameObject)Resources.Load(PlayerPrefs.GetString("P1_character"));
        character1 = Instantiate(character1Prefab);
        character1.transform.position = new Vector3(12, 1.5F, 0);

        character2Prefab = (GameObject)Resources.Load(PlayerPrefs.GetString("P2_character"));
        character2 = Instantiate(character2Prefab);
        character2.transform.position = new Vector3(-12, 1.5F, 0);
        //character2.transform.Rotate(new Vector3(0, 180, 0));

        character1.SetActive(true);
        character2.SetActive(true);

        stage = Instantiate(stagePrefab);
        stage.transform.position = new Vector3(0, 0, 0);

        if (PlayerPrefs.GetString("P1_character").Equals("Prefabs/ThomasSpaceEngine_BR"))
        {
            character1.transform.Rotate(new Vector3(180, 0, 0));
        }
        else if (PlayerPrefs.GetString("P1_character").Equals("Prefabs/Peekachew_BR"))
        {
            character1.transform.Rotate(new Vector3(0, 0, 180));
        }
        else if (PlayerPrefs.GetString("P1_character").Equals("Prefabs/CursedBlueper_BR"))
        {
            character1.transform.Rotate(new Vector3(0, -90, 0));
        }

        if (PlayerPrefs.GetString("P2_character").Equals("Prefabs/ThomasSpaceEngine_BR"))
        {
            character2.transform.Rotate(new Vector3(0, 0, 0));
            character2.transform.Rotate(new Vector3(0, 0, 0));
        }
        else if (PlayerPrefs.GetString("P2_character").Equals("Prefabs/Peekachew_BR"))
        {
            //character1.transform.Rotate(new Vector3(180, 0, 0));
        }
        else if (PlayerPrefs.GetString("P2_character").Equals("Prefabs/CursedBlueper_BR"))
        {
            character2.transform.Rotate(new Vector3(0, 90, 0));
        }

        character1.name = "character1";
        character2.name = "character2";
        stage.name = "stage";

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
