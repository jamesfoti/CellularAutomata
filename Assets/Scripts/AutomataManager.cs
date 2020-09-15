using System;
using UnityEngine;

public class AutomataManager : MonoBehaviour {

	public Color aliveColor;
	public Color deadColor;
	public float colorLerpTime;
	public float randomFillPercent;
	public string seed;
	public bool useRandomSeed;

	private GridManager grid;

	private void Awake() {
		grid = GetComponent<GridManager>();
	}

	public void ResetGrid() {
		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = Cell.States.Dead;
			grid.listCells[i].nextState = Cell.States.Dead;
		}
		UpdateGridColors();
	}

	public void RandomFillGrid(bool useRandomSeed, string seed, int randomFillPercent) {
		if (useRandomSeed) {
			seed = Time.time.ToString();
		}

		//System.Random prng = new System.Random(seed.GetHashCode());

		for (int i = 0; i < grid.listCells.Count; i++) {
			/*Array values = Enum.GetValues(typeof(Cell.States));
			Cell.States randomState = (Cell.States)values.GetValue(UnityEngine.Random.Range(0, values.Length));*/
			if (UnityEngine.Random.Range(0, 100) < randomFillPercent) {
				grid.listCells[i].currentState = Cell.States.Alive;
			}
			else {
				grid.listCells[i].currentState = Cell.States.Dead;
			}

			//grid.listCells[i].currentState = randomState;
			
		}
		UpdateGridColors();
	}

	public void Life() {
		// Rule set B3/S23
		for (int i = 0; i < grid.listCells.Count; i++) {
		
			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3) {
					// Born again. 
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					// Cell dies because there is not exactly 3 live neighbors. 
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			else if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 2 || livingNeighbors == 3) {
					// Lives on to the next generation.
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					// Cell dies due to underpopulation or overpopulation.
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void HighLife() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors < 2) {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
				if (livingNeighbors == 2 || livingNeighbors == 3) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				if (livingNeighbors > 3) {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				if (livingNeighbors == 6) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}

			}
		}
		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}
		UpdateGridColors(lerp: true);
	}

	public void Seeds() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Dead && livingNeighbors == 2) {
				grid.listCells[i].nextState = Cell.States.Alive;
			}
			else {
				grid.listCells[i].nextState = Cell.States.Dead;
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}
		UpdateGridColors(lerp: true);
	}

	public void DayAndNight() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 3 || livingNeighbors == 4 || livingNeighbors == 6 || livingNeighbors == 7 || livingNeighbors == 8) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3 || livingNeighbors == 6 || livingNeighbors == 7 || livingNeighbors == 8) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}

			}
		}
		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}
		UpdateGridColors(lerp: true);
	}

	public void Diamoeba() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 5 || livingNeighbors == 6 || livingNeighbors == 7 || livingNeighbors == 8) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3 || livingNeighbors == 5 || livingNeighbors == 6 || livingNeighbors == 7 || livingNeighbors == 8) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}

			}
		}
		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}
		UpdateGridColors(lerp: true);
	}

	public void Flock() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 1 || livingNeighbors == 2) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void Maze() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors >= 1 && livingNeighbors <= 5) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void Mazectric() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors >= 1 && livingNeighbors <= 4) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void Gems() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 4 || livingNeighbors == 5 || livingNeighbors == 6 || livingNeighbors == 8) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3 || livingNeighbors == 4 || livingNeighbors == 5 || livingNeighbors == 7) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void Flakes() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors >= 0  && livingNeighbors <= 8) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void LongLife() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 5) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors >= 3 && livingNeighbors <= 5) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}

	public void Stains() {
		for (int i = 0; i < grid.listCells.Count; i++) {

			int livingNeighbors = GetNearbyNeighbors(grid.listCells[i].rowPos, grid.listCells[i].colPos);

			if (grid.listCells[i].currentState == Cell.States.Alive) {
				if (livingNeighbors == 2 || livingNeighbors == 3 || (livingNeighbors >= 5 && livingNeighbors <= 8)) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
			if (grid.listCells[i].currentState == Cell.States.Dead) {
				if (livingNeighbors == 3   || (livingNeighbors >= 6 && livingNeighbors <= 8)) {
					grid.listCells[i].nextState = Cell.States.Alive;
				}
				else {
					grid.listCells[i].nextState = Cell.States.Dead;
				}
			}
		}

		for (int i = 0; i < grid.listCells.Count; i++) {
			grid.listCells[i].currentState = grid.listCells[i].nextState;
		}

		UpdateGridColors(lerp: true);
	}


	public int GetNearbyNeighbors(int gridRow, int gridCol) {
		// For more info: https://www.youtube.com/watch?v=FWSR_7kZuYg&t=1627s
		int count = 0;
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				int row = (gridRow + i + grid.numRows) % grid.numRows; // mod(numRows) allows wrapping around on the y-axis
				int col = (gridCol + j + grid.numCols) % grid.numCols; // mod(numCols) allows wrapping around on the x-axis

				// Increment if neighbor cell is alive and EXCLUDE the center cell. 
				if (grid.cells[row,col].currentState == Cell.States.Alive && (grid.cells[row, col] != grid.cells[gridRow, gridCol])) {
					count++;
				}
			}
		}

		return count;
	}

	public void UpdateGridColors(bool lerp = false) {
		float lerpTime;
		if (lerp == false) {
			lerpTime = 1f;
		}
		else {
			lerpTime = this.colorLerpTime;
		}

		for (int i = 0; i < grid.listCells.Count; i++) { 
			if (grid.listCells[i].currentState == Cell.States.Alive) {
				grid.listCells[i].render.color = Color.Lerp(grid.listCells[i].render.color, aliveColor, lerpTime);
			}
			else {
				grid.listCells[i].render.color = Color.Lerp(grid.listCells[i].render.color, deadColor, lerpTime);
			}
		}
	}
}