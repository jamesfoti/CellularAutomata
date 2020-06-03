using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {

	public Cell cell;
	public float detectionRadius;

	[HideInInspector]
	public List<Cell> cells = new List<Cell>();

	[SerializeField]
	private Vector3 gridOrigin;
	[SerializeField]
	[Range(0f, 100f)]
	private int numRows;
	[SerializeField]
	[Range(0f, 100f)]
	private int numCols;
	[SerializeField]
	[Range(0f, 100f)]
	private float tileSize;
	[SerializeField]
	private Vector3 spacing;
	
	public bool autoUpdate;

	private void Start() {
		GenerateGrid();
	}

	public void GenerateGrid() {
		ClearGrid(); // Clear grid if one aleady exists

		for (int row = 0; row < numRows; row++) {
			for (int col = 0; col < numCols; col++) {

				float xCord = col * tileSize;
				float yCord = row * -tileSize;
				Vector2 cellPosition = (Vector2)gridOrigin + new Vector2(xCord, yCord) * (Vector2.one + (Vector2) spacing);

				Cell cell = Instantiate(this.cell, parent: this.transform);
				cell.transform.localPosition = cellPosition;
				cell.transform.localScale = new Vector3(tileSize, tileSize, cell.transform.localScale.z);

				cells.Add(cell);
			}
		}

		float gridWidth = numCols * tileSize;
		float gridHeight = numRows * tileSize;
		this.transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2) * (Vector2.one + (Vector2)spacing);
	}

	public void ClearGrid() {
		foreach (Cell cell in cells) {
			if (cell != null) {
				if (EditorApplication.isPlaying) {
					Destroy(cell.gameObject);
				}
				else {
					DestroyImmediate(cell.gameObject);
				}
			}
		}
		cells.Clear();
	}

}
