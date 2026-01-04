using System.Collections.Generic;
using UnityEngine;

public class PartyHUD : MonoBehaviour
{
    [SerializeField] private Transform _charactersContainer;
    [SerializeField] private GameObject _characterSlotPrefab;

    private List<CharacterSlot> _characterSlots = new List<CharacterSlot>();

    private void Start()
    {
        CreateCharacterSlots();
        RefreshPartyDisplay();
    }

    private void CreateCharacterSlots()
    {

        CharacterType[] allTypes = { CharacterType.MainCharacter, CharacterType.Gladiator, CharacterType.Priest, CharacterType.Sailor };

        foreach (CharacterType type in allTypes)
        {
            GameObject slotObj = Instantiate(_characterSlotPrefab, _charactersContainer);

            CharacterSlot slot = slotObj.GetComponentInChildren<CharacterSlot>();

            if (slot != null)
            {
                CharacterData data = PartyManager.Instance.GetCharacterData(type);
                slot.Initialize(data);
                _characterSlots.Add(slot);
            }
            else
            {

            }
        }

    }

    public void RefreshPartyDisplay()
    {
        foreach (CharacterSlot slot in _characterSlots)
        {
            slot.UpdateDisplay();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefreshPartyDisplay();
        }
    }
}
