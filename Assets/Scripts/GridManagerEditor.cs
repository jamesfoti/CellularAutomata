using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor {

	public override void OnInspectorGUI() {
		GridManager gridManager = (GridManager)target;

		if (DrawDefaultInspector()) {
			if (gridManager.autoUpdate) {
				gridManager.GenerateGrid();
			}
		}

		if (GUILayout.Button("Generate Grid")) {
			gridManager.GenerateGrid();
		}

		if (GUILayout.Button("Clear Grid")) {
			
		}
	}
}