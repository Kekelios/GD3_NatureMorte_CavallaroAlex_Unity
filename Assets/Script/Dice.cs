using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;

    public void RollTheDice()
    {
        int value = Random.Range(1, 6);
        Debug.Log($"Le dé à fait {value}");
        _pawn.TryMoving(value);
    }
}
