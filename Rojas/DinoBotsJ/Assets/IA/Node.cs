using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public bool walkable;               // Si se pude caminar por el node
    public Vector3 worldPosition;       // Posicion en el mundo del nodo
    public int gridX;                   // Indice x del nodo en la malla
    public int gridY;                   // Indice y del nodo en la malla

    // Costos mediante heuristicas
    public int gCost;
    public int hCost;
    public Node parent;                 // Referencia al padre del nodo para unir el camino

    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}
