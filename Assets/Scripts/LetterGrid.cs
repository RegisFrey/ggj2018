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
	public string targetWord;
	public int targetWordStart;
	public string plaintext;
	public string ciphertext;
	public List<GridLetter> grid;

    private Image bkgImage;
    private Color bkgImageColor = Color.green; // testing

    private void Awake()
    {
        bkgImage = GetComponent<Image>();
    }

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
	
	public static int WrapIndex(int index, int length)
  {
    return ((index % length) + length) % length;
  }
	
	IEnumerator UpdateLetters(float numSecs)
	{
		while(true){
			for(int i = 0; i < grid.Count; i++)
			{
				int indexInPlaintext = WrapIndex(i + offset, plaintext.Length);
				bool target = (targetWordStart <= indexInPlaintext && indexInPlaintext < (targetWordStart + targetWord.Length) );
				// wrap i + offset
				RenderGridLetter(grid[i], plaintext[WrapIndex(i + offset, plaintext.Length)], GameManager.Instance.PercentageCorruption, target);
			}
            bkgImageColor.a = 1f - GameManager.Instance.PercentageCorruption;
            bkgImage.color = bkgImageColor;
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
    targetWordStart = Random.Range(0, plaintext.Length);
    targetWord = codeWords[Random.Range(0,codeWords.Count)];
    plaintext = plaintext.Substring(0, targetWordStart) + targetWord + plaintext.Substring(targetWordStart);
        
	}
	
	GridLetter CreateGridLetter(Vector2 position) {
        GridLetter gl = Instantiate(gridLetterPrefab,Vector3.zero, Quaternion.identity) as GridLetter;
				gl.gameObject.transform.SetParent(gameObject.transform, false);
        gl.gameObject.transform.localPosition = new Vector3(position.x, position.y, 0);
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
