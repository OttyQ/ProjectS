using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFill:MonoBehaviour
{
    private Transform gridParent;
    private GameObject cellPrefab;
    private int _maxDepth;
    private CountHandler _countHandler;
    
    private List<GameObject> cells = new List<GameObject>();

    public void Initialize(Transform gridParent, GameObject cellPrefab, int maxDepth, CountHandler countHandler)
    {
        this.gridParent = gridParent;
        this.cellPrefab = cellPrefab;
        _maxDepth = maxDepth;
        _countHandler = countHandler;
    }

    public void InitializeGrid(int fieldSize)
    {
        ClearGrid();
        for (int i = 0; i < fieldSize * fieldSize; i++)
        {
           GameObject cellObject = Instantiate(cellPrefab, gridParent);
           Cell cell = cellObject.GetComponent<Cell>();
           cell.OnDigged += _countHandler.UseShovel;
           cell.Initialize(_maxDepth, _countHandler);
           cells.Add(cellObject);
        }
    }

    public void ClearGrid()
    {
        foreach (var cell in cells)
        {
            Destroy(cell);
        }
        cells.Clear();
    }
}

