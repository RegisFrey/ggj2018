using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLetter : MonoBehaviour {

	public Text letter;

	public void SetLetter (System.Char l) {
		letter.text = l.ToString();
	}
	
	public void LetterColor(Color c) {
		letter.color = c;
	}
}
