using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#


public class TriggerInput : MonoBehaviour {

    public Text statusLabel;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

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
        float vibrationLeft = (state.Triggers.Left < targetLeft) ? (1f/targetLeft) * state.Triggers.Left : (state.Triggers.Left-1f)/(targetLeft-1f);
        float vibratioRight = (state.Triggers.Right < targetRight) ? (1f / targetRight) * state.Triggers.Right : (state.Triggers.Right - 1f) / (targetRight - 1f);
        GamePad.SetVibration(playerIndex, vibrationLeft, vibratioRight);

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
        SoundManager.Instance.SetPitch(1+(state.Triggers.Left-targetLeft)*2, 1+(state.Triggers.Right-targetRight)*2);
    }

    private void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
}
