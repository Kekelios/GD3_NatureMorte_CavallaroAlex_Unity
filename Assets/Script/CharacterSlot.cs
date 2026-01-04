using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private Image _portraitImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private GameObject _lockedOverlay;

    private CharacterData _characterData;

    public void Initialize(CharacterData data)
    {
        _characterData = data;

        if (_nameText != null)
        {
            _nameText.text = _characterData.characterName;
        }

        if (_backgroundImage != null)
        {
            _backgroundImage.color = _characterData.characterColor;
        }

        if (_portraitImage != null && _characterData.portrait != null)
        {
            _portraitImage.sprite = _characterData.portrait;
        }

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        bool isUnlocked = PartyManager.Instance.IsCharacterUnlocked(_characterData.characterType);

        if (_lockedOverlay != null)
        {
            _lockedOverlay.SetActive(!isUnlocked);
        }

        if (_portraitImage != null)
        {
            Color portraitColor = _portraitImage.color;
            portraitColor.a = isUnlocked ? 1f : 0.3f;
            _portraitImage.color = portraitColor;
        }
    }
}
