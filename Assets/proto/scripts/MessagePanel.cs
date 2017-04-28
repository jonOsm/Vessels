using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour {

	private static GameObject messagePanel;
	private static Text messengerName;
	private static Text messageText;

	// Use this for initialization
	void Start () {
		messagePanel = GameObject.Find("Message Panel");
		messengerName = GameObject.Find("Messenger Name").GetComponent<Text>();
		messageText = GameObject.Find("Message").GetComponent<Text>();
		CloseMessage();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void UpdateMessage(string messenger, string newMessage) {
		messengerName.text = messenger;
		messageText.text = newMessage;
	}

	public static void CloseMessage() {
		messagePanel.SetActive(false);
	}

	public static void OpenMessage() {
		messagePanel.SetActive(true);
	}

}
