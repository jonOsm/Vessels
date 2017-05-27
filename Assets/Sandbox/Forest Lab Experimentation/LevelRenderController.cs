using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRenderController : MonoBehaviour {
	//List<LevelSectionController> hiddenObjects;
	LevelSectionController[] levelSections;

	// Use this for initialization
	void Start () {
		levelSections = FindObjectsOfType<LevelSectionController>();
		foreach(LevelSectionController section in levelSections) {
			if(section.isHiddenFromMainCamera) {
				section.HideFromMainCamera();	
			}
		}
	}

	public void HideAll() {
		foreach (LevelSectionController section in levelSections) {
				section.HideFromMainCamera();
		}
	}	
}
