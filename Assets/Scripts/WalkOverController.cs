using System.Collections.Generic;
using UnityEngine;

public class WalkOverController : MonoBehaviour
{
    [SerializeField] private WordBox wordBox;
    [SerializeField] private SpheresGenerator spheresGenerator;

    public void WalkOverTheWord(string word)
    {
        wordBox.SetWord(word);
        spheresGenerator.GenerateSpheres(CalculateWayPoints());
    }

    private List<Vector3> CalculateWayPoints()
    {
        // Можно составить путь из кривых Безье, но для простоты были просто расставлены ключевые точки на префабах букв
        List<Vector3> wayPoints = new List<Vector3>();
        foreach (var letter in wordBox.currentLetters)
        {
            foreach (Transform point in letter.transform.GetChild(1))
            {
                wayPoints.Add(point.position);
            }
        }

        return wayPoints;
    }
}
