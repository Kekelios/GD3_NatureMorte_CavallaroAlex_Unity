using UnityEngine;


public class Cell : MonoBehaviour, ICellActivable

{
    [SerializeField] private Cell[] _nextCells;

    public Cell[] NextCells => _nextCells;

    public virtual void Activate(Pawn CurrentPawn)
    {
       if(GetComponent<IActionnable>() != null)
        {
            GetComponent<IActionnable>().Action(CurrentPawn);
        }
    }
}
