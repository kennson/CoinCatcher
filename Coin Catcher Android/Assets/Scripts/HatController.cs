using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour {

	public Camera cam;

	private float maxWidth;
	private bool canControl;

	void Start () {
		if (cam == null) {
			cam = Camera.main;		
		}
		canControl = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = renderer.bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;
	}

	void Update () {
		if (canControl) {
			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
			Vector3 targetPosition = new Vector3 (rawPosition.x, -3.5f, 0.0f);
			float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
			targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
			rigidbody2D.MovePosition (targetPosition);
		}
	}

	public void ToggleControl (bool toggle) {
		canControl = toggle;
	}
}





