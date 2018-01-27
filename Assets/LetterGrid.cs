using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class LetterGrid : MonoBehaviour {
	
	public GridLetter gridLetterPrefab;
	public int gridWidth = 32;
	public int gridHeight = 32;
	
	public int gridCols = 12;
	public int gridRows = 6;
	
	public StyleSet style;
	
	[Header("Character Sets")]
	public string correctedAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
	//public String corruptedAlpha = "αß¢Đëƒ*ʜ!յ<|3n0ρ@Ի$7μѵM%{}"; 
	public string corruptedSymbol = "!@#$%^&*()?1234567890~.,<>{}[]\\|/"; 
	
	[Header("Word Lists")]
	[FormerlySerializedAs("codewords")]
	public List<string> codeWords;
	public List<string> noiseWords;
	
	[Header("Runtime")]
	public int offset;
	public string targetWord;
	public int targetWordStart;
	public string plaintext;
	public string ciphertext;
	public List<GridLetter> grid;

	// Use this for initialization
	void Start () {
		CreatePlainText();
		// Setup grid
		grid = new List<GridLetter>();
        for(int i = 0; i < GridCharacters(); i++)
        {
            grid.Add( 
					    CreateGridLetter( 
						    new Vector2(
							    ((i % gridCols) * gridWidth), 
							    (Mathf.Floor(i / gridCols) * gridHeight * -1)
						    ) 
					    )
				    );
        }
		StartCoroutine("UpdateLetters", 1f);
	}
	
	IEnumerator UpdateLetters(float numSecs)
	{
		while(true){
			for(int i = 0; i < grid.Count; i++)
			{
				// wrap i + offset
				RenderGridLetter(grid[i], plaintext[i + offset], GameManager.Instance.PercentageCorruption, false);
			}
			yield return new WaitForSeconds(numSecs);
		}	
	}
	
	int GridCharacters() {
		return gridCols * gridRows;
	}
	
	void CreatePlainText() {
		int gridCharacters = GridCharacters();
    int randIndex;
		while(plaintext.Length < gridCharacters) {
			randIndex = Random.Range(0, noiseWords.Count);
			plaintext = plaintext + ' ' + noiseWords[randIndex];
		}
    // corrupt plain text a bit

    // add the code word
    randIndex = Random.Range(0, plaintext.Length);
    targetWord = codeWords[Random.Range(0,codeWords.Count)];
    plaintext = plaintext.Substring(0, randIndex) + targetWord + plaintext.Substring(randIndex);
        
	}
	
	GridLetter CreateGridLetter(Vector2 position) {
        GridLetter gl = Instantiate(gridLetterPrefab,Vector3.zero, Quaternion.identity) as GridLetter;
				gl.gameObject.transform.SetParent(gameObject.transform, false);
        gl.gameObject.transform.localPosition = new Vector3(position.x, position.y, 0);
        return gl;
	}

	// corruption 0-1, 
	void RenderGridLetter(GridLetter gl, System.Char letter, float corruption, bool target) {
		// UN-corruption % chance to get colorized
		if(target && Random.value > (1 - corruption) ) {
			gl.LetterStyle(style.highlights[0]);
		}
		// 3% chance a non-target gets colorized
		else if(Random.value > 0.97) {
			gl.LetterStyle(style.highlights[0]);
		}
		else
		{
			gl.LetterStyle(style.primary);
		}
		// corruption % chance will randomly replace with symbol
		if(Random.value > corruption) {
			gl.SetLetter(letter);
		}
		else
		{
			gl.SetLetter(
				corruptedSymbol[Random.Range(0, corruptedSymbol.Length)]
			);
		}
	}
}
