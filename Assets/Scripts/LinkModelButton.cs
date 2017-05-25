using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkModelButton : MonoBehaviour {
	private PlayerSquirrel playerSquirrel;
	private Button button;

	void Start() {
		playerSquirrel = FindObjectOfType<PlayerSquirrel>();
		button = GetComponent<Button>();
	}
	void Update() {
		if (!playerSquirrel.isLinked && playerSquirrel.isInLinkingRange) {
			GetComponent<Button>().interactable = true;
		} else {
			button.interactable = false;
		}
	}
}
