using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InteractionController : MonoBehaviour {

	public InteractionModel[] interactions;
	//public int interactionID;
	//public TextAsset jsonFile;

	private bool interactionIsPlaying;
	private bool playerInInteractionRange; //abstract this into a "InteractionTrigger" script?
	//private InteractionModel queuedInteraction;
	private int queuedInteractionIndex;
	private int queuedShardIndex = 0;
	private string activeLanguageCode = "en";

	// Use this for initialization
	void Start () {
	}
	
	//Update is called once per frame
	void Update () {
		if (playerInInteractionRange && !interactionIsPlaying && Input.GetButtonDown("Inspect")) {
			StartInteraction();
		}
		else if (interactionIsPlaying) {
			bool interactDown = Input.GetButtonDown("Inspect");
			bool jumpDown = Input.GetButtonDown("Jump");
			bool cancelDown = Input.GetButtonDown("Cancel");

			if (interactDown || jumpDown || cancelDown) {
				PlayNextShard();
			}
		}		
	}

	void OnTriggerEnter(Collider col) {
		//abstract into "InteractionTrigger" script?
		if (col.gameObject.GetComponent<InteractionArea>()) {
			playerInInteractionRange = true;
		}
	}	

	void OnTriggerExit(Collider col) {
		//abstract into "InteractionTrigger" script?
		if (col.gameObject.GetComponent<InteractionArea>()) {
			playerInInteractionRange = false;
		}
	}	

	void StartInteraction() {
		if (queuedInteractionIndex < interactions.Length) {
			interactionIsPlaying = true;		
			PlayNextShard();
		}
	}
	
	void StopActiveInteraction() {
		interactionIsPlaying = false;		
		if (interactions[queuedInteractionIndex].repeatInteraction) {
			queuedShardIndex = interactions[queuedInteractionIndex].repeatShardStartIndex;
		}
		else {
			queuedInteractionIndex++;
		}
	}

	void PlayNextShard() {

		if (queuedShardIndex >= interactions[queuedInteractionIndex].shards.Length) {
			queuedShardIndex = 0;
			StopActiveInteraction();
			return;
		}

		Shard shard = interactions[queuedInteractionIndex].shards[queuedShardIndex];
		shard.Play();
		queuedShardIndex++;
	}

}
