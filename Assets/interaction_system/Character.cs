using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative {
	public enum Affinity {
		FRIENDLY,
		NEUTRAL,
		HOSTILE,
		UNKNOWN
	}

	[CreateAssetMenu(fileName = "character", menuName = "Narrative/Character")]
	public class Character : ScriptableObject {
		public string characterName;
		public Affinity affinity = Affinity.NEUTRAL;
		public Color characterColor;
	}
}
