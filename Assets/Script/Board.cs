using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    public Cell GetCellByNumber(int number)
    {
        return _cells[number];
    }

    public int GetNextCellToMove(int cellNumber)
    {
        return cellNumber % _cells.Length;
    }
    
    public int GetCellCount()
    {
        return _cells.Length;
    }

    public List<Cell> GetReachableCells(Cell startCell, int steps)
    {
        List<Cell> reachableCells = new List<Cell>();

        if (steps == 0)
        {
            reachableCells.Add(startCell);
            return reachableCells;
        }

        if (startCell.NextCells == null || startCell.NextCells.Length == 0)
        {
            return reachableCells;
        }

        foreach (Cell nextCell in startCell.NextCells)
        {
            List<Cell> cellsFromNext = GetReachableCells(nextCell, steps - 1);
            foreach (Cell cell in cellsFromNext)
            {
                if (!reachableCells.Contains(cell))
                {
                    reachableCells.Add(cell);
                }
            }
        }

        return reachableCells;
    }
}
