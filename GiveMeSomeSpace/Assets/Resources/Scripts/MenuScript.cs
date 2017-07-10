using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {

    public UnityEngine.UI.Text audioToggleText, stageName, stageDescription;
    public UnityEngine.UI.Button fightButton, audioToggleButton, P1_C1, P2_C1, stageButton, stageButton2, stageButton3;
    public GameObject settingsHelper, menuHelper, characterSelectHelper, stageSelectHelper, characterSelector, preview, preview2, spotLight, door, doorKey, stageSpawner, doorContainer, neon1, neon2;
    public Light powerLight;
    public GameObject otherCam;
    private bool isMain, isSettings, isCharacterSelect, isStageSelect, isDone, alreadyClicked;
    private GameObject stage;
    private AudioSource audioMusic;
    private string buttonName, path;

    // Use this for initialization
    void Start () {
        setActiveScreen("main");
        StartCoroutine(goBack());
        isDone = false;
        alreadyClicked = false;
        audioMusic = UnityEngine.Camera.main.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void GoToSettings()
    {
        Debug.Log("Button is pressed");
        StartCoroutine(RotateScreen(Vector3.down * 91.55F, 0.8F, settingsHelper.transform));
        audioToggleButton.Select();
        setActiveScreen("settings");
    }

    public void GoToCharacterSelect()
    {
        StartCoroutine(RotateScreen(Vector3.up * 91.55F, 0.8F, characterSelectHelper.transform));
        setActiveScreen("character");
        characterSelector.transform.GetComponent<characterSelector>().enabled = true;
        P1_C1.Select();
        spotLight.SetActive(true);
        spotLight.transform.LookAt(preview.transform);
    }

    public void nextPlayerSelect()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName.Equals("P1_TDE"))
        {
            //path = "Prefabs/ThomasSpaceEngine_BR";
            path = "Prefabs/Character_TSE";
        }
        else if (buttonName.Equals("P1_Peekachew"))
        {
            path = "Prefabs/Character_Peekachew";
        }
        else if (buttonName.Equals("P1_CursedBlueper"))
        {
            path = "Prefabs/Character_Blueper";
        }
        PlayerPrefs.SetString("P1_character", path);

        spotLight.transform.LookAt(preview2.transform);
        P2_C1.Select();
    }

    public void goToStageSelect()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName.Equals("P2_TDE"))
        {
            //path = "Prefabs/ThomasSpaceEngine_BR";
            path = "Prefabs/Character_TSE";
        }
        else if (buttonName.Equals("P2_Peekachew"))
        {
            path = "Prefabs/Character_Peekachew";
        }
        else if (buttonName.Equals("P2_CursedBlueper"))
        {
            path = "Prefabs/Character_Blueper";
        }
        PlayerPrefs.SetString("P2_character", path);
        StartCoroutine(RotateScreen(Vector3.up * 91.55F, 0.8F, stageSelectHelper.transform));
        setActiveScreen("stage");
        stageButton.Select();
    }

    public void openDoor()
    {
        if (!alreadyClicked)
        {
            StartCoroutine(openDoorAnimation(Vector3.forward * 180, 1.5F));
            isDone = true;
        }
        alreadyClicked = true;
    }

    public void ToggleAudio()
    {

        if (UnityEngine.Camera.main.transform.GetComponent<AudioSource>().isPlaying)
        {
            UnityEngine.Camera.main.transform.GetComponent<AudioSource>().Stop();
            audioToggleText.text = "Audio: Off";
        }

        else
        {
            UnityEngine.Camera.main.transform.GetComponent<AudioSource>().Play();
            audioToggleText.text = "Audio: On";
        }
            
    }

    IEnumerator RotateScreen(Vector3 byAngles, float inTime, Transform helper)
    {
        var fromAngle = UnityEngine.Camera.main.transform.rotation;
        var toAngle = Quaternion.Euler(UnityEngine.Camera.main.transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            UnityEngine.Camera.main.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
        UnityEngine.Camera.main.transform.LookAt(helper);
    }

    IEnumerator goBack()
    {
        while (!isDone)
        {
            do
            {
                yield return null;
            } while (!Input.GetKeyDown("backspace"));
            if (isSettings)
            {
                StartCoroutine(RotateScreen(Vector3.up * 91.55F, 0.8F, menuHelper.transform));
                setActiveScreen("main");
                fightButton.Select();
            }
            else if (isCharacterSelect)
            {
                StartCoroutine(RotateScreen(Vector3.down * 91.55F, 0.8F, menuHelper.transform));
                setActiveScreen("main");
                fightButton.Select();
            }
            else if (isStageSelect)
            {
                StartCoroutine(RotateScreen(Vector3.down * 91.55F, 0.8F, characterSelectHelper.transform));
                setActiveScreen("character");
                P1_C1.Select();
            }
        }
        
    }

    void setActiveScreen(string activeScreen)
    {
        switch (activeScreen)
        {
            case "main":
                isMain = true;
                isSettings = false;
                isCharacterSelect = false;
                isStageSelect = false;
                break;
            case "settings":
                isMain = false;
                isSettings = true;
                isCharacterSelect = false;
                isStageSelect = false;
                break;
            case "character":
                isMain = false;
                isSettings = false;
                isCharacterSelect = true;
                isStageSelect = false;
                break;
            case "stage":
                isMain = false;
                isSettings = false;
                isCharacterSelect = false;
                isStageSelect = true;
                break;
        }
    }

    IEnumerator openDoorAnimation(Vector3 byAngles, float inTime)
    {
        var fromAngle = doorKey.transform.rotation;
        var toAngle = Quaternion.Euler(doorKey.transform.eulerAngles + byAngles);

        doorKey.GetComponent<Animation>().Play();

        powerLight.enabled = true;

        //for (var t = 0f; t < 1; t += Time.deltaTime / 0.8F)
        //{
       //     yield return null;
       // }

        Renderer renderer1 = neon1.GetComponent<Renderer>();
        Renderer renderer2 = neon2.GetComponent<Renderer>();
        Material mat1 = renderer1.material;
        Material mat2 = renderer2.material;

        for (var t = 0f; t < 1; t += Time.deltaTime / 2F)
        {
            //powerLight.intensity += t / 10F;

            float emission = Mathf.PingPong(Time.time / 2, 1.0f);
            //float emission = Mathf.MoveTowards(Time.time, 1F, Time.deltaTime);
            Color baseColor = Color.green; //Replace this with whatever you want for your base color at emission level '1'

            Color finalColor = baseColor + baseColor * Mathf.LinearToGammaSpace(emission);

            mat1.SetColor("_EmissionColor", finalColor);
            mat2.SetColor("_EmissionColor", finalColor);
            yield return null;
        }

        stage = Resources.Load<GameObject>("Prefabs/STAGE-1_TargetPlatform");
        stage.transform.GetChild(6).gameObject.SetActive(false);
        Instantiate(stage, stageSpawner.transform.position, stageSpawner.transform.rotation, stageSpawner.transform);

        for (var t = 0f; t < 1; t += Time.deltaTime / 0.5F)
        {
            yield return null;
        }

        StartCoroutine(clearScreen());

    }

    IEnumerator clearScreen()
    {
        for (var t = 0f; t < 1; t += Time.deltaTime / 0.4F)
        {
            stageButton.transform.Translate(Vector3.back * Time.deltaTime * 5);
            stageButton2.transform.Translate(Vector3.back * Time.deltaTime * 5);
            stageButton3.transform.Translate(Vector3.back * Time.deltaTime * 5);
            stageName.transform.Translate(Vector3.back * Time.deltaTime * 5);
            stageDescription.transform.Translate(Vector3.back * Time.deltaTime * 5);
            yield return null;
        }

        for (var t = 0f; t < 1; t += Time.deltaTime / 1F)
        {
            yield return null;
        }

        doorContainer.GetComponent<Animation>().Play();
        for (var t = 0f; t < 1; t += Time.deltaTime / 2.2F)
        {
            //door.transform.Translate(Vector3.forward * Time.deltaTime);
            yield return null;
        }

        UnityEngine.Camera.main.GetComponent<AudioListener>().enabled = false;
        UnityEngine.Camera.main.enabled = false;
        otherCam.SetActive(true);
        otherCam.GetComponent<Animation>().Play();

        StartCoroutine(findAudioAndFadeOut(14));

        for (var t = 0f; t < 1; t += Time.deltaTime / 10)
            {
                yield return null;
            }

        //SceneManager.LoadScene(1);
        
        AutoFade.LoadLevel(1, 3, 1, Color.white);
    }

    IEnumerator findAudioAndFadeOut(int secondsToFadeOut)
    {
        // Find Audio Music in scene
        if (UnityEngine.Camera.main == null) Debug.Log("HOLY SHIT");
        //AudioSource audioMusic = UnityEngine.Camera.main.GetComponent<AudioSource>();

        // Check Music Volume and Fade Out
        while (audioMusic.volume > 0.01f)
        {
            audioMusic.volume -= Time.deltaTime / secondsToFadeOut;
            yield return null;
        }

        // Make sure volume is set to 0
        audioMusic.volume = 0;

        // Stop Music
        audioMusic.Stop();

    }

}
