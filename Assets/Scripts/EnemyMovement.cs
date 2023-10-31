using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private int _destIndex;

    private void Awake()
    {
        speed = 8f;
        _destIndex = 0;
    }

    void FixedUpdate()
    {
        if (_destIndex == Waypoints.Points.Length)
        {
            Destroy(gameObject);
            return;
        }
        Transform target = Waypoints.Points[_destIndex];
        if ((transform.position - target.position).magnitude >= 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        }
        else
        {
            _destIndex++;
        }
    }
}
