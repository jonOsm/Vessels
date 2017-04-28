using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	public string nameDisplayed = "Forgetful Designer";
	[TextAreaAttribute]
	public string message = "Looks like someone forgot to add a message here. Shame.";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Interact() {
		MessagePanel.UpdateMessage(nameDisplayed, message);	
	}
}
