using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    [SerializeField] Vector3Int startingNodeGridPosition;
    [SerializeField] Vector3Int goalNodeGridPosition;

    List<Node> openList = new List<Node>();
    List<Node> finalPath = new List<Node>();
    List<Node> neighbours = new List<Node>();

    Grid grid;
    Node startingNode;
    Node goalNode;

    static int globalVersionNumber = 0;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    void FindPath(Node node)
    {
        if (node.parent != null)
        {
            finalPath.Add(node);
            FindPath(node.parent);
        }
        else
        {
            finalPath.Add(node);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startingNode = grid.GetNode(startingNodeGridPosition);
            goalNode = grid.GetNode(goalNodeGridPosition);

            globalVersionNumber++;
#if ASTAR_DEBUG
            startingNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 0.25f, 0, 1);
            goalNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0.25f, 1);
#endif

            Node currentNode = startingNode;
            openList.Add(currentNode);

            while (openList.Count > 0)
            {
                openList.Sort();
                currentNode = openList[0];

                if (currentNode.versionNumber < globalVersionNumber)
                {
                    currentNode.GCost = 0;
                    currentNode.HCost = 0;
                    currentNode.isVisited = false;
                    currentNode.versionNumber = globalVersionNumber;
                }

                openList.Remove(currentNode);
                currentNode.isVisited = true;

                if (currentNode == goalNode)
                {
                    FindPath(currentNode);
                    finalPath.Reverse();
                    print("Found Path");

#if ASTAR_DEBUG
                    for (int i = 0; i < finalPath.Count; i++)
                    {
                        finalPath[i].NodeGameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
                    }
#endif
                    return;
                }

                neighbours.Clear();

                Vector3Int rightNodePosition = currentNode.GridPosition + new Vector3Int(1, 0, 0);
                if (rightNodePosition.x < grid.gridNodeCountX)
                {
                    Node rightNode = grid.GetNode(rightNodePosition);
                    neighbours.Add(rightNode);
                    //rightNode.NodeGameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                }

                Vector3Int bottomNodePosition = currentNode.GridPosition + new Vector3Int(0, 0, -1);
                if (bottomNodePosition.z >= 0)
                {
                    Node bottomNode = grid.GetNode(bottomNodePosition);
                    neighbours.Add(bottomNode);
                    //bottomNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.magenta;
                }

                Vector3Int leftNodePosition = currentNode.GridPosition + new Vector3Int(-1, 0, 0);
                if (leftNodePosition.x >= 0)
                {
                    Node leftNode = grid.GetNode(leftNodePosition);
                    neighbours.Add(leftNode);
                    //leftNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.cyan;
                }

                Vector3Int topNodePosition = currentNode.GridPosition + new Vector3Int(0, 0, 1);
                if (topNodePosition.z < grid.gridNodeCountZ)
                {
                    Node topNode = grid.GetNode(topNodePosition);
                    neighbours.Add(topNode);
                    //topNode.NodeGameObject.GetComponent<Renderer>().material.color = Color.yellow;
                }

                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (neighbours[i].versionNumber < globalVersionNumber)
                    {
                        neighbours[i].GCost = 0;
                        neighbours[i].HCost = 0;
                        neighbours[i].isVisited = false;
                        neighbours[i].versionNumber = globalVersionNumber;
                    }

                    if (!neighbours[i].IsWalkable || neighbours[i].isVisited)
                    {
                        continue;
                    }

                    int newDistanceCost = CalculateDistance(neighbours[i].GridPosition, currentNode.GridPosition) + currentNode.GCost;

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
        }
    }

    int CalculateDistance(Vector3Int VectorA, Vector3Int VectorB)
    {
        return Mathf.Abs(VectorA.x - VectorB.x + VectorA.y - VectorB.y + VectorA.z - VectorB.z);
    }
}