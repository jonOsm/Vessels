using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "event", menuName = "Interaction/Event")]
public class Event : Shard {

	public override void Play() {
		Debug.Log("play event");
	}
}
