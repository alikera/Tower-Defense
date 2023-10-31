using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class Turrent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private string _targetTag;

    [SerializeField] private float fireRate;
    [SerializeField] private float fireCountDown;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform fireHole;
    [SerializeField] private float range;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform partToRotate;
    private void Awake()
    {
        _targetTag = "Enemy";
        range = 13f;
        turnSpeed = 10f;
        fireRate = 1f;
        fireCountDown = 0f;
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget),0,0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_targetTag);
        GameObject nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;
        foreach (var enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (IsInRange(enemyDistance) && IsClosest(enemyDistance, nearestEnemyDistance))
            {
                nearestEnemy = enemy;
                nearestEnemyDistance = enemyDistance;
            }
        }

        target = nearestEnemy;
    }
    
    
    private bool IsInRange(float enemyDistance)
    {
        return enemyDistance < range;
    }
    private bool IsClosest(float enemyDistance, float nearest)
    {
        return enemyDistance < nearest;
    }
    private void Update()
    {
        if(target == null)
            return;
        
        RotateTower();
        if (fireCountDown <= 0)
        {
            ShootEnemy();
            
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    private void RotateTower()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void ShootEnemy()
    {
        GameObject bulletGo = Instantiate(bullet, fireHole.position, fireHole.rotation);
        Bullet firedBullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
            firedBullet.SetTarget(target);
    }
}
