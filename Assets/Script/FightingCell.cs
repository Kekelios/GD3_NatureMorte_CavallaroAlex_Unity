using UnityEngine;

public class FightingCell : MonoBehaviour, IActionnable
{
    [SerializeField] private UIFightDialogueController _fightDialogueController;
    [SerializeField] private Board _board;

    private Pawn _currentPawn;
    private const int FIGHT_DC = 15;
    private const int FLEE_DC = 10;

    public void Action(Pawn CurrentPawn)
    {
        _currentPawn = CurrentPawn;
        _fightDialogueController.StartFightDialogue(this);
    }

    public void OnChoiceFight()
    {
        int roll = Random.Range(1, 21);
        Debug.Log($"Combat : Lancé 1D20 = {roll}");

        if (roll > FIGHT_DC)
        {
            Debug.Log("Victoire ! Le minotaure est vaincu !");
            _fightDialogueController.ShowResult("Victoire ! Tu as vaincu le minotaure !");
        }
        else
        {
            Debug.Log("Défaite... Le minotaure t'a vaincu.");
            _fightDialogueController.ShowResult("Défaite... Tu es mort. Partie terminée.");
            GameOver();
        }
    }

    public void OnChoiceGladiator()
    {
        Debug.Log("Système de personnage pas encore implémenté !");
        _fightDialogueController.ShowResult("Tu n'as pas encore de gladiateur...");
    }

    public void OnChoiceFlee()
    {
        int roll = Random.Range(1, 21);
        Debug.Log($"Fuite : Lancé 1D20 = {roll}");

        if (roll > FLEE_DC)
        {
            Cell currentCell = _board.GetCellByNumber(_currentPawn.GetCurrentCellNumber());

            if (currentCell.NextCells.Length > 0)
            {
                int randomIndex = Random.Range(0, currentCell.NextCells.Length);
                Cell randomCell = currentCell.NextCells[randomIndex];

                Debug.Log("Fuite réussie ! Tu t'échappes dans une direction aléatoire.");
                _fightDialogueController.ShowResult("Tu réussis à fuir !");

                _currentPawn.MoveToSelectedCell(randomCell);
            }
            else
            {
                Debug.Log("Pas de sortie possible !");
                _fightDialogueController.ShowResult("Pas de sortie ! Tu es forcé de combattre.");
                OnChoiceFight();
            }
        }
        else
        {
            Debug.Log("Fuite échouée ! Tu es forcé de combattre.");
            _fightDialogueController.ShowResult("Fuite échouée ! Le minotaure te rattrape.");
            OnChoiceFight();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
    }
}
