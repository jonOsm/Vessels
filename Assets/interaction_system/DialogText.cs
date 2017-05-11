
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogText{
	public LanguageCode languageCode;
	[TextAreaAttribute]
	public string text;
}
