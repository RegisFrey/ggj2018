using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverOverlay : MonoBehaviour {

    public GameObject failObj;
	// Update is called once per frame
	void Update () {
        Debug.Log("GAME OVER? " + GameManager.Instance.IsInGameOver());
        failObj.SetActive(GameManager.Instance.IsInGameOver());
	}
}
