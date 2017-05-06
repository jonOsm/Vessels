using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Interaction", menuName = "Narrative/Interaction")]
public class InteractionModel : ScriptableObject {
	public enum test {
		test1, test2
	}
	public int interactionID;
	public string interactionName;
	public Shard[] shards;
	public bool repeatInteraction;
	public int repeatShardStartIndex;
	public int nextInteractionID;
	public int previousInteractionID;
	public int[] interactionDependencyIDs;
}
