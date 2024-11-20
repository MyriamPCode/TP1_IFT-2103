using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Transform target;  // Cible de l'IA (point d'arrivée)
    public float moveSpeed = 5f;  // Vitesse de l'IA
    private Rigidbody2D rb;
    private List<Vector2> path = new List<Vector2>();  // Liste des nœuds du chemin

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindPath(transform.position, target.position);  // Trouver un chemin au démarrage
    }

    private void Update()
    {
        if (path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        Vector2 currentTarget = path[0];  // Le prochain nœud à atteindre
        Vector2 direction = (currentTarget - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Si l'IA atteint le nœud actuel, passe au suivant
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            path.RemoveAt(0);  // Retirer le nœud atteint
        }
    }

    private void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        // A* : listes ouvertes et fermées
        List<Node> openList = new List<Node>();  // Nœuds à explorer
        HashSet<Node> closedList = new HashSet<Node>();  // Nœuds déjà explorés

        Node startNode = new Node(startPos, null, 0, Vector2.Distance(startPos, targetPos));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // Trier les nœuds de la liste ouverte par coût total (f = g + h)
            openList.Sort((a, b) => a.f.CompareTo(b.f));
            Node currentNode = openList[0];
            openList.RemoveAt(0);
            closedList.Add(currentNode);

            // Si la cible est atteinte, reconstruire le chemin
            if ((Vector2)currentNode.position == targetPos)
            {
                ReconstructPath(currentNode);
                return;
            }

            // Explore les voisins du nœud courant
            foreach (Vector2 direction in GetDirections())
            {
                Vector2 neighborPos = currentNode.position + direction;
                
                // Vérifie si le voisin est accessible (pas un obstacle)
                if (IsWalkable(neighborPos) && !closedList.Contains(new Node(neighborPos)))
                {
                    float gCost = currentNode.g + Vector2.Distance(currentNode.position, neighborPos);
                    float hCost = Vector2.Distance(neighborPos, targetPos);
                    Node neighborNode = new Node(neighborPos, currentNode, gCost, hCost);

                    // Si le voisin n'est pas encore dans la liste ouverte, on l'ajoute
                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    private void ReconstructPath(Node targetNode)
    {
        // Reconstruire le chemin en remontant les parents
        List<Vector2> newPath = new List<Vector2>();
        Node currentNode = targetNode;
        while (currentNode != null)
        {
            newPath.Insert(0, currentNode.position);
            currentNode = currentNode.parent;
        }
        path = newPath;  // Mettre à jour le chemin trouvé
    }

    private bool IsWalkable(Vector2 position)
    {
        // Vérifie si la position donnée est accessible (pas un obstacle)
        // Remplace cette logique en fonction de ton environnement
        return !Physics2D.OverlapCircle(position, 0.1f, LayerMask.GetMask("Obstacles"));
    }

    private List<Vector2> GetDirections()
    {
        // Directions possibles (haut, bas, gauche, droite) pour un mouvement sur la plateforme
        return new List<Vector2>
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };
    }

    // Classe représentant un nœud dans l'algorithme A*
    private class Node
    {
        public Vector2 position;
        public Node parent;
        public float g;  // Coût pour atteindre ce nœud
        public float h;  // Heuristique : estimation du coût restant
        public float f => g + h;  // Coût total (g + h)

        public Node(Vector2 position, Node parent, float g, float h)
        {
            this.position = position;
            this.parent = parent;
            this.g = g;
            this.h = h;
        }

        // Comparaison des nœuds pour les ajouter aux listes ouvertes ou fermées
        public override bool Equals(object obj)
        {
            return obj is Node node && position == node.position;
        }

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }
    }
}
