using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAffector : MonoBehaviour {
	public enum AffectorType {
		FADEOUT	}
	public AudioSource source;
	public AffectorType affectorType;
	public float affectorDuration;

	private bool isFadingOut = false;
	private float interpolator;	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isFadingOut) {
			interpolator += Time.deltaTime / affectorDuration;
			source.volume = Mathf.Lerp(source.volume, 0, interpolator);
			if (source.volume == 0 ) {
				interpolator = 0;
				isFadingOut = false;
			}
		}	
	}

	void OnTriggerEnter() {
		if(affectorType == AffectorType.FADEOUT) {
			isFadingOut = true;
		}
	}
}
