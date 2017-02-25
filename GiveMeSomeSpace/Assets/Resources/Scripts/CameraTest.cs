using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour {

    private BallController controlScript;
    private GameObject character;
    private int camShot, counter;
    private bool stopCutscene, cancelWait, endOfWait;

    void Awake()
    {
        counter = 0;
        stopCutscene = false;
        cancelWait = false;
        endOfWait = false;
        camShot = 0;
        character = GameObject.Find("Thomas_The_WEED_ENGINE");
        controlScript = character.GetComponent<BallController>();
        controlScript.enabled = false;
        StartCoroutine(camMovement2());
        StartCoroutine(Interrupt());
    }

	// Use this for initialization
	void Start () {
        //controlScript.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        cameraBehaviour(character.transform);
        Debug.Log(counter);
    }

    IEnumerator camMovement()
    {
        this.transform.position = new Vector3(2, 1, -10);
        yield return new WaitForSeconds(3.0f);

        this.transform.position = new Vector3(-2, 1, -10);
        yield return new WaitForSeconds(3.0f);

        this.transform.position = new Vector3(0, 1, -10);
        yield return new WaitForSeconds(3.0f);
        controlScript.enabled = true;
    }

    IEnumerator camMovement2()
    {
        // loop between camera shots
        IEnumerator wait = waitSeconds(8.0F);
        while (stopCutscene == false)
        {
            // define time frame for each camera shot
            switch (camShot)
            {
                case 0:
                    StartCoroutine(wait);
                    do
                    {
                        yield return null;
                    } while (!endOfWait);
                    //StopCoroutine(wait);
                    continue;
                case 1:
                    StartCoroutine(wait);
                    do
                    {
                        yield return null;
                    } while (!endOfWait);
                    //StopCoroutine(wait);
                    continue;
                case 2:
                    StartCoroutine(wait);
                    do
                    {
                        yield return null;
                    } while (!endOfWait);
                    StopCoroutine(wait);
                    continue;
                case 3:
                    StartCoroutine(wait);
                    do
                    {
                        yield return null;
                    } while (!endOfWait);
                    StopCoroutine(wait);
                    continue;
                default:
                    break;
            }
        }
        
    }

    IEnumerator Interrupt()
    {
        // if any key is pressed, break camera shots cycle.
        do
        {
            yield return null;
        } while (!Input.GetKeyDown("space"));
        stopCutscene = true;
    }

    void cameraBehaviour(Transform characterTransform)
    {
        if (camShot == 0)
        {
            this.transform.LookAt(characterTransform);
            this.transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (camShot == 1)
        {
            this.transform.LookAt(characterTransform);
            this.transform.Translate(Vector3.left * Time.deltaTime);
        }
        else if (camShot == 2)
        {
            this.transform.LookAt(characterTransform);
            this.transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if (camShot == 3)
        {
            this.transform.LookAt(characterTransform);
            this.transform.Translate(Vector3.down * Time.deltaTime);
        }

    }

    IEnumerator waitSeconds(float upperBound)
    {
        endOfWait = false;
        counter++;
        
        float timer = 0.0F;
        while(timer <= upperBound && !cancelWait)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            yield return null;
        }
        endOfWait = true;
        if (camShot == 3)
        {
            camShot = 0;
        }
        else
        {
            camShot++;
        }

        if (cancelWait)
        {
            cancelWait = false;
        }
    }
}
