using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    [SerializeField] Vector3Int startingNodeGridPosition;
    [SerializeField] Vector3Int goalNodeGridPosition;

    Grid grid;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        Node startingNode = grid.GetNode(startingNodeGridPosition);
        startingNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 0.25f, 0, 1);

        Node goalNode = grid.GetNode(goalNodeGridPosition);
        goalNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0.25f, 1);

        Node currentNode = startingNode;

        Vector3Int rightNodePosition = currentNode.GridPosition + new Vector3Int(1, 0, 0);
        if (rightNodePosition.x < grid.gridNodeCountX)
        {
            Node rightNode = grid.GetNode(rightNodePosition);
            rightNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

        Vector3Int bottomNodePosition = currentNode.GridPosition + new Vector3Int(0, 0, -1);
        if (bottomNodePosition.z >= 0)
        {
            Node bottomNode = grid.GetNode(bottomNodePosition);
            bottomNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }

        Vector3Int leftNodePosition = currentNode.GridPosition + new Vector3Int(-1, 0, 0);
        if (leftNodePosition.x >= 0)
        {
            Node leftNode = grid.GetNode(leftNodePosition);
            leftNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }

        Vector3Int topNodePosition = currentNode.GridPosition + new Vector3Int(0, 0, 1);
        if (topNodePosition.z < grid.gridNodeCountZ)
        {
            Node topNode = grid.GetNode(topNodePosition);
            topNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    void Update()
    {

    }
}