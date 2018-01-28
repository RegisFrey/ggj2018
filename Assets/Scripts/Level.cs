using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 0)]
public class Level : ScriptableObject {
	
	public string name;
	[Space(10)]
	[TextArea]
	public string notes;
	[Space(10)]
	public float seconds;
	public StyleSet style;
	
	[System.Serializable]
	public class Dialogue {
		public string text;
		public string speaker;
		// audio asset voice line
	}
	[Space(10)]
	public List<Dialogue> introDialogue;
	[Space(10)]
	public List<string> cluewotds;
	[Header("Either define fake words to pick from or write plaintext")]
	public List<string> fakewotds;
	public string plaintext; 
	
	[Space(10)]
	[EditInPlaceAttribute]
	public List<DecodeChoice> choices;
	public int answer;
	
}