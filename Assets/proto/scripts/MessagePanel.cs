using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour {

	private static GameObject messagePanel;
	private static Text messengerName;
	private static Text messageText;
	private static GameController gameController;
	private static float messageDurationTimer = 0;
	private static float lastMessageDuration;
	public static bool messageIsOpen;

	// Use this for initialization
	void Start () {
		messagePanel = GameObject.Find("Message Panel");
		messengerName = GameObject.Find("Messenger Name").GetComponent<Text>();
		messageText = GameObject.Find("Message Panel Text").GetComponent<Text>();
		gameController = FindObjectOfType<GameController>();

		CloseMessage();
	}
	
	// Update is called once per frame
	void Update () {
		if (messageIsOpen) {
			messageDurationTimer += Time.deltaTime;
		}

		if (messageDurationTimer > lastMessageDuration && lastMessageDuration > 0) {
			CloseMessage();
		} 	
	}

	public static void UpdateMessage(string messenger, InteractionShard.SpeakerTypes speakerType, string newMessage, float messageDuration = 0) {
		messageDurationTimer = 0;
		lastMessageDuration = messageDuration;	
		messengerName.text = messenger + ":";
		messageText.text = newMessage;
	}

	public static void CloseMessage() {
		messagePanel.SetActive(false);
		messageIsOpen = false;
		
	}

	public static void OpenMessage() {
		messageDurationTimer = 0;
		messagePanel.SetActive(true);
		messageIsOpen = true;
	} 
	//not using this right now, but it works;
	private static Color FindSpeakerColor(InteractionShard.SpeakerTypes speakerType) {
		if (speakerType == InteractionShard.SpeakerTypes.PLAYER) {
			return gameController.playerLabelColor;
		} else if (speakerType == InteractionShard.SpeakerTypes.FRIENDLY) {
			return gameController.friendlyLabelColor;
		} else if (speakerType == InteractionShard.SpeakerTypes.NEUTRAL) {
			return gameController.neutralLabelColor;
		} else if (speakerType == InteractionShard.SpeakerTypes.ENEMY) {
			return gameController.enemyLabelColor;
		} else {
			return Color.magenta;
		}
	}

}
