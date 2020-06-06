using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour {

	public float timeDelay;
	[HideInInspector]
	public bool isPlaying = false;

	private GridManager grid;


	private void Awake() {
		grid = GetComponent<GridManager>();
	}

	private void Start() {
		grid.GenerateGrid();
	}

	private void Update() {
		if (isPlaying) {
			for (int i = 0; i < grid.listCells.Count; i++) {
				grid.listCells[i].UpdateCell();
			}
		}
	}


}