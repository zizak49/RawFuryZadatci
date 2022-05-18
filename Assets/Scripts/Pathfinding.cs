using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	public void FindPath(Tile start, Tile target)
	{
		Debug.Log("start");

		List<Tile> openSet = new List<Tile>();
		HashSet<Tile> closedSet = new HashSet<Tile>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			Tile currentTile = openSet[0];

			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost)
				{
					if (openSet[i].hCost < currentTile.hCost)
						currentTile = openSet[i];
				}
			}

			openSet.Remove(currentTile);
			closedSet.Add(currentTile);

			if (currentTile == target)
			{
				Debug.Log("end");
				RetracePath(start, target);
				return;
			}

			foreach (Tile neighbour in GridManager.Instance.GetTileNeighbours(currentTile))
			{
				if (neighbour.isWall || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, target);
					neighbour.parent = currentTile;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Tile startNode, Tile endNode)
	{
		List<Tile> path = new List<Tile>();
		Tile currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode.ColorVisited();
			currentNode = currentNode.parent;
		}
		Debug.Log(path.Count);
		path.Reverse();
	}

	int GetDistance(Tile tileA, Tile tileB)
	{
		int dstX = Mathf.Abs(tileA.posX - tileB.posX);
		int dstY = Mathf.Abs(tileA.posY - tileB.posY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}
