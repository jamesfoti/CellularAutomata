using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {

	public Cell cell;

	[SerializeField]
	private int numRows;
	[SerializeField]
	private int numCols;
	[SerializeField]
	private float tileSize;
	[SerializeField]
	private float distanceOffset;

	private Color[] colors;

	private void Start() {

		colors = new Color[2];
		colors[0] = Color.white;
		colors[1] = Color.green;

		GenerateGrid();
	}

	private void GenerateGrid() {
		for (int row = 0; row < numRows; row++) {
			for (int col = 0; col < numCols; col++) {

				float xCord = col * tileSize;
				float yCord = row * -tileSize;
				Vector2 cellPosition = new Vector2(xCord, yCord);

				Cell cell = Instantiate(this.cell, parent: this.transform);
				cell.transform.localPosition = cellPosition;
				cell.material.color = Color.red;
				cell.transform.localScale = new Vector3(tileSize - distanceOffset, tileSize - distanceOffset, tileSize - distanceOffset);
			}
		}

		float gridWidth = numCols * tileSize;
		float gridHeight = numRows * tileSize;
		this.transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2);
	}

}
