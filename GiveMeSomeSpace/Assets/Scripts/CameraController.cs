using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private BallController controlScript1, controlScript2;
    private GameObject character1, character2, stage;
    private int camShot, counter;
    private bool stopCutscene, cancelWait, endOfWait;

    void Awake()
    {
        counter = 0;
        stopCutscene = false;
        cancelWait = false;
        endOfWait = false;
        camShot = 0;
        character1 = GameObject.Find("Thomas_The_WEED_ENGINE");
        character2 = GameObject.Find("Thomas_The_WEED_ENGINE (1)");
        stage = GameObject.Find("STAGE-1_TargetPlatform");
        controlScript1 = character1.GetComponent<BallController>();
        controlScript2 = character2.GetComponent<BallController>();
        controlScript1.enabled = false;
        controlScript2.enabled = false;
        StartCoroutine(camMovement2());
        StartCoroutine(Interrupt());
    }

    // Use this for initialization
    void Start()
    {
        //controlScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        cameraBehaviour();
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
        controlScript1.enabled = true;
        controlScript2.enabled = true;
    }

    IEnumerator camMovement2()
    {
        float timer = 0.0F;
        // loop between camera shots
        while (stopCutscene == false)
        {
            // define time frame for each camera shot
            switch (camShot)
            {
                case 0:
                    this.transform.position = new Vector3(-5, 10, -18);
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 3.0F && !stopCutscene);
                    camShot++;
                    timer = 0.0F;
                    continue;
                case 1:
                    this.transform.position = new Vector3(-10, 3, -5);
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 0.1F && !stopCutscene);
                    timer = 0.0F;
                    camShot++;
                    continue;
                case 2:
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 4.0F && !stopCutscene);
                    timer = 0.0F;
                    camShot++;
                    continue;
                case 3:
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 8.0F && !stopCutscene);
                    timer = 0.0F;
                    camShot = 0;
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

    void cameraBehaviour()
    {
        Transform character1Transform = character1.transform;
        Transform character2Transform = character2.transform;
        Transform stageTransform = stage.transform;
        if (camShot == 0)
        {
            this.transform.LookAt(stageTransform);
            this.transform.Translate(Vector3.left * Time.deltaTime * 2);
        }
        else if (camShot == 1)
        {
            this.transform.LookAt(character1Transform);
            this.transform.Translate(new Vector3(1, -0.5F, 0) * Time.deltaTime * 5);
        }
        else if (camShot == 2)
        {
            this.transform.LookAt(character1Transform);
            this.transform.Translate(new Vector3(1, 0, -0.4F) * Time.deltaTime * 2);
        }
        else if (camShot == 3)
        {
            this.transform.LookAt(stageTransform);
            this.transform.Translate(Vector3.down * Time.deltaTime);
        }

    }

    void timeBuffer(float delaySeconds)
    {
        float timer = 0.0F;

    }
}
