using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	public enum InteractionTypes {
		EXPLICIT,
		TRIGGERENTER,
	}

	public enum ExitConditions {
		TRIGGEREXIT,
		TIMER,
		EXPLICT
	}

	public string nameDisplayed = "Forgetful Designer";
	[TextAreaAttribute]
	public string message = "Looks like someone forgot to add a message here. Shame.";
	public InteractionTypes interactionType = InteractionTypes.EXPLICIT;
	public ExitConditions exitCondition = ExitConditions.TRIGGEREXIT;

	private bool isActive = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")) {
			isActive = false;
			MessagePanel.CloseMessage();
		}		
	}

	public void Interact() {
		if (interactionType == InteractionTypes.EXPLICIT) {
			InitiateInteraction();
		}
	}

	public void OnTriggerEnter(Collider col) {
		InteractionArea playerInteractionArea = col.gameObject.GetComponent<InteractionArea>();
		if (playerInteractionArea && interactionType == InteractionTypes.TRIGGERENTER) {
			InitiateInteraction();
		}
	}

	void OnTriggerExit(Collider col) {
		//MessagePanel.CloseMessage();
		if (exitCondition == ExitConditions.TRIGGEREXIT) {
			isActive = false;
			MessagePanel.CloseMessage();
		}

	}

	void InitiateInteraction() {
		isActive = true;
		MessagePanel.UpdateMessage(nameDisplayed, message);	
		MessagePanel.OpenMessage();
	}
}
