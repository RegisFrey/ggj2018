﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Selection {
	public Sprite symbol;

    public Selection(Sprite symbol)
    {
        this.symbol = symbol;
    }
}

public class Selector : MonoBehaviour {

	public Image selectionPrefab;
	public List<Selection> selections;
	[Tooltip("Must be odd for selection to be correctly indicted")]
	public int visibleSelections = 5;
	public int baseSize = 200;
	public int sizeFalloff = 60;
	// Runtime
	public List<Image> selectionObjects;
	private int focusedIndex = 2; // start in middle
	private int visibleRange = 2;

    private List<DecodeChoice> choices;

    void OnEnable()
    {
        EventsManager.StartListening("SelectionButtonPressed", SelectionMade);
        EventsManager.StartListening("ScrollingRight", CycleRight);
        EventsManager.StartListening("ScrollingLeft", CycleLeft);
        EventsManager.StartListening("NewLevelLoaded", NewLevelLoaded);
    }

    void OnDisable()
    {
        EventsManager.StopListening("SelectionButtonPressed", SelectionMade);
        EventsManager.StopListening("ScrollingRight", CycleRight);
        EventsManager.StopListening("ScrollingLeft", CycleLeft);
        EventsManager.StopListening("NewLevelLoaded", NewLevelLoaded);
    }


    void Render() {
		for(int i = 0; i < selectionObjects.Count; i++)
        {			
			// Disable selections out of Range
			/////////////////////////////////////
			// we are moving a reference frame over an array
			//   [1,2,3,4,5]
			// [0,1,2,3,4,5,6,7]
			// focusedIndex is 3
			// drop index 0 because visibleRange = 2 and 
			// focusedIndex (3) - visibleRange (2) = 1
			// and index 0 is < 1
			
			if( i < (focusedIndex - visibleRange) || i > (focusedIndex + visibleRange) )
			{
				selectionObjects[i].gameObject.SetActive(false);
			}
			else
			{
				selectionObjects[i].gameObject.SetActive(true);
				// size down selections not in center 
				//if(i != focusedIndex)
				//{
				int distance = (int)Mathf.Abs(focusedIndex - i);
				int edge = baseSize - (distance * sizeFalloff);
				Vector2 size = new Vector2(edge, edge);
				selectionObjects[i].rectTransform.sizeDelta = size;
				//}
			}
        }		
	}

    void NewLevelLoaded()
    {
        visibleRange = focusedIndex = (int)Mathf.Floor(visibleSelections / 2);
        choices = LoadLevelManager.Instance.GetCurrentLevel().choices;
        // Remove all children
        selectionObjects.Clear();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Add choices as children
        foreach (Transform child in transform) {
     GameObject.Destroy(child.gameObject);
 }        for (int i = 0; i < choices.Count; i++)
        {
            Image selObj = Instantiate(selectionPrefab, Vector3.zero, Quaternion.identity) as Image;
            selObj.gameObject.transform.SetParent(gameObject.transform, false);
            selObj.sprite = choices[i].symbol;
            selectionObjects.Add(selObj);
        }

        Render();
    }

    void SelectionMade()
    {
        Debug.Log("Selection made!!!");
        // Check whether the choice is correct or not

        LoadLevelManager.Instance.LoadNextLevel();
    }

    void CycleLeft () {
        Debug.Log("Cycle left");
        focusedIndex--;
		Render();
	}
	
	void CycleRight () {
        Debug.Log("Cycle right");
        focusedIndex++;
		Render();
	}
}
