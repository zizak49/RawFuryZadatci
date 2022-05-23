using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	public void FindPath(Tile start, Tile target)
	{
		List<Tile> openSet = new List<Tile>();
		HashSet<Tile> closedSet = new HashSet<Tile>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			Tile currentTile = openSet[0];

			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].FCost < currentTile.FCost || openSet[i].FCost == currentTile.FCost)
				{
					if (openSet[i].HCost < currentTile.HCost)
						currentTile = openSet[i];
				}
			}

			openSet.Remove(currentTile);
			closedSet.Add(currentTile);

			if (currentTile == target)
			{
				RetracePath(start, target);
				return;
			}

			foreach (Tile neighbour in currentTile.Neighbours)
			{
				if (closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = currentTile.GCost + GetDistance(currentTile, neighbour);
				if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
				{
					neighbour.GCost = newCostToNeighbour;
					neighbour.HCost = GetDistance(neighbour, target);
					neighbour.Parent = currentTile;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Tile startTile, Tile endTile)
	{
		List<Tile> path = new List<Tile>();
		Tile currentTile = endTile;

		while (currentTile != startTile)
		{
			path.Add(currentTile);
			currentTile.ColorVisited();
			currentTile = currentTile.Parent;
		}
		path.Reverse();

		path[0].ColorStart();
		path[path.Count - 1].ColorEnd();
	}

	// Manhattan distance
	int GetDistance(Tile tileA, Tile tileB)
	{
		int dstX = Mathf.Abs(tileA.PosX - tileB.PosX);
		int dstY = Mathf.Abs(tileA.PosY - tileB.PosY);

		return dstX + dstY;
	}
}
