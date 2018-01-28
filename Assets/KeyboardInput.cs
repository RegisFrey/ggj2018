using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        // Keyboard events
        if (Input.GetKeyUp(KeyCode.A))
        {
            EventsManager.TriggerEvent("SelectionButtonPressed");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            EventsManager.TriggerEvent("ScrollingRight");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            EventsManager.TriggerEvent("ScrollingLeft");
        }
    }
}
