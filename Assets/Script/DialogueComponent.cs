using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueData;
    private DialogueRow _currentRow;
    private int _currentRowIndex = 0;
    [SerializeField] private UIDialogueController _dialogueController;

    public void Action(Pawn CurrentPawn)
    {
        _currentRow = GetDialogueRow();
        // afficher ma ligne 
        _dialogueController.StartDialogue(this);
    }
    public DialogueRow GetDialogueRow()
    {
        return _dialogueData.rows[_currentRowIndex];
    }

    public string GetDialogueText()
    {
        return _currentRow.longDialogue;
    }

    public string GetCharacterName()
    {
        return _currentRow.characterName;
    }

    public void GetNextRow()
    {
        if (_currentRow.nextRowNumber == -1)
        {
            _dialogueController.EndDialogue();
            ResetDialogue();
        }
        else
        {
            _currentRowIndex = _currentRow.nextRowNumber;
            _currentRow = GetDialogueRow();
            _dialogueController.UpdateText();
        }
    }

    public void ResetDialogue()
    {
        _currentRowIndex = 0;
    }
}   