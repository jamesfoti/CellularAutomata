using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour {

	[HideInInspector]
	public bool isAlive;
	public Material material;
	public float neighborRadius = 1f;

	private void Awake() {
		this.material = GetComponent<Renderer>().material;
	}

	private void Update() {
		// CheckNeighbors()
	}

	private void CheckNeighbors() {
		// GetNearbyCells()
		int numAliveCells = CountNearbyLiveCells();

		/* Rules:
		 * 1) Any cell with fewer than 2 live neighbors dies -> underpopulation
		 * 2) Any cell with 2 or 3 neighbors lives on to the next generation
		 * 3) Any cell with more than 3 neighbors dies -> overcrowding
		 * 4) Any dead cell with exactly 3 live neighbors comes back to life -> reproduction
		 */

		if (this.tag == "isAlive") {
			UnderPopulation(numAliveCells);
			Lives(numAliveCells);
			OverCrowding(numAliveCells);
		}
		else if (this.tag == "isDead") {
			Reproduction(numAliveCells);
		}
	}

	private int CountNearbyLiveCells() {
		Collider2D[] neighborCells = Physics2D.OverlapCircleAll(this.transform.position, neighborRadius);

		int numAliveCells = 0;

		foreach (Collider2D cellCollider in neighborCells) {
			if (cellCollider.tag == "isAlive") {
				numAliveCells++;
			}
		}

		return numAliveCells;


	}

	private void UnderPopulation(int numAliveCells) {

		if (numAliveCells < 2) {
			this.tag = "isDead";
			this.material.color = Color.white;
		}
	}

	private void Lives(int numAlivecells) {
		
		if (numAlivecells == 2 || numAlivecells == 3) {
			this.tag = "isAlive";
			this.material.color = Color.green;
		}
	}

	private void OverCrowding(int numAliveCells) {

		if (numAliveCells > 3) {
			this.tag = "isDead";
			this.material.color = Color.white;
		}
	}

	private void Reproduction(int numAlivecells) {
		
		if (numAlivecells == 3) {
			this.tag = "isAlive";
			this.material.color = Color.green;
		}
	}



}
