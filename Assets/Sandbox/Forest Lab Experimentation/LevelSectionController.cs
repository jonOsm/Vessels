using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSectionController : MonoBehaviour {
	public bool isHiddenFromMainCamera = false;
	private LevelRenderController levelRenderController;

	void Start() {
		levelRenderController = FindObjectOfType<LevelRenderController>();
	}

	public void HideFromMainCamera() {
		isHiddenFromMainCamera = true;
		LayerMask hiddenLayer =LayerMask.NameToLayer("Hidden"); 
		gameObject.layer = hiddenLayer;
		foreach(Transform child in transform) {
			child.gameObject.layer = hiddenLayer;
		}
	}

	public void MakeVisibleToMainCamera() {
		isHiddenFromMainCamera = false;
		LayerMask visibleLayer = LayerMask.NameToLayer("Default"); 
		gameObject.layer = visibleLayer;
		foreach(Transform child in transform) {
			child.gameObject.layer = visibleLayer;
		}
	}

	void OnTriggerEnter(Collider col) {
		// Debug.Log("sup?");
		// if (col.GetComponent<Interactor>()) {
		// 	levelRenderController.HideAll();
		// 	MakeVisibleToMainCamera();
		// }
	}
}
