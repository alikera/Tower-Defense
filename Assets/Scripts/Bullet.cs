using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float power;
    public void SetTarget(GameObject target)
    {
        enemy = target;
    }
    private void Awake()
    {
        smoothSpeed = 20f;
        power = 10f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        if (distanceToEnemy >= 1f)
        {
            Vector3 smoothedPosition =
                Vector3.Lerp(transform.position, enemy.transform.position, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            transform.LookAt(enemy.transform);
        }
        else
        {
            Destroy(gameObject);
            enemy.GetComponent<Enemy>().DecreaseHitPoint(power);
        }
    }
}
