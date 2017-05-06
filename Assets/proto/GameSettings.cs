using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageCode {
	en,
	fr
}
public class GameSettings : MonoBehaviour {
	public static LanguageCode activateLanguage = LanguageCode.en;
}
