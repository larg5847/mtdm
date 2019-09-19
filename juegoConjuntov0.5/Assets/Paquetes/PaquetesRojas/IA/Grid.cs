using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGridGizmos;                  // Este boolean nos permite esconder o mostrar la malla en el editor
    public LayerMask unwalkableMask;                // La capa de los objetos que no se puede caminar sobre de ellos
    public Vector2 gridWorldSize;                   // El tamaño de la malla 
    private Vector2 startGridPosition;              // Posicion inicial de la malla
    public float nodeRadius;                        // El radio de los nodos
    public int obstaclePenalty = 50;                // Penalty por caminar cerca de un obstaculo
    public TerrainType[] walkebleRegions;           // Regiones las cuales se puden caminar y su penalidad
    private LayerMask walkableMask;                 // Capa de los objetos que se puede caminar arriba de

    // Diccionario para las penalidad des los diferentes terrenos
    private Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

    private Node[,] grid;                           // Arreglo bidimensional de nodos

    [HideInInspector]
    public float nodeDiameter;                     // Diamaetro de un nodo
    private int gridSizeX, gridSizeY;               // EL numero de nodos en el ejeX y el ejeY

    int penaltyMin = int.MaxValue;                  // Penalidad minima total
    int penaltyMax = int.MinValue;                  // Penalidad maxima total

    public static Grid instance;

    void Awake()
    {
        instance = this;

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        foreach(TerrainType region in walkebleRegions) {
            walkableMask.value |= region.terrainMask.value;
            walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask, 2), region.terrainPenalty);
        }

        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    // Crea la malla de nodos
    void CreateGrid()
    {
        startGridPosition = transform.position;
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(new Vector2(worldPoint.x, worldPoint.y), nodeRadius, unwalkableMask));

                int movementPenalty = 0;
                //if(walkable) {
                    Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(worldPoint.x, worldPoint.y), nodeRadius, walkableMask);
                    for(int i = 0; i < cols.Length; i++) {
                        walkableRegionsDictionary.TryGetValue(cols[0].gameObject.layer, out movementPenalty);
                    }

                if(!walkable) {
                    movementPenalty += obstaclePenalty;
                }

                grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }

        BlurPenaltyMap(1);
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public List<Node> GetSideNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        if(node.gridX + 1 < gridSizeX) 
            neighbours.Add(grid[node.gridX + 1, node.gridY]);

        if(node.gridX - 1 >= 0)
            neighbours.Add(grid[node.gridX - 1, node.gridY]);

        return neighbours;
    }

    // Regresa el nodo ubicado a cierta posicion de la malla
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - startGridPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y - startGridPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void BlurPenaltyMap(int blurSize) {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize - 1) / 2;

        int [,] penaltyHorizontalPass = new int[gridSizeX, gridSizeY];
        int [,] penaltyVerticalPass = new int[gridSizeX, gridSizeY];

        for(int y = 0; y < gridSizeY; y++) {
            for(int x = -kernelExtents; x <= kernelExtents; x++) {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltyHorizontalPass[0, y] += grid[sampleX, y].movementPenalty;
            }

            for(int x = 1; x < gridSizeX; x++) {
                int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, gridSizeX);
                int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX - 1);

                penaltyHorizontalPass[x, y] = penaltyHorizontalPass[x-1, y] - grid[removeIndex, y].movementPenalty + grid[addIndex, y].movementPenalty;
            }
        }

        for(int x = 0; x < gridSizeX; x++) {
            for(int y = -kernelExtents; y <= kernelExtents; y++) {
                int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                penaltyVerticalPass[x, 0] += penaltyHorizontalPass[x, sampleY];
            }

            for(int y = 1; y < gridSizeY; y++) {
                int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeY);
                int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY - 1);

                penaltyVerticalPass[x, y] = penaltyVerticalPass[x, y-1] - penaltyHorizontalPass[x, removeIndex] + penaltyHorizontalPass[x, addIndex];
                int blurredPenalty = Mathf.RoundToInt((float)penaltyVerticalPass[x,y] / (kernelSize * kernelSize));
                grid[x, y].movementPenalty = blurredPenalty;

                if(blurredPenalty > penaltyMax)
                    penaltyMax = blurredPenalty;

                if(blurredPenalty < penaltyMin)
                    penaltyMin = blurredPenalty;
            }
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0.5f));

        if (grid != null && displayGridGizmos)
        {
            foreach (Node node in grid) {
                //Gizmos.color = Color.Lerp(new Color(1, 1, 1, 0.4f), new Color(0, 0, 0, 0.4f), Mathf.InverseLerp(penaltyMin, penaltyMax, node.movementPenalty));
                //Gizmos.color = node.walkable ? Gizmos.color : new Color(1, 0, 0, 0.4f);
                Gizmos.color = node.walkable ? new Color(1, 1, 1, 0.1f) : new Color(1, 0, 0, 0.4f);

                Gizmos.DrawWireCube(node.worldPosition, Vector3.one * (nodeDiameter));
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter));
            }
        }
    }

    [System.Serializable]
    public class TerrainType {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }
}
