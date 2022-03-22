using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresGenerator : MonoBehaviour
{
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private float spheresGenerationPeriod = 1f;

    private Stack<SphereController> spheresPool;

    private void Start()
    {
        spheresPool = new Stack<SphereController>();
    }

    public void ReturnSphereToPool(SphereController sphere)
    {
        sphere.gameObject.SetActive(false);
        spheresPool.Push(sphere);
    }

    private SphereController GetSphereFromPool()
    {
        SphereController sphere;
        if (spheresPool.Count == 0)
        {
            var sphereObject = Instantiate(spherePrefab, transform);
            sphere = sphereObject.GetComponent<SphereController>();
            sphere.OnWalkOverEnded.AddListener(ReturnSphereToPool);
            return sphere;
        }
        sphere = spheresPool.Pop();
        sphere.gameObject.SetActive(true);
        return sphere;
    }

    public void GenerateSpheres(List<Vector3> wayPoints)
    {
        StopAllCoroutines();
        ClearSpheres();
        StartCoroutine(GenerateSpheresCoroutine(wayPoints));
    }

    private IEnumerator GenerateSpheresCoroutine(List<Vector3> wayPoints)
    {
        while (true)
        {
            LaunchSphere(wayPoints);
            yield return new WaitForSeconds(spheresGenerationPeriod);
        }
    }

    private void LaunchSphere(List<Vector3> wayPoints)
    {

        var sphere = GetSphereFromPool();
        sphere.transform.position = wayPoints[0];
        sphere.WalkOver(wayPoints);
    }

    private void ClearSpheres()
    {
        spheresPool.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
