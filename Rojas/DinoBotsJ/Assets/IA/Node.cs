using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {
    public bool walkable;               // Si se pude caminar por el node
    public Vector3 worldPosition;       // Posicion en el mundo del nodo
    public int gridX;                   // Indice x del nodo en la malla
    public int gridY;                   // Indice y del nodo en la malla
    public int movementPenalty;

    // Costos mediante heuristicas
    public int gCost;
    public int hCost;
    public Node parent;                 // Referencia al padre del nodo para unir el camino
    int heapIndex;

    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY, int _penalty) {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }

        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}
