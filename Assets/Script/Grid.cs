using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;
    [SerializeField] public int gridNodeCountX;
    [SerializeField] public int gridNodeCountZ;

    [SerializeField] int NodeWidth;
    [SerializeField] int NodeHeight;
    int gridArraySize;

    Node[] nodes;

    void Start()
    {
        gridArraySize = gridNodeCountX * gridNodeCountZ;

        nodes = new Node[gridArraySize];

        for (int z = 0; z < gridNodeCountZ; z++)
        {
            for (int x = 0; x < gridNodeCountX; x++)
            {
                int i = x + z * gridNodeCountX;

                Vector3Int gridPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = new Vector3(x * NodeWidth, 0, z * NodeHeight);

                GameObject go = Instantiate(nodePrefab, worldPosition, Quaternion.identity);
                go.transform.localScale = new Vector3(NodeWidth, 1, NodeHeight);

                bool isWalkable = !Physics.CheckBox(worldPosition, new Vector3(NodeWidth / 2.0f, 0, NodeHeight / 2.0f));

                nodes[i] = new Node(gridPosition, worldPosition, isWalkable, go);

                if (!nodes[i].IsWalkable)
                {
                    nodes[i].NodeGameObject.GetComponent<Renderer>().material.color = new Color(0.45f, 0, 0, 1);
                }
            }
        }
    }

    public Node GetNode(Vector3Int GridPosition)
    {
        int i = GridPosition.x + GridPosition.z * gridNodeCountX;
        return nodes[i];
    }

    void Update()
    {

    }
}