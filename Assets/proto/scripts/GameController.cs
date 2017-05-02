using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Color playerLabelColor;
	public Color friendlyLabelColor;
	public Color neutralLabelColor;
	public Color enemyLabelColor;

	private GameObject menu;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		menu = GameObject.FindGameObjectWithTag("MainMenu");
		player = FindObjectOfType<PlayerController>();
		Time.timeScale = 1;

		menu.SetActive(false);
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.Escape)) {
		// 	Debug.Log("Should Esc on Windows");
		// 	Application.Quit();
		// }

		if (Input.GetButtonDown("Pause")) {
			if (menu) ToggleMenu();
		}
	}

	public void LoadLevel(string levelName) {
		SceneManager.LoadScene(levelName);
	}
	
	public void Quit() {
		Application.Quit();
	}

	public void ToggleMenu() {
		if (menu.activeSelf) {
			menu.SetActive(false);
			Time.timeScale = 1;
			player.UnfreezePosition();
		}
		else if (!menu.activeSelf) {
			menu.SetActive(true);
			Time.timeScale = 0;
			player.FreezePosition();
		}
	}

}
