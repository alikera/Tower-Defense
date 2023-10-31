using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hitPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        hitPoint = 50f;
    }

    public void DecreaseHitPoint(float amount)
    {
        hitPoint -= amount;
    }

    private void Update()
    {
        if (hitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
