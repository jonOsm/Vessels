using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {
	public enum InteractionTypes {
		SIMPLE,
		STORY
	}
	public enum InteractionStartConditions {
		EXPLICIT,
		TRIGGERENTER,
	}

	public enum InteractionExitConditions {
		TRIGGEREXIT,
		TIMER,
		EXPLICT
	}
	
	public InteractionShard[] shards; 
	// public string nameDisplayed = "Forgetful Designer";
	// [TextAreaAttribute]
	// public string message = "Looks like someone forgot to add a message here. Shame.";
	public InteractionStartConditions interactionType = InteractionStartConditions.EXPLICIT;
	public InteractionExitConditions exitCondition = InteractionExitConditions.TRIGGEREXIT;
	public bool loopInteraction = true;

	
	private int currentShard = 0;
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
		if (interactionType == InteractionStartConditions.EXPLICIT) {
				InitiateInteraction();
		}
	}

	public void OnTriggerEnter(Collider col) {
		InteractionArea playerInteractionArea = col.gameObject.GetComponent<InteractionArea>();
		if (playerInteractionArea && interactionType == InteractionStartConditions.TRIGGERENTER) {
			InitiateInteraction();
		}
	}

	void OnTriggerExit(Collider col) {
		//MessagePanel.CloseMessage();
		if (exitCondition == InteractionExitConditions.TRIGGEREXIT) {
			isActive = false;
			MessagePanel.CloseMessage();
		}

	}

	void InitiateInteraction() {
		isActive = true;
		MessagePanel.UpdateMessage(shards[currentShard].speaker, shards[currentShard].message);	
		MessagePanel.OpenMessage();
		currentShard++;

		if (currentShard >= shards.Length && loopInteraction) {
			currentShard = 0;
		}	
	}
}
