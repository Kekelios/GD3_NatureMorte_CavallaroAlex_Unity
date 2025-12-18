using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Board _board;
    [SerializeField] private PathSelector _pathSelector;

    private void Start()
    {
        MoveToCell();
    }

    private void MoveToCell()
    {
        Transform newPos = _board.GetCellByNumber(_playerData._cellNumber).transform;
        transform.position = newPos.position;
        transform.rotation = newPos.rotation;
    }

    public void TryMoving(int value)
    {
        Cell currentCell = _board.GetCellByNumber(_playerData._cellNumber);
        List<Cell> reachableCells = _board.GetReachableCells(currentCell, value);

        if (reachableCells.Count == 0)
        {
            Debug.LogWarning("No reachable cells found!");
            return;
        }

        if (reachableCells.Count == 1)
        {
            MoveToSelectedCell(reachableCells[0]);
        }
        else
        {
            _pathSelector.ShowAvailablePaths(reachableCells);
        }
    }

    public void MoveToSelectedCell(Cell targetCell)
    {
        UpdateCellNumber(targetCell);
        MoveToCell();
        ActivateCell();
    }

    private void UpdateCellNumber(Cell targetCell)
    {
        for (int i = 0; i < _board.GetCellCount(); i++)
        {
            if (_board.GetCellByNumber(i) == targetCell)
            {
                _playerData._cellNumber = i;
                return;
            }
        }
    }

    private void ActivateCell()
    {
        Cell cell = _board.GetCellByNumber(_playerData._cellNumber);
        cell.Activate(CurrentPawn: this);
    }
}
