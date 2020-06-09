using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
	
	public void GenerateGrid() {
		ClearGrid();

		cells = new Cell[numRows, numCols];

		for (int row = 0; row < numRows; row++) {
			for (int col = 0; col < numCols; col++) {

				float xCord = col * tileSize;
				float yCord = row * -tileSize;
				Vector2 cellPosition = (Vector2)gridOrigin + new Vector2(xCord, yCord) * (Vector2.one + (Vector2) spacing);

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
				if (EditorApplication.isPlaying) {
					Destroy(cell.gameObject);
				}
				else {
					DestroyImmediate(cell.gameObject);
				}
			}
		}

		if (cells != null) {
			Array.Clear(cells, 0, cells.Length);
		}
		listCells.Clear();
	}


	public int CountLiveNeighbors(int x, int y) {
		int count = 0;

		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {

				int row = (x + j + numRows) % numRows;
				int col = (y + i + numCols) % numCols;
				
				count += (int)cells[row, col].currentState;
			}
		}
		count -= (int)cells[x, y].currentState;

		return count;
	}
	/*
	public void RandomizeCells() {
		for (int i = 0; i < listCells.Count; i++) {
			Array values = Enum.GetValues(typeof(Cell.States));
			Cell.States randomState = (Cell.States)values.GetValue(UnityEngine.Random.Range(0, values.Length));

			listCells[i].currentState = randomState;
			listCells[i].UpdateColors();
		}
	}*/
}
