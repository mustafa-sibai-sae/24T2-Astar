using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Node : IComparable
{
    public Node parent;
    public GameObject NodeGameObject { get; private set; }

    public bool isVisited = false;
    public int versionNumber;
    
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

#if ASTAR_DEBUG
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
#endif
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

#if ASTAR_DEBUG
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
#endif
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

#if ASTAR_DEBUG
        nodeGameObject.transform.
        GetChild(0).
        GetChild(3).
        GetComponent<TMP_Text>().text = gridPosition.ToString();
#endif
    }

    public int CompareTo(object obj)
    {
        Node comingInNode = (Node)obj;

        if (FCost < ((Node)obj).FCost)
            return -1;
        else if (FCost > comingInNode.FCost)
            return 1;
        else
            return 0;
    }
}