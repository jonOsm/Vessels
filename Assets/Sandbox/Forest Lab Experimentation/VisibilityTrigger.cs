using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityTrigger : MonoBehaviour {
	public LevelSectionController[] sectionsToToggle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Interactor>()) {
			foreach (LevelSectionController section in sectionsToToggle) {
				if (section.isHiddenFromMainCamera) {
					section.MakeDescendantsVisible();
				}
				else {
					section.HideDescendantsFromMainCamera();
				}
			}
		}
	}
}
