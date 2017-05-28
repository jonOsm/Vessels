using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSectionController : MonoBehaviour {
	public bool isHiddenFromMainCamera = false;
	public float minLayerChangeDelay = 0.5f;
	public float maxLayerChangeDelay = 1f;

	private LevelRenderController levelRenderController;
	private LayerMask visibleLayer;
	void Start() {
		levelRenderController = FindObjectOfType<LevelRenderController>();
		LayerMask visibleLayer = LayerMask.NameToLayer("Default"); 
	}

	public void HideFromMainCamera() {
		isHiddenFromMainCamera = true;
		LayerMask hiddenLayer =LayerMask.NameToLayer("Hidden"); 
		gameObject.layer = hiddenLayer;
		ChangeDescendantsLayer(transform, hiddenLayer);
	}

	private void ChangeDescendantsLayer(Transform parent, LayerMask newLayer) {
		foreach(Transform child in parent) {
			child.gameObject.layer = newLayer;
			IEnumerator makePartHidden = ChangePartLayer(child, newLayer);
			StartCoroutine(makePartHidden);

			if (child.childCount > 0) {
				ChangeDescendantsLayer(child, newLayer);
			}
		}
	}
	public void MakeVisibleToMainCamera() {
		isHiddenFromMainCamera = false;
		gameObject.layer = visibleLayer;
		foreach(Transform child in transform) {
			//child.gameObject.layer = visibleLayer;
			//IEnumerator makePartVisible = ChangePartLayer(child, visibleLayer);
			IEnumerator makePartVisible = EnablePart(child);
			StartCoroutine(makePartVisible);
		}
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log("sup?");
		if (col.GetComponent<Interactor>()) {
			//levelRenderController.HideAll();
			MakeVisibleToMainCamera();
		}
	}

	void OnTriggerExit(Collider col) {
		Debug.Log("sup?");
		if (col.GetComponent<Interactor>()) {
			//levelRenderController.HideAll();
			HideFromMainCamera();
		}
	}

//Different techniques to achieve a similar affect
	IEnumerator DisablePart(Transform part) {

		yield return new WaitForSeconds(Random.Range(minLayerChangeDelay, maxLayerChangeDelay));
		part.gameObject.SetActive(false);
	}

	IEnumerator EnablePart(Transform part) {

		yield return new WaitForSeconds(Random.Range(minLayerChangeDelay, maxLayerChangeDelay));
		part.gameObject.SetActive(true);
	}
	//more or less performant than ChangePartLayer?
	IEnumerator DisableMeshRenderer(Transform part) {

		yield return new WaitForSeconds(Random.Range(minLayerChangeDelay, maxLayerChangeDelay));
		part.gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	IEnumerator ChangePartLayer(Transform part, LayerMask layer) {
		yield return new WaitForSeconds(Random.Range(minLayerChangeDelay, maxLayerChangeDelay));
		part.gameObject.layer = layer;
	}
}
