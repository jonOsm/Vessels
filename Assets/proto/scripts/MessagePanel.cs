using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour {

	private Text messengerName;
	private Text messageText;
	// Use this for initialization
	void Start () {
		messengerName = GameObject.Find("Messenger Name").GetComponent<Text>();
		messageText = GameObject.Find("Message").GetComponent<Text>();
		UpdateMessage("Test", "This is the new message to send");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMessage(string messenger, string newMessage) {
		messengerName.text = messenger;
		messageText.text = newMessage;
	}
}
