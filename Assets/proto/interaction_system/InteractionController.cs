using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InteractionController : MonoBehaviour {

	public InteractionModel[] interactions = new InteractionModel[1];
	public InteractionView view;
	public bool loopInteractions = false;
	public bool isSeamless = false;

	private bool interactionIsPlaying;
	private bool playerInInteractionRange; //abstract this into a "InteractionTrigger" script?
	private int queuedInteractionIndex = 0;
	private int queuedShardIndex = 0;
	private string activeLanguageCode = "en";
	private PlayerController player;

	void Start() {
		if (!view) view = GameSettings.dialogView;	
		player = FindObjectOfType<PlayerController>();
	}

	//Update is called once per frame
	void Update () {

		if (playerInInteractionRange && !interactionIsPlaying && Input.GetButtonDown("Inspect")) {
			StartInteraction();
		}
		else if (interactionIsPlaying) {
			bool interactDown = Input.GetButtonDown("Inspect");
			//bool jumpDown = Input.GetButtonDown("Jump");
			bool cancelDown = Input.GetButtonDown("Cancel");

			if (interactDown || cancelDown) {

				PlayNextShard();
			}
		}		
	}

	void OnTriggerEnter(Collider col) {
	}	

	void OnTriggerExit(Collider col) {
		//abstract into "InteractionTrigger" script?
		if (col.gameObject.GetComponent<Interactor>()) {
			playerInInteractionRange = false;
		}

	}	
	public void PrimeInteraction() {
		if (queuedInteractionIndex >= interactions.Length) return;
		playerInInteractionRange = true;
		if (interactions[queuedInteractionIndex].activateOnTriggerEnter) {
			StartInteraction();
		}
	}
	void StartInteraction() {
		if (queuedInteractionIndex < interactions.Length) {
			interactionIsPlaying = true;		

			if(interactions[queuedInteractionIndex].lockPlayerMotion) {
				GameController.currentState[GameState.PLAYER_FROZEN] = true;
				//TIGHTLY COUPLED, INSTEAD HAVE PLAYER SUBSCRIBE TO A EVEN EMITTED BY GAMECONTROLLER WHEN STATE CHANGES
				player.FreezePosition();
			}

			//view.OpenView();
			PlayNextShard();
		}
	}
	
	void StopActiveInteraction() {
		GameController.currentState[GameState.PLAYER_FROZEN] = false;
		interactionIsPlaying = false;		
		view.CloseView();
		player.UnfreezePosition();
		if (interactions[queuedInteractionIndex].repeatInteraction) {
			queuedShardIndex = interactions[queuedInteractionIndex].repeatShardStartIndex;
		}
		else {
			queuedInteractionIndex++;

			if (queuedInteractionIndex >= interactions.Length && loopInteractions) {
				queuedInteractionIndex = 0;
			}
		}

		// if (isSeamless) {
		// 	StartInteraction();
		// }
	}

	void PlayNextShard() {
		if (queuedShardIndex >= interactions[queuedInteractionIndex].shards.Length) {
			queuedInteractionIndex= 0;
			StopActiveInteraction();
			return;
		}

		Dialog dialog = interactions[queuedInteractionIndex].shards[queuedShardIndex] as Dialog;
		view.DisplayDialog(dialog);
		queuedShardIndex++;
	}

}
