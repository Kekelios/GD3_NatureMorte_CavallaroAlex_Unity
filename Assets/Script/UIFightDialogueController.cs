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

    private void Awake()
    {
        if (_fightButton != null) _fightButton.onClick.AddListener(OnFightClicked);
        if (_gladiatorButton != null) _gladiatorButton.onClick.AddListener(OnGladiatorClicked);
        if (_fleeButton != null) _fleeButton.onClick.AddListener(OnFleeClicked);
        if (_continueButton != null) _continueButton.onClick.AddListener(OnContinueClicked);
    }

    public void StartFightDialogue(FightingCell fightingCell)
    {
        _currentFightingCell = fightingCell;
        ResetDialogue();

        _titleText.text = "Minotaure";
        _descriptionText.text = "Un minotaure furieux bloque ton chemin ! Que fais-tu ?";

        gameObject.SetActive(true);
    }

    public void ShowResult(string resultMessage)
    {
        _resultText.text = resultMessage;
        _choicesPanel.SetActive(false);
        _resultPanel.SetActive(true);
    }

    public void ResetDialogue()
    {
        _fightDialoguePanel.SetActive(true);
        _choicesPanel.SetActive(true);
        _resultPanel.SetActive(false);
    }

    public void OnFightClicked()
    {
        _currentFightingCell.OnChoiceFight();
    }

    public void OnGladiatorClicked()
    {
        _currentFightingCell.OnChoiceGladiator();
    }

    public void OnFleeClicked()
    {
        _currentFightingCell.OnChoiceFlee();
    }

    public void OnContinueClicked()
    {
        gameObject.SetActive(false);
    }
}
