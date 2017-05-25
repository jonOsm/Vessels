using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Narrative;

public class InteractionView : MonoBehaviour {

	public Text label;
	public Text textArea;
	private static int numViewsOpen;

	private Dialog currentDialog;
	private Canvas messageCanvas;
	private bool viewOpen;
	private float openDuration = 0;
	void Start () {
		messageCanvas = transform.parent.GetComponent<Canvas>();
		CloseView();
	}
	
	// Update is called once per frame
	void Update () {
		if (viewOpen) {
			openDuration += Time.deltaTime;
		}

		if (currentDialog && currentDialog.hasExpiry && openDuration > currentDialog.expiryTime) {
			CloseView();
		}
	}

	public void DisplayDialog(Dialog dialog) {
		Character character = dialog.speaker;
		string dialogText = dialog.GetText(GameSettings.activateLanguage);

		currentDialog = dialog;
		label.text = character.characterName;
		textArea.text = dialogText;
		OpenView();
	}

	public void CloseView() {
		GameController.currentState[GameState.DIALOG_OPEN] = false;
		transform.localScale = new Vector3(0,0,0);
		viewOpen = false;
		openDuration = 0;
		numViewsOpen--;
		messageCanvas.enabled = false;
		//gameObject.SetActive(false);
	}

	public void OpenView() {
		GameController.currentState[GameState.DIALOG_OPEN] = true;
		transform.localScale = new Vector3(1,1,1);
		viewOpen = true;
		numViewsOpen++;
		messageCanvas.enabled = true;
		//gameObject.SetActive(true);
	}
}
