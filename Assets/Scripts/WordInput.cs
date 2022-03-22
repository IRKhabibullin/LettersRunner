using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_InputField))]
public class WordInput : MonoBehaviour
{
    public UnityEvent<string> OnInputUpdate;

    [SerializeField] private SO_Vocabulary vocabulary;
    [SerializeField] private TextMeshProUGUI availableLettersText;
    private TMP_InputField input;

    void Start()
    {
        input = GetComponent<TMP_InputField>();
        input.onValidateInput += delegate (string input, int charIndex, char addedChar) { return vocabulary.ValidateLetter(addedChar); };

        availableLettersText.text = $"Доступные буквы: {string.Join(", ", vocabulary.AvailableLetters())}";
    }

    public void UpdateInput()
    {
        if (string.IsNullOrEmpty(input.text)) return;
        OnInputUpdate?.Invoke(input.text);
    }
}
