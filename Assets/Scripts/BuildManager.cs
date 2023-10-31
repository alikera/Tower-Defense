using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private GameObject _towerToBuild;
    public void Awake()
    {
        if (Instance != null) 
        {
            Debug.Log("BuildManager already exists!");
            return;
        }
        Instance = this;
    }

    public GameObject GetTowerToBuild()
    {
        return _towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        _towerToBuild = tower;
    }
    
}
