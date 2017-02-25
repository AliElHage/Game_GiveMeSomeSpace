using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Canvas mainCanvas;
    private Transform frontP, backP, leftP, rightP, centerOfRotation;

    void Awake()
    {
        mainCanvas.enabled = true;
        frontP = mainCanvas.transform.GetChild(0);
        rightP = mainCanvas.transform.GetChild(1);
        leftP = mainCanvas.transform.GetChild(2);
        backP = mainCanvas.transform.GetChild(3);
        centerOfRotation = mainCanvas.transform.GetChild(4);
    }

    void Update()
    {
        frontP.Translate(Vector3.right);
        frontP.LookAt(centerOfRotation);
        backP.Translate(Vector3.right);
        backP.LookAt(centerOfRotation);
        leftP.Translate(Vector3.right);
        leftP.LookAt(centerOfRotation);
        rightP.Translate(Vector3.right);
        rightP.LookAt(centerOfRotation);
        //rightP.RotateAround(new Vector3(0, 0, -315), Vector3.up, -90.0f * Time.deltaTime);
        //leftP.RotateAround(new Vector3(0, 0, -315), Vector3.up, -90.0f * Time.deltaTime);
        //backP.RotateAround(new Vector3(0, 0, -315), Vector3.up, -90.0f * Time.deltaTime);
    }

    void OnGUI()
    {
        /*if (openingScreen.enabled)
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
        }*/
    }

    void rotateCam(int screen)
    {
        if(screen == 1)
        {
            this.transform.Rotate(45, 0, 0);
        }
    }
}
