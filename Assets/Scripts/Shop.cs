using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject standardTurretPrefab;
    private bool _turretAlreadyPurchased;

    public void Awake()
    {
        _turretAlreadyPurchased = false;
    }

    public void SetTurretPurchased()
    {
        _turretAlreadyPurchased = false;
    }
    public void PurchaseTurret()
    {
        if (_turretAlreadyPurchased == false)
        {
            BuildManager.Instance.SetTowerToBuild(standardTurretPrefab);
            _turretAlreadyPurchased = true;
            Debug.Log("Turret Purchased.");
        }
        else
        {
            _turretAlreadyPurchased = true;
        }
    }
}