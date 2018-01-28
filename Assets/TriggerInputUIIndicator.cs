using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerInputUIIndicator : MonoBehaviour {

    public bool isLeftTrigger;
    public Image fillerImage;

	void Update () {
        fillerImage.fillAmount = isLeftTrigger ? InputManager.Instance.LeftTrigger : InputManager.Instance.RightTrigger;
	}
}
