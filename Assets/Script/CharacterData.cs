using UnityEngine;

public enum CharacterType
{
    MainCharacter,
    Gladiator,
    Priest,
    Sailor
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objectcs/CharacterData")]
public class CharacterData : ScriptableObject 
{
    [Header("informations Générales")]
    public CharacterType characterType;
    public string characterName;

[TextArea(3, 5)]
public string description;

[Header("Visuel")]
public Sprite portrait;
public Color characterColor = Color.white;

[Header("Capacités")]
public bool hasSpecialAbility = false;

[TextArea(2, 3)]
public string abilityDescription;

}
