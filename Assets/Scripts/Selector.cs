using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Selector : MonoBehaviour, IColorizable {

	public Selection selectionPrefab;
	public List<Image> images;
	[Tooltip("Must be odd for selection to be correctly indicted")]
	public int visibleSelections = 5;
	public int baseSize = 200;
	public int sizeFalloff = 60;
	// Runtime
	public List<Selection> selectionObjects;
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
	
	public void Colorize(StyleSet s) {
		// colorize component images
		for (int i = 0; i < images.Count; i++)
        {
			images[i].color = s.primary.fgColor;
		}
		
		// color selectable objects
		for (int i = 0; i < selectionObjects.Count; i++)
        {
			selectionObjects[i].Symbol.color = s.primary.fgPopColor;
		}
	}
	public void Colorize(Style s) {
		// colorize component images
		for (int i = 0; i < images.Count; i++)
        {
			images[i].color = s.fgColor;
		}
		// color selectable objects
		for (int i = 0; i < selectionObjects.Count; i++)
        {
			selectionObjects[i].Symbol.color = s.fgPopColor;
		}
	}


    void Render() {
        int midPointIndex = selectionObjects.Count / 2; // 2 for 5
        int offset = focusedIndex - midPointIndex; // 0 for focusing on 2, -1 for focusing on 3

		for(int i = 0; i < selectionObjects.Count; i++)
        {
            int newIndex = (i - offset + selectionObjects.Count) % selectionObjects.Count;
            selectionObjects[i].gameObject.transform.SetSiblingIndex(newIndex);
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
        for (int i = 0; i < choices.Count; i++)
        {
            Selection selObj = Instantiate(selectionPrefab, Vector3.zero, Quaternion.identity) as Selection;
            selObj.gameObject.transform.SetParent(gameObject.transform, false);
            selObj.name = choices[i].name;
            selObj.Symbol.sprite = choices[i].symbol;
            selObj.IsCorrect = (i == LoadLevelManager.Instance.GetCurrentLevel().answer);
            selectionObjects.Add(selObj);
        }

        Render();
    }

    void SelectionMade()
    {
        Debug.Log("Selection made!!!");
        Debug.Log("Focused index " + focusedIndex);
        
        // Check whether the choice is correct or not
        if(focusedIndex == LoadLevelManager.Instance.GetCurrentLevel().answer)
        {
            GameManager.Instance.LevelCompleted(EndResult.SUCCESS);
        }
        else
        {
            GameManager.Instance.LevelCompleted(EndResult.FAILURE);
        }
    }

    void CycleLeft () {
        focusedIndex++;
        if (focusedIndex == choices.Count)
        {
            focusedIndex = 0;
        }
		Render();
	}
	
	void CycleRight () {
        focusedIndex--;
        if (focusedIndex < 0)
        {
            focusedIndex = choices.Count - 1;
        }
        Render();
	}
}
