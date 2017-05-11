using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageCode {
	en,
	fr
}
public class GameSettings : MonoBehaviour {
	
	public static LanguageCode activateLanguage = LanguageCode.en;
	public static InteractionView dialogView;
	public InteractionView defaultDialogView;
	
	private static GameSettings _self;	
	void Awake() {
		if (!_self) {
			_self = this;
			dialogView = defaultDialogView;
		} else {
			Destroy(gameObject);
		} 
	}
}
