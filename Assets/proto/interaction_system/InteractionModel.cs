using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Interaction", menuName = "Interaction/Interaction")]
public class InteractionModel : ScriptableObject {
	public Shard[] shards = new Shard[1];
	public bool repeatInteraction = true;
	public int repeatShardStartIndex = 0;
	public bool lockPlayerMotion = true;
	public bool activateOnTriggerEnter = false;
	public bool selfCloseView = false;
	public float secondsToDisplay = 0;
}
