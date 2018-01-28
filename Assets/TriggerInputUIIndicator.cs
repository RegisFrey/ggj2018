using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerInputUIIndicator : MonoBehaviour {

    public bool isLeftTrigger;
    public Image fillerImage;
    public Image foundSSImage;

	void Update () {
        fillerImage.fillAmount = isLeftTrigger ? InputManager.Instance.LeftTrigger : InputManager.Instance.RightTrigger;
        foundSSImage.enabled = GameManager.Instance.GetPercentageCorruption(isLeftTrigger) <= 0.05f;

    }
}
