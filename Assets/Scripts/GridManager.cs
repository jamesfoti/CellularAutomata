using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GridManager : MonoBehaviour {

	public Cell cell;

	[HideInInspector]
	public Cell[,] cells;
	[HideInInspector]
	public List<Cell> listCells = new List<Cell>();

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

	private void Start() {
		//numCols = Screen.width;
	}

	public void GenerateGrid() {
		ClearGrid();

		cells = new Cell[numRows, numCols];

		for (int row = 0; row < numRows; row++) {
			for (int col = 0; col < numCols; col++) {

				float xCord = col * tileSize;
				float yCord = row * -tileSize;
				Vector2 cellPosition = (Vector2)gridOrigin + new Vector2(xCord, yCord) * (Vector2.one + (Vector2)spacing);

				Cell cell = Instantiate(this.cell, parent: this.transform);
				cell.transform.localPosition = cellPosition;
				cell.transform.localScale = new Vector3(tileSize, tileSize, cell.transform.localScale.z);
				cell.rowPos = row;
				cell.colPos = col;

				cells[row, col] = cell;
				listCells.Add(cell);
			}
		}

		float gridWidth = numCols * tileSize;
		float gridHeight = numRows * tileSize;
		this.transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2) * (Vector2.one + (Vector2)spacing);
	}

	public void ClearGrid() {
		foreach (Cell cell in listCells) {
			if (cell != null) {

				#if UNITY_EDITOR
				DestroyImmediate(cell.gameObject);
				continue;
				#endif

				Destroy(cell.gameObject);
				
			}
		}

		if (cells != null) {
			Array.Clear(cells, 0, cells.Length);
		}
		listCells.Clear();
	}
}
