using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSectionController : MonoBehaviour {
	public bool isHiddenFromMainCamera = false;
	public float minLayerChangeDelay = 0.5f;
	public float maxLayerChangeDelay = 1f;

	private LevelRenderController levelRenderController;
	private LayerMask visibleLayer;
	private List<Coroutine> ongoingTransitions = new List<Coroutine>();

	void Start() {
		levelRenderController = FindObjectOfType<LevelRenderController>();
		LayerMask visibleLayer = LayerMask.NameToLayer("Default"); 
	}

	public void HideDescendantsFromMainCamera() {
		isHiddenFromMainCamera = true;
		StopAllCoroutines();
		LayerMask hiddenLayer =LayerMask.NameToLayer("Hidden"); 
		//gameObject.layer = hiddenLayer;
		ChangeDescendantsLayer(transform, hiddenLayer);
	}

	public void MakeDescendantsVisible() {
		isHiddenFromMainCamera = false;
		//gameObject.layer = visibleLayer;
		StopAllCoroutines();
		ChangeDescendantsLayer(transform, visibleLayer);
	}

	private void ChangeDescendantsLayer(Transform parent, LayerMask newLayer) {
		foreach(Transform child in parent) {
			//child.gameObject.layer = newLayer;
			IEnumerator changeLayer = ChangePartLayer(child, newLayer);
			ongoingTransitions.Add(StartCoroutine(changeLayer));

			if (child.childCount > 0) {
				ChangeDescendantsLayer(child, newLayer);
			}
		}
	}

	private void StopOngoingTransitions() {
		foreach(IEnumerator transition in ongoingTransitions) {
			StopCoroutine(transition);
		}
	}
	void OnTriggerEnter(Collider col) {
		if (col.GetComponent<Interactor>()) {
			//levelRenderController.HideAll();
			MakeDescendantsVisible();
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.GetComponent<Interactor>()) {
			//levelRenderController.HideAll();
			HideDescendantsFromMainCamera();
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
