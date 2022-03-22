using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBox : MonoBehaviour
{
    [SerializeField] private SO_Vocabulary vocabulary;

    [Tooltip("Space between letters")]
    [SerializeField] private float lettersKerning;

    public List<GameObject> currentLetters { get; private set; }

    private void Start()
    {
        currentLetters = new List<GameObject>();
    }

    public void SetWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return;

        ClearCurrentWord();
        DrawWord(word, GetWordStartPosition(word));

        StopAllCoroutines();
        StartCoroutine(AlignCameraToFitWord(word));
    }

    private void DrawWord(string word, float wordStartPosition)
    {
        for (var i = 0; i < word.Length; i++)
        {
            var letterPrefab = vocabulary.GetLetterPrefab(word[i]);
            if (letterPrefab != null)
            {
                var letter = Instantiate(letterPrefab, new Vector3(wordStartPosition + lettersKerning * i, 0, 0), letterPrefab.transform.rotation);
                letter.transform.parent = transform;
                currentLetters.Add(letter);
            }
        }
    }

    private float GetWordStartPosition(string word)
    {
        return -lettersKerning * (word.Length - 1) / 2.0f;
    }

    public void ClearCurrentWord()
    {
        currentLetters.Clear();
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator AlignCameraToFitWord(string word)
    {
        Camera camera = Camera.main;
        var newCameraSize = Mathf.Max(word.Length * (lettersKerning / 2) / Camera.main.aspect, (lettersKerning / 2));

        float startTime = Time.time;
        while (camera.orthographicSize != newCameraSize)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newCameraSize, Mathf.Pow((Time.time - startTime), 1f));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
