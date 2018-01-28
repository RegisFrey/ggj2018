using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice", menuName = "Decode Choice", order = 0)]
public class DecodeChoice : ScriptableObject {
	public string name;
	public Sprite symbol;
	public string notes;
}