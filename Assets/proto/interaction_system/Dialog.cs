using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrative;

[System.Serializable]
[CreateAssetMenu(fileName = "dialog", menuName = "Narrative/Shard/Dialog")]
public class Dialog:Shard{
	public Character speaker;
	public DialogText[] dialogText;

	public override void Play() {
		for (int i = 0; i < dialogText.Length; i++) {
			if (dialogText[i].languageCode == GameSettings.activateLanguage) {
				Debug.Log(dialogText[i].text);	
			}
		}
	}
}
