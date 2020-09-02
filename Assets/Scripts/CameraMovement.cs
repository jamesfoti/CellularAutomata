using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float zoomSpeed;
	public float maxZoomIn;

	private Vector3 worldPosition;
	private Vector3 initialPosition;

	private void Start() {
		initialPosition = transform.position;
	}

	private void Update() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;
		worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

		float direction = Input.GetAxis("Mouse ScrollWheel");
		float zoomVelocity = direction * zoomSpeed;

		Debug.Log(direction);

		if (direction > 0) {
			ZoomIn(worldPosition, zoomVelocity);
		}
		else if (direction < 0) {
			ZoomOut(initialPosition, zoomVelocity);
		}

	}

	private void ZoomIn(Vector3 targetPosition, float zoomVelocity) {
		//targetPosition.z = maxZoomIn;
		if (targetPosition.z <= maxZoomIn) {
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, zoomVelocity * Time.deltaTime);
		}
	}

	private void ZoomOut(Vector3 targetPosition, float zoomVelocity) {
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, -zoomVelocity * Time.deltaTime);

	}
}