using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour {

    //public GameObject character1Prefab, character2Prefab, stagePrefab;
    private GameObject character1, character2, stage, character1Prefab, character2Prefab;
    private int char1Score, char2Score, stageCounter;
    private bool fallDetected;
    private Rigidbody rig1, rig2;
    public GameObject stagePrefab;

    // Use this for initialization
    void Awake () {

        char1Score = 0;
        char2Score = 0;
        stageCounter = 0;

        fallDetected = false;

        character1Prefab = (GameObject)Resources.Load(PlayerPrefs.GetString("P1_character"));
        character1 = Instantiate(character1Prefab);
        character1.transform.position = new Vector3(12, 1.5F, 0);

        character2Prefab = (GameObject)Resources.Load(PlayerPrefs.GetString("P2_character"));
        character2 = Instantiate(character2Prefab);
        character2.transform.position = new Vector3(-12, 1.5F, 0);
        //character2.transform.Rotate(new Vector3(0, 180, 0));

        character1.SetActive(true);
        character2.SetActive(true);

        character1.transform.GetComponent<CharacterController>().vertical = "Vertical";
        character1.transform.GetComponent<CharacterController>().horizontal = "Horizontal";
        character1.transform.GetComponent<CharacterController>().pushButton = "Push";
        character1.transform.GetComponent<CharacterController>().playerNumber = 0;
        character2.transform.GetComponent<CharacterController>().vertical = "Vertical2";
        character2.transform.GetComponent<CharacterController>().horizontal = "Horizontal2";
        character2.transform.GetComponent<CharacterController>().pushButton = "Push2";
        character2.transform.GetComponent<CharacterController>().playerNumber = 1;

        stage = Instantiate(stagePrefab);
        stage.transform.GetChild(6).gameObject.SetActive(true);
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

        rig1 = character1.GetComponent<Rigidbody>();
        rig2 = character2.GetComponent<Rigidbody>();

        StartCoroutine(reorganize());

    }
	
    IEnumerator reorganize()
    {
        do
        {
            fallDetected = stage.transform.GetChild(6).GetComponent<FallDetection>().hasFallen;
            yield return null;
        } while (!fallDetected);
        Debug.Log("neg");
        char1Score = stage.transform.GetChild(6).GetComponent<FallDetection>().c1_score;
        char2Score = stage.transform.GetChild(6).GetComponent<FallDetection>().c2_score;

        // make sure both objects cannot move ///////////
        rig1.isKinematic = true;
        rig2.isKinematic = true;

        // delay to show new score //////////////////////
        yield return new WaitForSeconds(3F);

        // reposition players ///////////////////////////
        /*if (Mathf.Max(char1Score, char2Score) == 1)
        {
            character1.transform.position = new Vector3(11, 1.5F, 0);
            character2.transform.position = new Vector3(-11, 1.5F, 0);
            stageCounter++;
        }
        else if(Mathf.Max(char1Score, char2Score) == 2)
        {
            character1.transform.position = new Vector3(6, 1.5F, 0);
            character2.transform.position = new Vector3(-6, 1.5F, 0);
            stageCounter++;
        }*/
        stageCounter++;
        if (stageCounter < 2)
        {
            character1.transform.position = new Vector3(12, 1.5F, 0);
            character2.transform.position = new Vector3(-12, 1.5F, 0);
        }
        else if (stageCounter >= 2 && stageCounter < 4)
        {
            character1.transform.position = new Vector3(11, 1.5F, 0);
            character2.transform.position = new Vector3(-11, 1.5F, 0);
        }
        else if (stageCounter >= 4)
        {
            character1.transform.position = new Vector3(6, 1.5F, 0);
            character2.transform.position = new Vector3(-6, 1.5F, 0);
        }

        if (char1Score == 3 || char2Score == 3)
        {
            Debug.Log("PLS");
            if(char1Score == 3)
            {
                Debug.Log("PLS2");
                character1.transform.position = new Vector3(0, 1.5F, 0);
                character2.transform.position = new Vector3(0, -1.5F, 0);
                character1.transform.GetComponent<Rigidbody>().isKinematic = false;
                yield return new WaitForSeconds(0.5F);
                character1.transform.GetComponent<Rigidbody>().isKinematic = true;
                UnityEngine.Camera.main.transform.position = new Vector3(-3, 2, 0);
                UnityEngine.Camera.main.transform.LookAt(character1.transform);

            }
            else if(char2Score == 3)
            {
                character2.transform.position = new Vector3(0, 1.5F, 0);
                character1.transform.position = new Vector3(0, -1.5F, 0);
                character2.transform.GetComponent<Rigidbody>().isKinematic = false;
                yield return new WaitForSeconds(0.5F);
                character2.transform.GetComponent<Rigidbody>().isKinematic = true;
                UnityEngine.Camera.main.transform.position = new Vector3(-3, 2, 0);
                UnityEngine.Camera.main.transform.LookAt(character2.transform);
            }
            yield break;
        }
        // aesthetic delay //////////////////////////////
        yield return new WaitForSeconds(1F);

        // alter stage //////////////////////////////////
        //if(Mathf.Max(char1Score, char2Score) == 1 && stageCounter == 1)
        if(stageCounter == 2)
        {
            stage.transform.GetChild(2).GetComponent<MeshCollider>().enabled = true;
            stage.transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
            stage.transform.GetChild(1).GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(3F);
            stage.transform.GetChild(1).GetComponent<Rigidbody>().useGravity = false;
            stage.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        }
        //else if (Mathf.Max(char1Score, char2Score) == 2 && stageCounter < 4)
        else if (stageCounter == 4)
        {
            stage.transform.GetChild(4).GetComponent<MeshCollider>().enabled = true;
            stage.transform.GetChild(2).GetComponent<MeshCollider>().enabled = false;
            stage.transform.GetChild(3).GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(3F);
            stage.transform.GetChild(3).GetComponent<Rigidbody>().useGravity = false;
            stage.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = false;
        }

        StartCoroutine(UnityEngine.Camera.main.GetComponent<CameraController>().fightAnim());

        Debug.Log(char1Score + " " + char2Score);

        // re-grant movement ////////////////////////////
        rig1.isKinematic = false;
        rig2.isKinematic = false;

        // reset state of collision /////////////////////
        stage.transform.GetChild(6).GetComponent<FallDetection>().hasFallen = false;
        stage.transform.GetChild(6).GetComponent<FallDetection>().fallRank = 0;
        
        // call coroutine recursively ///////////////////
        StartCoroutine(reorganize());
    }

    IEnumerator setScoreVisible()
    {
        yield return new WaitForSeconds(3F);
        StartCoroutine(reorganize());
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
