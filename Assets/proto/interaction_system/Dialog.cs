using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrative;

[System.Serializable]
[CreateAssetMenu(fileName = "dialog", menuName = "Narrative/Dialog")]
public class Dialog:Shard{
	public Character speaker;
	public DialogText[] dialogText = new DialogText[1];
	public bool hasExpiry = false;
	public float expiryTime = 0;
	public override void Play() {
	}

	public string GetText(LanguageCode languageCode) {
		for (int i = 0; i < dialogText.Length; i++) {
			if (dialogText[i].languageCode == GameSettings.activateLanguage) {
				return dialogText[i].text;
			}
		}
		return null;
	}
}
