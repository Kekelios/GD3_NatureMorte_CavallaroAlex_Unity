using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] private Button _diceButton;
    [SerializeField] private Color _disabledColor = Color.gray;

    private Color _originalColor;
    private bool _canRoll = true;

    private void Start()
    {
        if (_diceButton != null)
        {
            _originalColor = _diceButton.image.color;
        }
    }

    public void RollTheDice()
    {
        if (!_canRoll)
        {
            Debug.Log("Attendez la fin du déplacement !");
            return;
        }

        _canRoll = false;
        SetButtonState(false);

        int value = Random.Range(1, 5);
        Debug.Log($"Le dé a fait {value}");
        _pawn.TryMoving(value);
    }

    public void EnableDiceRoll()
    {
        _canRoll = true;
        SetButtonState(true);
    }

    private void SetButtonState(bool enabled)
    {
        if (_diceButton != null)
        {
            _diceButton.interactable = enabled;
            _diceButton.image.color = enabled ? _originalColor : _disabledColor;
        }
    }
}
