using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    [SerializeField] private List<CharacterData> _allCharacters = new List<CharacterData>();

    private HashSet<CharacterType> _unlockedCharacters = new HashSet<CharacterType>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UnlockCharacter(CharacterType.MainCharacter);
    }

    public void UnlockCharacter(CharacterType type)
    {
        if (_unlockedCharacters.Add(type))
        {
            Debug.Log($"✅ Personnage débloqué : {type}");

            PartyHUD partyHUD = FindFirstObjectByType<PartyHUD>();
            if (partyHUD != null)
            {
                partyHUD.RefreshPartyDisplay();
            }
        }
    }

    public bool IsCharacterUnlocked(CharacterType type)
    {
        return _unlockedCharacters.Contains(type);
    }

    public CharacterData GetCharacterData(CharacterType type)
    {
        return _allCharacters.Find(c => c.characterType == type);
    }

    public List<CharacterData> GetUnlockedCharactersData()
    {
        List<CharacterData> unlocked = new List<CharacterData>();
        foreach (var character in _allCharacters)
        {
            if (IsCharacterUnlocked(character.characterType))
            {
                unlocked.Add(character);
            }
        }
        return unlocked;
    }

    public void ResetParty()
    {
        _unlockedCharacters.Clear();
        UnlockCharacter(CharacterType.MainCharacter);
        Debug.Log("🔄 Équipe réinitialisée");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UnlockCharacter(CharacterType.Gladiator);
            Debug.Log("🎮 Gladiateur débloqué via touche G !");
        }
    }
}
