using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IActivatable{
	
	public Material activatedMaterial;
	public Material deactivatedMaterial;

	private BoxCollider boxCollider;
	private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();	
		meshRenderer = GetComponent<MeshRenderer>();	
		if (!activatedMaterial) {
			activatedMaterial = meshRenderer.material;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate() {
		meshRenderer.material = activatedMaterial;
		gameObject.layer = LayerMask.NameToLayer("Ground");
		boxCollider.enabled = true;
	}
	public void Deactivate() {
		meshRenderer.material = deactivatedMaterial;
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		boxCollider.enabled = false;
	}
}
