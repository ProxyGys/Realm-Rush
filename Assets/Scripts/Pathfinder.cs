using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    [SerializeField] bool isRunning = true;
    
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
    }

    void Pathfind()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            print("S" + searchCenter);
            HaltIfEndFound(searchCenter);
            ExploreNeighbours(searchCenter);
            searchCenter.isExplored = true;
        }

        print("F");
    }

    private void HaltIfEndFound(Waypoint serrchCenter)
    {
        if (serrchCenter == endWaypoint)
        {
            print("E");
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = from.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
            catch
            {
                //do nothing
            }
        }

    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored)
        {
            // do nothing
        }
        else
        {
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            print("Q" + neighbour);
        }

    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("skip" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

}
