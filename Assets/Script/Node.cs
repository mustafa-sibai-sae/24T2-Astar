using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node
{
    public Node parent;
    public GameObject NodeGameObject { get; private set; }

    bool isWalkable;
    public bool IsWalkable
    {
        get
        {
            return isWalkable;
        }

        private set
        {
            isWalkable = value;
        }
    }

    int gCost;
    public int GCost
    {
        get
        {
            return gCost;
        }

        set
        {
            gCost = value;

            NodeGameObject.
                transform.
                GetChild(0).
                GetChild(0).
                GetComponent<TMP_Text>().text = gCost.ToString();

            NodeGameObject.
                transform.
                GetChild(0).
                GetChild(2).
                GetComponent<TMP_Text>().text = FCost.ToString();
        }
    }

    int hCost;
    public int HCost
    {
        get
        {
            return hCost;
        }

        set
        {
            hCost = value;

            NodeGameObject.
                transform.
                GetChild(0).
                GetChild(1).
                GetComponent<TMP_Text>().text = hCost.ToString();

            NodeGameObject.
                transform.
                GetChild(0).
                GetChild(2).
                GetComponent<TMP_Text>().text = FCost.ToString();
        }
    }

    public int FCost
    {
        get
        {
            return gCost + HCost;
        }
    }

    public Vector3Int GridPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }

    public Node(Vector3Int gridPosition, Vector3 worldPosition, bool isWalkable, GameObject nodeGameObject)
    {
        GridPosition = gridPosition;
        WorldPosition = worldPosition;
        NodeGameObject = nodeGameObject;
        IsWalkable = isWalkable;

        nodeGameObject.transform.
        GetChild(0).
        GetChild(3).
        GetComponent<TMP_Text>().text = gridPosition.ToString();
    }
}