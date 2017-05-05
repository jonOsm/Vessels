using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyController : SceneController {
	//the reason we're using this proxy is because of this bug: https://issuetracker.unity3d.com/issues/loadlevel-gi-and-reflection-probes-are-not-loaded-with-the-scene-in-editor-if-auto-baking-mode-is-used
	//if we run reload function too quickly, the next scene will have messed up lighting
	public float loadLevelDelay = 1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (gameController.GetComponent<AudioSource>().isPlaying) {
			gameController.GetComponent<AudioSource>().Stop();
		}	

		Invoke("LoadLevel", 1f);		
	}

	void LoadLevel() {
		gameController.LoadLevel("prototype");			
	}
}
