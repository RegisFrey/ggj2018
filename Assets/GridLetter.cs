using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLetter : MonoBehaviour {

	public Text letter;
	public Image backing;

	public void SetLetter (System.Char l) {
		letter.text = l.ToString();
	}
	
	public void LetterStyle(Style s) {
		letter.color = s.fgColor;
		backing.gameObject.SetActive(true); // could disable if style matches main bkg style
		backing.color = s.bkgColor;
	}
}
