using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereController : MonoBehaviour
{
    [SerializeField] private float speed;
    public UnityEvent<SphereController> OnWalkOverEnded;

    public void WalkOver(List<Vector3> wayPoints)
    {
        StartCoroutine(WalkOverCoroutine(wayPoints));
    }

    private IEnumerator WalkOverCoroutine(List<Vector3> wayPoints)
    {
        foreach(var point in wayPoints)
        {
            while (transform.position != point)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        OnWalkOverEnded?.Invoke(this);
    }
}
