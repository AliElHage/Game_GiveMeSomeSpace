using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Canvas openingScreen;
    public Canvas characterSelect;
    //public Canvas stageSelect;

    void Awake()
    {
        openingScreen.enabled = true;
        characterSelect.enabled = false;
        //stageSelect.enabled = false;
    }

    void OnGUI()
    {
        if (openingScreen.enabled)
        {
            if (Event.current.type == EventType.KeyDown)
            {
                openingScreen.enabled = false;
                characterSelect.enabled = true;
                //stageSelect.enabled = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                openingScreen.enabled = true;
                characterSelect.enabled = false;
                //stageSelect.enabled = false;
            }
        }
    }

}
