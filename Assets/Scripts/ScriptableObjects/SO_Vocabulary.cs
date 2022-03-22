using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Vocabulary")]
public class SO_Vocabulary : ScriptableObject
{
    [SerializeField] private List<SO_Letter> letters;

    public List<char> AvailableLetters()
    {
        return letters.Select(x => x.letter).ToList();
    }

    public GameObject GetLetterPrefab(char letter)
    {
        var prefab = letters.Find(x => x.letter == letter);
        return prefab?.letterPrefab;
    }

    public char ValidateLetter(char letter)
    {
        letter = char.ToUpper(letter);
        return AvailableLetters().Contains(letter) ? letter : '\0';
    }
}
