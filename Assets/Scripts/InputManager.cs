using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure; // Required in C#
using UnityEngine;

public class InputManager : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;


    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
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

    public void VibrateController(float vibrationLeft, float vibratioRight)
    {
        GamePad.SetVibration(playerIndex, vibrationLeft, vibratioRight);
    }

    public PlayerIndex PlayerIndex
    {
        get { return playerIndex; }
    }
    public GamePadTriggers Triggers
    {
        get { return state.Triggers; }
    }
    public float LeftTrigger
    {
        get { return state.Triggers.Left; }
    }
    public float RightTrigger
    {
        get { return state.Triggers.Right; }
    }
    
}
