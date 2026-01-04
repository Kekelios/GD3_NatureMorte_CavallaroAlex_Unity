using UnityEngine;
using UnityEngine.SceneManagement;

public class FightingCell : MonoBehaviour, IActionnable
{
    [SerializeField] private UIFightDialogueController _fightDialogueController;
    [SerializeField] private Board _board;

    private Pawn _currentPawn;
    private const int FIGHT_DC = 15;
    private const int FLEE_DC = 10;
    private const int GLADIATOR_AUTO_WIN_DC = 5;

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
        if (!PartyManager.Instance.IsCharacterUnlocked(CharacterType.Gladiator))
        {
            Debug.Log("❌ Gladiateur non débloqué !");
            _fightDialogueController.ShowResult("Tu n'as pas encore de gladiateur dans ton équipe...");
            return;
        }

        int roll = Random.Range(1, 21);
        Debug.Log($"Gladiateur : Lancé 1D20 = {roll}");

        if (roll > GLADIATOR_AUTO_WIN_DC)
        {
            Debug.Log(" Le gladiateur écrase le minotaure !");
            _fightDialogueController.ShowResult("Ton gladiateur détruit le minotaure d'un coup puissant !");
        }
        else
        {
            Debug.Log(" Le gladiateur a vaincu le minotaure mais a été blessé.");
            _fightDialogueController.ShowResult("Victoire ! Mais ton gladiateur est blessé...");
        }
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
        Invoke(nameof(ReloadScene), 2f);

    }

    private void ReloadScene() // Remplacer ça plus tard par un "QuitScene" avec un retour au menu principal ou sauvegarde
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
