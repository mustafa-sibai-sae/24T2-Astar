using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    [SerializeField] Vector3Int startingNodeGridPosition;
    [SerializeField] Vector3Int goalNodeGridPosition;

    List<Node> closedList = new List<Node>();
    List<Node> openList = new List<Node>();

    Grid grid;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        Node startingNode = grid.GetNode(startingNodeGridPosition);
        startingNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 0.25f, 0, 1);

        Node goalNode = grid.GetNode(goalNodeGridPosition);
        goalNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0.25f, 1);

        Node currentNode = startingNode;
        closedList.Add(currentNode);

        List<Node> neighbours = new List<Node>();

        Vector3Int rightNodePosition = currentNode.GridPosition + new Vector3Int(1, 0, 0);
        if (rightNodePosition.x < grid.gridNodeCountX)
        {
            Node rightNode = grid.GetNode(rightNodePosition);
            neighbours.Add(rightNode);
            rightNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

        Vector3Int bottomNodePosition = currentNode.GridPosition + new Vector3Int(0, 0, -1);
        if (bottomNodePosition.z >= 0)
        {
            Node bottomNode = grid.GetNode(bottomNodePosition);
            neighbours.Add(bottomNode);
            bottomNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }

        Vector3Int leftNodePosition = currentNode.GridPosition + new Vector3Int(-1, 0, 0);
        if (leftNodePosition.x >= 0)
        {
            Node leftNode = grid.GetNode(leftNodePosition);
            neighbours.Add(leftNode);
            leftNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }

        Vector3Int topNodePosition = currentNode.GridPosition + new Vector3Int(0, 0, 1);
        if (topNodePosition.z < grid.gridNodeCountZ)
        {
            Node topNode = grid.GetNode(topNodePosition);
            neighbours.Add(topNode);
            topNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }

        for (int i = 0; i < neighbours.Count; i++)
        {
            if (!neighbours[i].IsWalkable || closedList.Contains(neighbours[i]))
            {
                continue;
            }

            int newDistanceCost = CalculateDistance(neighbours[i].GridPosition, currentNode.GridPosition);

            if (newDistanceCost < neighbours[i].GCost || !openList.Contains(neighbours[i]))
            {
                neighbours[i].GCost = newDistanceCost;
                neighbours[i].HCost = CalculateDistance(neighbours[i].GridPosition, goalNode.GridPosition);
                neighbours[i].parent = currentNode;

                if (!openList.Contains(neighbours[i]))
                {
                    openList.Add(neighbours[i]);
                }
            }
        }
    }

    void Update()
    {
    }

    int CalculateDistance(Vector3Int VectorA, Vector3Int VectorB)
    {
        return Mathf.Abs(VectorA.x - VectorB.x + VectorA.y - VectorB.y + VectorA.z - VectorB.z);
    }
}