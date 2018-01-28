using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StyleSet", menuName = "Style/New Style", order = 0)]
public class Style : ScriptableObject {
	public Color fgColor;
	public Color bkgColor;
	
	public Color fgPopColor;
}