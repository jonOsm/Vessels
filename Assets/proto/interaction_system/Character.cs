using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative {
[CreateAssetMenu(fileName = "character", menuName = "Narrative/Character")]
	public class Character : ScriptableObject {

		public string characterName;
		public Color characterColor;
	}
}
