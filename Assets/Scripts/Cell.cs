using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour {

	public enum States {
		Dead, Alive
	}

	public States currentState;
	public States nextState;

	public int rowPos;
	public int colPos;

	public GameManager gameManager;

	public Material material;
	public TextMesh text;

	public int neighbors;

	private void Awake() {
		material = GetComponent<SpriteRenderer>().material;
		material.color = Color.black;
		currentState = States.Dead;
		gameManager = FindObjectOfType<GameManager>();
	}

	private void Start() {
		currentState = States.Dead;
	}

	private void Update() {
		neighbors = CountNearbyNeighbors();
		text.text = neighbors.ToString();
	}

	public void UpdateCell() {
		if (currentState == States.Dead && neighbors == 3) {
			nextState = States.Alive;
		}
		else if (currentState == States.Alive && (neighbors < 2 || neighbors > 3)) {
			nextState = States.Dead;
		}
		else {
			nextState = currentState;
		}

		currentState = nextState;
		UpdateColors();
	}

	public void UpdateColors() {

		if (currentState == States.Alive) {
			material.color = Color.white;
		}
		else if (currentState == States.Dead) {
			material.color = Color.black;
		}
	}

	public int CountNearbyNeighbors() {

		Collider2D[] livingNeighbors = Physics2D.OverlapCircleAll(this.transform.position, gameManager.cellDetectionRadius);
		

		int count = 0;
		foreach (Collider2D liveNeighbor in livingNeighbors) {
			if (liveNeighbor.GetComponent<Cell>().currentState == Cell.States.Alive) {
				count++;
			}
		}

		if (this.currentState == States.Alive) {
			count--;
		}

		return count;


	}


	void OnMouseDown() {
		if (currentState == States.Alive) {
			currentState = States.Dead;
		}
		else if (currentState == States.Dead) {
			currentState = States.Alive;
		}
		UpdateColors();
	}

	void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, gameManager.cellDetectionRadius);
	}
}

