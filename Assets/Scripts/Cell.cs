﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour {

	public enum States {Dead, Alive}
	public States currentState;
	public States nextState;
	public int rowPos;
	public int colPos;

	private GridManager grid;
	private AutomataManager autoManager;
	private SpriteRenderer render;
	private int neighbors;


	private void Awake() {
		render = GetComponent<SpriteRenderer>();
		grid = FindObjectOfType<GridManager>();
		autoManager = FindObjectOfType<AutomataManager>();
	}

	private void Start() {
		// Every cell starts out dead for now
		currentState = States.Dead;
		render.color = autoManager.deadColor;
	}

	private void Update() {
		neighbors = CountNearbyNeighbors(); // Always checking neighbors
	}

	public void UpdateCell() {
		if (currentState == States.Alive) {
			if (neighbors < 2) {
				// Underpopulation
				nextState = States.Dead;
			}
			else if (neighbors == 2 || neighbors == 3) {
				// Remains alive
				nextState = States.Alive;
			}
			else if (neighbors > 3) {
				// Overpopulation
				nextState = States.Dead;
			}
		}
		else if (currentState == States.Dead) {
			if (neighbors == 3) {
				// Reproduction
				nextState = States.Alive;
			}
		}

		currentState = nextState;
		UpdateColors();
		
	}

	public void UpdateColors() {
		if (currentState == States.Alive) {
			render.color = Color.Lerp(render.color, autoManager.aliveColor, autoManager.colorLerpTime);
		}
		else if (currentState == States.Dead) {
			render.color = Color.Lerp(render.color, autoManager.deadColor, autoManager.colorLerpTime);
		}
	}

	public int CountNearbyNeighbors() {
		// Iteration over a 3x3 grid where this cell is the center
		int count = 0;
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				int row = (rowPos + i + grid.numRows) % grid.numRows;
				int col = (colPos + j + grid.numCols) % grid.numCols;
				
				if (grid.cells[row, col].currentState == Cell.States.Alive) {
					count++;
				}
			}
		}

		if (currentState == States.Alive) {
			// If this cell is alive, then don't include it
			count--;
		}

		return count;
	}


	void OnMouseDown() {
		Debug.Log("Clicked on [" + grid.numRows + ", " + grid.numCols + "]");
		if (currentState == States.Alive) {
			currentState = States.Dead;
		}
		else if (currentState == States.Dead) {
			currentState = States.Alive;
		}
		UpdateColors();
	}

}

