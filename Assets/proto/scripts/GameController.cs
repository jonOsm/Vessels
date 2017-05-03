using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Color playerLabelColor;
	public Color friendlyLabelColor;
	public Color neutralLabelColor;
	public Color enemyLabelColor;

	public bool sceneHasPauseMenu = false;
	public GameObject finalCameraFocus;
	public Animator fadeOutAnimation;
	private GameObject menu;
	private PlayerController player;
	private CameraController camera;

	// Use this for initialization
	void Start () {
		// Time.timeScale = 1;
		player = FindObjectOfType<PlayerController>();
		camera = FindObjectOfType<CameraController>();
		Cursor.visible = false;
		if (sceneHasPauseMenu) {
			menu = GameObject.FindGameObjectWithTag("MainMenu");
			menu.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.Escape)) {
		// 	Debug.Log("Should Esc on Windows");
		// 	Application.Quit();
		// }

		if (Input.GetButtonDown("Pause") && sceneHasPauseMenu) {
			if (menu) ToggleMenu();
		}
	}

	public void LoadLevel(string levelName) {
		SceneManager.LoadScene(levelName);
	}
	public void FadeOutOfScene() {
		fadeOutAnimation.Play("fadeOut");
	}	
	public void Quit() {
		Application.Quit();
	}

	public void ToggleMenu() {
		if (menu.activeSelf) {
			// Time.timeScale = 1;
			menu.SetActive(false);
			player.UnfreezePosition();
		}
		else if (!menu.activeSelf) {
			// Time.timeScale = 0;
			menu.SetActive(true);
			player.FreezePosition();
		}
	}

	public void EndPrototype() {
		camera.setObjectToFocus(finalCameraFocus);
		
	}

}
