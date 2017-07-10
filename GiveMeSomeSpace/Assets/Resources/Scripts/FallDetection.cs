using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallDetection : MonoBehaviour {

    private bool fallDetected, charHasFallen;
    private int counter = 0, char1Score = 0, char2Score = 0, firstFall = 0;
    private Sprite P1_full, P2_full;
    private Canvas scoreCanvas, fallCanvas;
    private Image P1_Score1, P1_Score2, P1_Score3, P2_Score1, P2_Score2, P2_Score3, exclam;

    // Use this for initialization
    void Start () {
        fallDetected = false;
        charHasFallen = false;
        scoreCanvas = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        fallCanvas = GameObject.Find("FallCanvas").GetComponent<Canvas>();
        P1_Score1 = scoreCanvas.transform.GetChild(2).GetComponent<Image>();
        P1_Score2 = scoreCanvas.transform.GetChild(3).GetComponent<Image>();
        P1_Score3 = scoreCanvas.transform.GetChild(4).GetComponent<Image>();
        P2_Score1 = scoreCanvas.transform.GetChild(5).GetComponent<Image>();
        P2_Score2 = scoreCanvas.transform.GetChild(6).GetComponent<Image>();
        P2_Score3 = scoreCanvas.transform.GetChild(7).GetComponent<Image>();
        exclam = fallCanvas.transform.GetChild(0).GetComponent<Image>();
        P1_full = Resources.Load<Sprite>("Textures/UI_Assets/GMSS_UI_P1_FullScore");
        P2_full = Resources.Load<Sprite>("Textures/UI_Assets/GMSS_UI_P2_FullScore");

        exclam.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(exclam == null)
        {
            Debug.Log("LELELEL");
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("WORKING");
        fallDetected = true;
        StartCoroutine(fallReaction());
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Collider"))
        {
            StartCoroutine(delayItemDestruction(other));
        }

    }

    IEnumerator delayItemDestruction(Collider other)
    {
        yield return new WaitForSeconds(3.0f);
        if (other.transform.parent.parent.GetComponent<Rigidbody>() == null) Debug.Log("FUCK");
        other.transform.parent.parent.GetComponent<Rigidbody>().isKinematic = true;
        // increment score /////////////
        if(firstFall == 0)
        {
            if (other.transform.parent.parent.name.Equals("character1")) char2Score++;
            else if (other.transform.parent.parent.name.Equals("character2")) char1Score++;
            firstFall++;

            if (char1Score == 1) P1_Score1.sprite = P1_full;
            else if (char1Score == 2) P1_Score2.sprite = P1_full;
            else if (char1Score == 3) P1_Score3.sprite = P1_full;

            if (char2Score == 1) P2_Score1.sprite = P2_full;
            else if (char2Score == 2) P2_Score2.sprite = P2_full;
            else if (char2Score == 3) P2_Score3.sprite = P2_full;
        }

        // set collision state /////////
        fallDetected = false;
        charHasFallen = true;
        Debug.Log("YEP.");
    }

    IEnumerator fallReaction()
    {
        exclam.enabled = true;
        yield return new WaitForSeconds(0.2F);
        exclam.enabled = false;
        yield return new WaitForSeconds(0.2F);
        exclam.enabled = true;
        yield return new WaitForSeconds(0.2F);
        exclam.enabled = false;
        yield return new WaitForSeconds(0.2F);
        exclam.enabled = true;
        yield return new WaitForSeconds(0.2F);
        exclam.enabled = false;
    }

    public bool fallTriggered
    {
        get { return fallDetected; }
        set { fallDetected = value; }
    }

    public bool hasFallen
    {
        get { return charHasFallen; }
        set { charHasFallen = value; }
    }

    public int c1_score
    {
        get { return char1Score; }
        set { char1Score = value; }
    }

    public int c2_score
    {
        get { return char2Score; }
        set { char2Score = value; }
    }

    public int fallRank
    {
        get { return firstFall; }
        set { firstFall = value; }
    }
}
