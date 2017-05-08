using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum GameState {
	MENU_OPEN,
	DIALOG_OPEN,
	PLAYER_FROZEN,
	DIALOG_ON_TIMER
}

public class GameController : MonoBehaviour {
	public static Dictionary<GameState, bool> currentState = new Dictionary<GameState, bool>();
	public float musicFadeInTime; 

	public bool sceneHasPauseMenu = false;
	public GameObject finalCameraFocus;
	public Animator fadeOutAnimation;
	
	private GameObject menu;
	private PlayerController player;
	private CameraController mainCamera;
	private AudioSource audioSource;
	private float musicFadeInInterpolator = 0;
	private bool isMusicFadingIn = true;

	// Use this for initialization
	void Awake() {
		BuildGameState();
	}
	void Start () {
		// Time.timeScale = 1;
		player = FindObjectOfType<PlayerController>();
		mainCamera = FindObjectOfType<CameraController>();
		audioSource = GetComponent<AudioSource>();
		Cursor.visible = false;
		if (sceneHasPauseMenu) {
			menu = GameObject.FindGameObjectWithTag("MainMenu");
			menu.SetActive(false);
		}
	}

	void BuildGameState() {
		if (currentState.Count <=0) {
			foreach(GameState state in System.Enum.GetValues(typeof(GameState))) {
				GameController.currentState.Add(state, false);
			}
		}
	}	

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Pause") && sceneHasPauseMenu) {
			if (menu) ToggleMenu();
		}

		if (isMusicFadingIn) {
			FadeInMusic();
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
		mainCamera.setObjectToFocus(finalCameraFocus);
		
	}

	public void FadeInMusic() {
		if(!audioSource.isPlaying)
			audioSource.Play();

		musicFadeInInterpolator += Time.deltaTime/musicFadeInTime;
		
		audioSource.volume = Mathf.Lerp(0, 1, musicFadeInInterpolator);
		if (musicFadeInInterpolator > 1) {
			isMusicFadingIn = false;
		}
	}

	public void DeletePlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

}
