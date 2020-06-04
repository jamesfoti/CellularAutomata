using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {

	public Cell cell;
	public Color color;

	[HideInInspector]
	public Cell[,] cells;
	public List<Cell> listCells = new List<Cell>();
	public Dictionary<Cell, Vector2> cellIndicies = new Dictionary<Cell, Vector2>();

	[SerializeField]
	private Vector3 gridOrigin;
	[SerializeField]
	[Range(0f, 100f)]
	public int numRows;
	[SerializeField]
	[Range(0f, 100f)]
	public int numCols;
	[SerializeField]
	[Range(0f, 100f)]
	private float tileSize;
	[SerializeField]
	private Vector3 spacing;
	
	public bool autoUpdate;

	private GameManager gameManager;

	private void Awake() {
		gameManager = GetComponent<GameManager>();
	}

	private void Start() {
		GenerateGrid();
	}

	private void Update() {
		if (gameManager.isPlaying) {
			// Update
		}
	}

	public void GenerateGrid() {
		for (int row = 0; row < numRows; row++) {
			for (int col = 0; col < numCols; col++) {

				float xCord = col * tileSize;
				float yCord = row * -tileSize;
				Vector2 cellPosition = (Vector2)gridOrigin + new Vector2(xCord, yCord) * (Vector2.one + (Vector2) spacing);

				Cell cell = Instantiate(this.cell, parent: this.transform);
				cell.transform.localPosition = cellPosition;
				cell.transform.localScale = new Vector3(tileSize, tileSize, cell.transform.localScale.z);
			}
		}

		float gridWidth = numCols * tileSize;
		float gridHeight = numRows * tileSize;
		this.transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2) * (Vector2.one + (Vector2)spacing);
	}

}
