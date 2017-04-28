using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionShard {
	public enum SpeakerTypes {
		NEUTRAL,
		PLAYER,
		ENEMY
	}
	public string speaker;
	public SpeakerTypes speakerType;
	[MultilineAttribute]
	public string message;
}
