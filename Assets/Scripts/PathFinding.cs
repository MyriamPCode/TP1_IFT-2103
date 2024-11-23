using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public List<Transform> platformNodes = new List<Transform>();
    public string obstacleTag = "Obstacle";
    public string platformTag = "Platforme";

    public Transform FindClosestNode(Vector2 position)
    {
        Transform closestNode = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform node in platformNodes)
        {
            float distance = Vector2.Distance(position, node.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public List<Transform> FindPath(Transform startNode, Transform endNode)
    {
        List<Transform> openList = new List<Transform>();
        HashSet<Transform> closedList = new HashSet<Transform>();
        Dictionary<Transform, Transform> cameFrom = new Dictionary<Transform, Transform>();
        Dictionary<Transform, float> gScore = new Dictionary<Transform, float>();
        Dictionary<Transform, float> fScore = new Dictionary<Transform, float>(); 

        openList.Add(startNode);
        gScore[startNode] = 0;
        fScore[startNode] = Vector2.Distance(startNode.position, endNode.position);

        while (openList.Count > 0)
        {
            Transform currentNode = GetNodeWithLowestFScore(openList, fScore);
            if (currentNode == endNode)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Transform neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor) || IsObstacle(neighbor))
                    continue;

                float tentativeGScore = gScore[currentNode] + Vector2.Distance(currentNode.position, neighbor.position);
                if (!openList.Contains(neighbor)) 
                    openList.Add(neighbor);
                else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor, Mathf.Infinity))
                    continue;

                cameFrom[neighbor] = currentNode;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Vector2.Distance(neighbor.position, endNode.position);
            }
        }

        return new List<Transform>();
    }

    private List<Transform> GetNeighbors(Transform node)
    {
        List<Transform> neighbors = new List<Transform>();

        foreach (Transform platform in platformNodes)
        {
            if (Vector2.Distance(node.position, platform.position) <= 2f)
            {
                neighbors.Add(platform);
            }
        }

        return neighbors;
    }

    private bool IsObstacle(Transform node)
    {
        Collider2D coll = node.GetComponent<Collider2D>();
        return coll != null && coll.CompareTag(obstacleTag);
    }

    private Transform GetNodeWithLowestFScore(List<Transform> openList, Dictionary<Transform, float> fScore)
    {
        Transform lowestFScoreNode = openList[0];
        float lowestFScore = Mathf.Infinity;

        foreach (Transform node in openList)
        {
            float score = fScore.GetValueOrDefault(node, Mathf.Infinity);
            if (score < lowestFScore)
            {
                lowestFScore = score;
                lowestFScoreNode = node;
            }
        }

        return lowestFScoreNode;
    }

    private List<Transform> ReconstructPath(Dictionary<Transform, Transform> cameFrom, Transform currentNode)
    {
        List<Transform> path = new List<Transform>();
        path.Add(currentNode);

        while (cameFrom.ContainsKey(currentNode))
        {
            currentNode = cameFrom[currentNode];
            path.Insert(0, currentNode);
        }

        return path;
    }
}
