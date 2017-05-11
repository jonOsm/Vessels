using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartMessage : MonoBehaviour {

	public float fadeTime;

	private float interpolator = 0;
	private Text text;
	private Color color;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		color = text.color;	
	}
	
	// Update is called once per frame
	void Update () {
		color.a = Mathf.PingPong(Time.time/fadeTime, 1);
		text.color = color;
	}

}
