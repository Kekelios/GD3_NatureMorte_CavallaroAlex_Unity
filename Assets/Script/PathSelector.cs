using System.Collections.Generic;
using UnityEngine;

public class PathSelector : MonoBehaviour
{
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Pawn _pawn;

    private List<Cell> _availableCells;
    private Dictionary<Cell, Material> _originalMaterials = new Dictionary<Cell, Material>();
    private bool _isWaitingForSelection = false;

    public void ShowAvailablePaths(List<Cell> cells)
    {
        _availableCells = cells;
        _isWaitingForSelection = true;

        foreach (Cell cell in cells)
        {
            HighlightCell(cell);
        }
    }

    private void HighlightCell(Cell cell)
    {
        Renderer renderer = cell.GetComponent<Renderer>();
        if (renderer != null)
        {
            _originalMaterials[cell] = renderer.material;
            renderer.material = _highlightMaterial;
        }
    }

    private void Update()
    {
        if (!_isWaitingForSelection) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Cell clickedCell = hit.collider.GetComponent<Cell>();

                if (clickedCell != null && _availableCells.Contains(clickedCell))
                {
                    SelectCell(clickedCell);
                }
            }
        }
    }

    private void SelectCell(Cell selectedCell)
    {
        _isWaitingForSelection = false;
        ClearHighlights();
        _pawn.MoveToSelectedCell(selectedCell);
    }

    private void ClearHighlights()
    {
        foreach (var pair in _originalMaterials)
        {
            Renderer renderer = pair.Key.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = pair.Value;
            }
        }
        _originalMaterials.Clear();
    }
}
