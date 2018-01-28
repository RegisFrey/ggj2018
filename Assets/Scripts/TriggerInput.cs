using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#


public class TriggerInput : MonoBehaviour {

    public Text statusLabel;

    float targetLeft;
    float targetRight;

    float correctnessThreshold = 0.90f;

    void Start () {
        // Set random targets for vibration
        targetLeft = Random.Range(0f, 1f);
        targetRight = Random.Range(0f, 1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float vibrationLeft = (InputManager.Instance.LeftTrigger < targetLeft) ? (1f/targetLeft) * InputManager.Instance.LeftTrigger : (InputManager.Instance.LeftTrigger - 1f)/(targetLeft-1f);
        float vibratioRight = (InputManager.Instance.RightTrigger < targetRight) ? (1f / targetRight) * InputManager.Instance.RightTrigger : (InputManager.Instance.RightTrigger - 1f) / (targetRight - 1f);
        InputManager.Instance.VibrateController(vibrationLeft, vibratioRight);

        if (statusLabel != null) {
            if (vibrationLeft > correctnessThreshold && vibratioRight > correctnessThreshold)
            {
                statusLabel.text = "SWEET!";
            }
            else
            {
                statusLabel.text = "KEEP ADJUSTING";
            }
        }

        GameManager.Instance.SetPercentageCorruptions(1f - vibrationLeft, 1f - vibratioRight);
        SoundManager.Instance.SetPitch(1+(InputManager.Instance.LeftTrigger - targetLeft));
    }   
}
