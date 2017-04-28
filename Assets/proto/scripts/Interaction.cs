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

	public static Interaction activeInteraction;
	
	public InteractionShard[] shards; 
	// public string nameDisplayed = "Forgetful Designer";
	// [TextAreaAttribute]
	// public string message = "Looks like someone forgot to add a message here. Shame.";
	public InteractionStartConditions interactionType = InteractionStartConditions.EXPLICIT;
	public InteractionExitConditions exitCondition = InteractionExitConditions.TRIGGEREXIT;
	public bool loopInteraction = true;
	public bool freezePlayer = false;

	
	private int currentShard = 0;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();	
	}
	
	// Update is called once per frame
	void Update () {
		// print(freezePlayer);
		if (activeInteraction == this && Input.GetButtonDown("Cancel") && !freezePlayer) {
			MessagePanel.CloseMessage();
		}		

		if (activeInteraction == null && Input.GetButtonDown("Cancel")) {
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
			MessagePanel.CloseMessage();
		}

	}

	void InitiateInteraction() {
		activeInteraction = this;
		if (freezePlayer) {
			player.FreezePosition();
			if (currentShard >= shards.Length) {
				player.UnfreezePosition();
				MessagePanel.CloseMessage();
				currentShard = shards.Length -1;
				return;
			}	
		}

		if (currentShard >= shards.Length) {
			if(loopInteraction) {
				currentShard = 0;
			} else {
				currentShard = shards.Length -1;
			}
		}	

		MessagePanel.UpdateMessage(shards[currentShard].speaker, shards[currentShard].message);	
		MessagePanel.OpenMessage();
		currentShard++;

	}
}
