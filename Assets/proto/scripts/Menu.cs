using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	// Use this for initialization
	public ScrollRect submenuScrollRect;
	private EventSystem eventSystem;
	private GameObject openSubmenu = null;
	void Awake() {
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
	}
	void OnEnable() {
		Time.timeScale = 0;
		eventSystem.SetSelectedGameObject(null);
		eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
	}

	void OnDisable() {
		Time.timeScale = 1;
	}

	public void DisplaySubmenu(GameObject submenu) {
		if (openSubmenu) {
			openSubmenu.SetActive(false);
		}
		openSubmenu = submenu;
		submenuScrollRect.content = submenu.GetComponent<RectTransform>();
		submenu.SetActive(true);
	}
}
