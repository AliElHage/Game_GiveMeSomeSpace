using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private CharacterController controlScript1, controlScript2;
    private GameObject character1, character2, stage;
    private int camShot, counter;
    private bool stopCutscene, cancelWait, endOfWait, canStart;
    private GameObject[] sceneObjects;
    public Canvas instructionCanvas, scoreCanvas, fightCanvas;

    void Awake()
    {
        // GUI /////////////
        scoreCanvas.enabled = false;
        ////////////////////

        counter = 0;
        stopCutscene = false;
        cancelWait = false;
        endOfWait = false;
        canStart = false;
        camShot = 0;
        character1 = GameObject.Find("character1");
        if (character1 == null) Debug.Log("Dumbo");
        character2 = GameObject.Find("character2");
        stage = GameObject.Find("stage");
        if (stage == null) Debug.Log("Dumbo");
        StartCoroutine(buffer(0.2F));
        
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stopCutscene == false && canStart)
        {
            cameraBehaviour();
        }
        //cameraBehaviour();
    }

    

    IEnumerator camMovement()
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
                    } while (timer < 5.0F && !stopCutscene);
                    timer = 0.0F;
                    camShot++;
                    continue;
                case 2:
                    this.transform.position = new Vector3(5, 10, -18);
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 4.0F && !stopCutscene);
                    timer = 0.0F;
                    camShot++;
                    continue;
                case 3:
                    this.transform.position = new Vector3(5, 10, -18);
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 3.0F && !stopCutscene);
                    timer = 0.0F;
                    camShot++;
                    continue;
                case 4:
                    this.transform.position = new Vector3(10, 3, -5);
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 5.0F && !stopCutscene);
                    timer = 0.0F;
                    camShot++;
                    continue;
                case 5:
                    this.transform.position = new Vector3(-5, 10, -18);
                    do
                    {
                        yield return null;
                        timer += Time.deltaTime;
                    } while (timer < 4.0F && !stopCutscene);
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
        } while (Input.GetAxisRaw("Push") < 0.5F && Input.GetAxisRaw("Push2") < 0.5F);
        this.GetComponent<AudioSource>().Stop();
        AudioClip song = Resources.Load<AudioClip>("Music/Circuit Theme - Mario Kart Double Dash!!");
        this.GetComponent<AudioSource>().clip = song;
        this.GetComponent<AudioSource>().Play();
        stopCutscene = true;
        instructionCanvas.enabled = false;
        scoreCanvas.enabled = true;
        camShot = 6;
        this.transform.position = new Vector3(0, 36, 0);
        this.transform.LookAt(new Vector3(0, 0, 0));
        this.transform.Rotate(new Vector3(0, 0, 90));
        controlScript1.enabled = true;
        controlScript2.enabled = true;
        StartCoroutine(fightAnim());
    }

    void cameraBehaviour()
    {
        Transform character1Transform = character1.transform;
        Transform character2Transform = character2.transform;
        Transform stageTransform = stage.transform;

        // stage left-rotating shot
        if (camShot == 0)
        {
            this.transform.LookAt(stageTransform);
            this.transform.Translate(Vector3.left * Time.deltaTime * 2);
        }
        // rotating close-up of character 1
        else if (camShot == 1)
        {
            this.transform.LookAt(character1Transform);
            this.transform.Translate(new Vector3(1, 0, -0.4F) * Time.deltaTime * 1.2F);
        }
        // downward translating shot of character 2
        else if (camShot == 2)
        {
            this.transform.LookAt(character2Transform);
            this.transform.Translate(Vector3.down * Time.deltaTime);
        }
        // stage right-rotating shot
        else if (camShot == 3)
        {
            this.transform.LookAt(stageTransform);
            this.transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
        // rotating close-up of character 2
        else if (camShot == 4)
        {
            this.transform.LookAt(character2Transform);
            this.transform.Translate(new Vector3(-1, 0, -0.4F) * Time.deltaTime * 1.2F);
        }
        // downward translating shot of character 1
        else if (camShot == 5)
        {
            this.transform.LookAt(character1Transform);
            this.transform.Translate(Vector3.down * Time.deltaTime);
        }

    }

    IEnumerator buffer(float inTime)
    {
        float time = 0F;
        do
        {
            time += Time.deltaTime / inTime;
            yield return null;
        } while (time < inTime);
        //character1.SetActive(true);
        //character2.SetActive(true);
        controlScript1 = character1.GetComponent<CharacterController>();
        controlScript2 = character2.GetComponent<CharacterController>();
        controlScript1.enabled = false;
        controlScript2.enabled = false;
        StartCoroutine(camMovement());
        StartCoroutine(Interrupt());
        canStart = true;
    }

    public IEnumerator fightAnim()
    {
        yield return new WaitForSeconds(0.5F);
        fightCanvas.GetComponent<Animation>().Play();
    }
}
