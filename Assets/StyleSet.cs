using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Style", menuName = "Style/New Style Set", order = 1)]
public class StyleSet : ScriptableObject {
	public Style primary;
	public List<Style> highlights;
}


