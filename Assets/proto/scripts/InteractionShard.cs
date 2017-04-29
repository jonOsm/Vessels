using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionShard {
	public enum SpeakerTypes {
		NEUTRAL,
		PLAYER,
		ENEMY,
		FRIENDLY 
	}
	public string speaker;
	public SpeakerTypes speakerType;
	[MultilineAttribute]
	public string message;
}
