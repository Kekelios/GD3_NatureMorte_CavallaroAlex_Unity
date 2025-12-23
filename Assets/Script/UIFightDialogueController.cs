using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFightDialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _fightDialoguePanel;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;

    [SerializeField] private GameObject _choicesPanel;
    [SerializeField] private Button _fightButton;
    [SerializeField] private Button _gladiatorButton;
    [SerializeField] private Button _fleeButton;

    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _continueButton;

    private FightingCell _currentFightingCell;

    private void Start()
    {
        _fightButton.onClick.AddListener(OnFightClicked);
        _gladiatorButton.onClick.AddListener(OnGladiatorClicked);
        _fleeButton.onClick.AddListener(OnFleeClicked);
        _continueButton.onClick.AddListener(OnContinueClicked);
    }

    public void StartFightDialogue(FightingCell fightingCell)
    {
        _currentFightingCell = fightingCell;

        ResetDialogue();

        _titleText.text = "Minotaure";
        _descriptionText.text = "Un minotaure furieux bloque ton chemin ! Que fais-tu ?";

        _fightDialoguePanel.SetActive(true);
    }

    public void ShowResult(string resultMessage)
    {
        _resultText.text = resultMessage;
        _choicesPanel.SetActive(false);
        _resultPanel.SetActive(true);
    }

    private void ResetDialogue()
    {
        _choicesPanel.SetActive(true);
        _resultPanel.SetActive(false);
    }

    private void OnFightClicked()
    {
        _currentFightingCell.OnChoiceFight();
    }

    private void OnGladiatorClicked()
    {
        _currentFightingCell.OnChoiceGladiator();
    }

    private void OnFleeClicked()
    {
        _currentFightingCell.OnChoiceFlee();
    }

    private void OnContinueClicked()
    {
        _fightDialoguePanel.SetActive(false);
    }
}
