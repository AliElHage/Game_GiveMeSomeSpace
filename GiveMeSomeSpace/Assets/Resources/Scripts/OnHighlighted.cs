using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHighlighted : MonoBehaviour, ISelectHandler, IDeselectHandler{

    public GameObject preview;
    public string path;
    public int buttonIndex;
    private GameObject character;
    public Text characterName;

	// Use this for initialization
	void Start () {
        character = Resources.Load<GameObject>(path);
        
        //Instantiate(character, preview.transform.position, preview.transform.rotation, preview.transform);
        //character.SetActive(false);
        StartCoroutine(buffer(buttonIndex));
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void OnSelect(BaseEventData eventData)
    {
        //preview.transform.GetChild(buttonIndex).gameObject.SetActive(true);
        if(buttonIndex == 0)
        {
            preview.transform.GetChild(0).gameObject.SetActive(true);
            preview.transform.GetChild(1).gameObject.SetActive(false);
            preview.transform.GetChild(2).gameObject.SetActive(false);
            characterName.text = "Thomas the Space Engine";
        }
        else if(buttonIndex == 1)
        {
            preview.transform.GetChild(0).gameObject.SetActive(false);
            preview.transform.GetChild(1).gameObject.SetActive(true);
            preview.transform.GetChild(2).gameObject.SetActive(false);
            characterName.text = "Peekachew";
        }
        else if (buttonIndex == 2)
        {
            preview.transform.GetChild(0).gameObject.SetActive(false);
            preview.transform.GetChild(1).gameObject.SetActive(false);
            preview.transform.GetChild(2).gameObject.SetActive(true);
            characterName.text = "Cursed Blueper";
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //preview.transform.GetChild(buttonIndex).gameObject.SetActive(false);
        //Debug.Log("DESELECTED");
    }

    IEnumerator buffer(int order)
    {
        float time = 0.0F;
        for(int i = 0; i < order; i++)
        do
        {
            time += Time.deltaTime;
            yield return null;
        } while (time < 0.1F);
        Instantiate(character, preview.transform.position, preview.transform.rotation, preview.transform);
        character.SetActive(false);
        if(buttonIndex == 0)
        {
            preview.transform.GetChild(buttonIndex).transform.Rotate(-90, 0, 0);
            preview.transform.GetChild(buttonIndex).transform.Rotate(0, -90, 0);
        }
        else if(buttonIndex == 1)
        {
            preview.transform.GetChild(buttonIndex).transform.Rotate(-90, 0, 0);
            preview.transform.GetChild(buttonIndex).transform.Rotate(0, 0, 0);
            preview.transform.GetChild(buttonIndex).transform.Translate(0, 0, -0.1F);
        }
        else if (buttonIndex == 2)
        {
            //preview.transform.GetChild(buttonIndex).transform.Rotate(-90, 0, 0);
            preview.transform.GetChild(buttonIndex).transform.Rotate(0, 0, 180);
            preview.transform.GetChild(buttonIndex).transform.Translate(0, 0.05F, 0);
        }

    }

}
