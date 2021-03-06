﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class LetterGrid : MonoBehaviour, IColorizable {
	
	public GridLetter gridLetterPrefab;
	public int gridWidth = 32;
	public int gridHeight = 32;
	
	public int gridCols = 12;
	public int gridRows = 6;
	
	public StyleSet style;
	
	[Header("Design Vars")]
	[Range(0, 1)]
	public float percentFalsePositives = 0.005f;
	[Range(0, 1)]
	public float percentThresholdGlitch = 0.7f;
	
	[Header("Character Sets")]
	public string correctedAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
	//public String corruptedAlpha = "αß¢Đëƒ*ʜ!յ<|3n0ρ@Ի$7μѵM%{}"; 
	public string corruptedSymbol = "!@#$%^&*()?1234567890~.,<>{}[]\\|/"; 
	
	[Header("Word Lists")]
	[FormerlySerializedAs("codewords")]
	public List<string> codeWords;
	public List<string> noiseWords;
	
	[Header("Runtime")]
	[Range(0, 100)]
	public int offset;
	public List<string> targetWords;
	public List<int> targetWordIndices;
	public string plaintext;
	public string ciphertext;
	public List<GridLetter> grid;

    void OnEnable()
    {
        EventsManager.StartListening("NewLevelLoaded", NewLevelLoaded);
        EventsManager.StartListening("SelectionButtonPressed", SelectionMade);
    }

    void OnDisable()
    {
        EventsManager.StopListening("NewLevelLoaded", NewLevelLoaded);
        EventsManager.StopListening("SelectionButtonPressed", SelectionMade);
    }

    void SelectionMade()
    {
        StopCoroutine("UpdateLetters");
    }

    void NewLevelLoaded()
    {
        Debug.Log("NEW LEVEL LOADED");
        codeWords = LoadLevelManager.Instance.GetCurrentLevel().cluewotds;
        noiseWords = LoadLevelManager.Instance.GetCurrentLevel().fakewotds;

        CreatePlainText();
        // Find offset to start with
        RectTransform container = gameObject.GetComponent<RectTransform>();
        int widthOffset = gridWidth / 2;//(int)(container.sizeDelta.x / 2);
        int heightOffset = gridHeight / 2;//(int)(container.sizeDelta.y / 2);

        // Setup grid
        grid = new List<GridLetter>();
        for (int i = 0; i < GridCharacters(); i++)
        {
            grid.Add(
                        CreateGridLetter(
                            new Vector2(
                                (((i % gridCols) * gridWidth) + widthOffset),
                                ((Mathf.Floor(i / gridCols) * gridHeight * -1) - heightOffset)
                            )
                        )
                    );
        }
        StopCoroutine("UpdateLetters");
        StartCoroutine("UpdateLetters", 1f);
    }
	
	public void Colorize(Style s)//StyleSet s)
	{
		//this.style = s;
		// can't with just style
	}
	public void Colorize(StyleSet s)
	{
		this.style = s;
		RenderLetterGrid();
	}
	
	public static int WrapIndex(int index, int length)
	{
		return ((index % length) + length) % length;
	}
	
	public void RenderLetterGrid() {
		for(int i = 0; i < grid.Count; i++)
		{
			int indexInPlaintext = WrapIndex(i + offset, plaintext.Length);
			RenderGridLetter(grid[i], plaintext[indexInPlaintext], GameManager.Instance.PercentageCorruption, IsInTargetRange(indexInPlaintext) );
		}
	}
	
	IEnumerator UpdateLetters(float numSecs)
	{
		while(true){
			RenderLetterGrid();
            yield return new WaitForSeconds(numSecs);
		}	
	}
	
	int GridCharacters() {
		return gridCols * gridRows;
	}
	
	bool IsInTargetRange(int index) {
		for(int i = 0; i < targetWords.Count; i++)
		{
			if(targetWordIndices[i] <= index && index < (targetWordIndices[i] + targetWords[i].Length) )
				return true;
		}
		return false;
	} 
	
	void CreatePlainText() {
		int gridCharacters = GridCharacters();
    	int randIndex;
        plaintext = "";
        targetWords.Clear();
        targetWordIndices.Clear();

        while (plaintext.Length < gridCharacters) {
			randIndex = Random.Range(0, noiseWords.Count);
			plaintext = plaintext + ' ' + noiseWords[randIndex];
		}
	    // add the code words
		for(int i = 0; i < codeWords.Count; i++)
		{
			// Try and pick an index that doesn't overlap with other codewords
			int attemptIndex;
			do
			{
				attemptIndex = Random.Range(0, plaintext.Length);
			} 
			while ( IsInTargetRange(attemptIndex) );
			// Found an index!
			targetWords.Add(codeWords[i]);
			targetWordIndices.Add( attemptIndex );
            // Insert to plaintext
            //plaintext = plaintext.Substring(0, targetWordIndices[i]) + targetWords[i] + plaintext.Substring(targetWordIndices[i]);
            //plaintext = plaintext.Insert(targetWordIndices[i], targetWord[i].Length, targetWords[i]);
            // overwrite characters
            string firstPart = plaintext.Substring(0, targetWordIndices[i]);
            string secondPart = targetWordIndices[i] + targetWords[i].Length < plaintext.Length ? plaintext.Substring(targetWordIndices[i] + targetWords[i].Length) : "";

            plaintext = firstPart + targetWords[i] + secondPart;
		}
	}
	
	GridLetter CreateGridLetter(Vector2 position) {
        GridLetter gl = Instantiate(gridLetterPrefab,Vector3.zero, Quaternion.identity) as GridLetter;
		gl.gameObject.transform.SetParent(gameObject.transform, false);
        RectTransform glRectTransform = (RectTransform)gl.gameObject.transform;
		glRectTransform.anchoredPosition = new Vector3(position.x, position.y, 0);
        return gl;
	}

	// corruption 0-1, 
	void RenderGridLetter(GridLetter gl, System.Char letter, float corruption, bool target) {
		// UN-corruption % chance to get properly colorized
		if(target && Random.value > corruption ) {
			gl.LetterStyle(style.highlights[0]); // TODO: pick a SPECIFIC highlight for codeword
		}
		// % chance a non-target gets colorized
		else if(Random.value > (1-percentFalsePositives)) {
			gl.LetterStyle(style.highlights[0]); // TODO: random highlights for false positives
		}
		else
		{
			gl.LetterStyle(style.primary);
		}
		// corruption % chance will randomly replace with symbol
		float clarity = Random.value;
		if(clarity > corruption) {
			gl.Unglitch();
			gl.SetLetter(letter);
		}
		// corruption is low enough to show symbol
		else if (clarity < percentThresholdGlitch) {
			gl.Unglitch();
			gl.SetLetter(
				corruptedSymbol[Random.Range(0, corruptedSymbol.Length)]
			);
		}
		// glitch
		else
		{
			gl.Glitch();
		}
	}
}
