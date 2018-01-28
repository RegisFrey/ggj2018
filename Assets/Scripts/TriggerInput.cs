using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TriggerInput : MonoBehaviour {

    public Text statusLabel;

    float targetLeft;
    float targetRight;
	
	// Update is called once per frame
	void FixedUpdate () {

        if (GameManager.Instance.STATE == GameState.STARTED)
        {
            float targetLeft = GameManager.Instance.TargetLeftTrigger;
            float targetRight = GameManager.Instance.TargetRightTrigger;

            float vibrationLeft = (InputManager.Instance.LeftTrigger < targetLeft) ? (1f / targetLeft) * InputManager.Instance.LeftTrigger : (InputManager.Instance.LeftTrigger - 1f) / (targetLeft - 1f);
            float vibratioRight = (InputManager.Instance.RightTrigger < targetRight) ? (1f / targetRight) * InputManager.Instance.RightTrigger : (InputManager.Instance.RightTrigger - 1f) / (targetRight - 1f);

            InputManager.Instance.VibrateController(vibrationLeft, vibratioRight);
            GameManager.Instance.SetPercentageCorruptions(1f - vibrationLeft, 1f - vibratioRight);
            SoundManager.Instance.SetAudioCorruption(GameManager.Instance.percentageCorruption);
        }
    }   
}
