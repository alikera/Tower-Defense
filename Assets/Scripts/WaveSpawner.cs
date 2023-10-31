using UnityEngine;
using System.Collections;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private float enemyDelay = 0.3f;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI nextWaveComing;

    private int _waveNumber = 0;
    private float _countDown = 5f;
    
    private void Update()
    {
        if (_countDown <= 0)
        {
            _waveNumber++;
            StartCoroutine(SpawnEnemies());
            _countDown = spawnTime;
        }
        ShowTimerCountDown();
        _countDown -= Time.deltaTime;
    }
    private void ShowTimerCountDown()
    {
        timer.text = (Mathf.Floor(_countDown)).ToString();
        if (_countDown <= 1)
        {
            timer.text = "Now!";
        }
        if (_countDown <= 6)
        {
            timer.gameObject.SetActive(true);
            nextWaveComing.gameObject.SetActive(true);
        }
        else
        {
            timer.gameObject.SetActive(false);
            nextWaveComing.gameObject.SetActive(false);
        }
        
    }
    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < _waveNumber; i++)
        {
            Instantiate(enemy, startPoint.position, startPoint.rotation);
            yield return new WaitForSeconds(enemyDelay);
        }
    }
}
