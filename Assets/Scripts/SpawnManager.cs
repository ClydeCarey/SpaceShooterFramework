using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _crazyAntPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject[] secondaryPowerups;
    public GameObject wave2Text;
    public GameObject wave3Text;
    public GameObject bossWaveText;

    Player player;

    int count = 0;

    private bool _stopSpawning = false;

    
    public void StartSpawning()
    {
        //StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnCrazyAntRoutine());
        WaveOne();
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnSecondaryFireRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void WaveOne()
    {
        
        StartCoroutine(IncrementalSpawnEnemyRoutine());
        
    }

    private void WaveTwo()
    {
        wave2Text.SetActive(false);
        StartCoroutine(IncrementalSpawnEnemyRoutine2());
    }

    private void WaveThree()
    {
        wave3Text.SetActive(false);
        StartCoroutine(IncrementalSpawnEnemyRoutine3());
    }

    private void BossWave()
    {
        wave3Text.SetActive(false);
        StartCoroutine(BossWaveRoutine());
    }

    IEnumerator IncrementalSpawnEnemyRoutine()
    {
        int count = 0;
        while (count < 6)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
            count++;
        }

        yield return new WaitForSeconds(5.0f);

        wave2Text.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        WaveTwo();
    }

    IEnumerator IncrementalSpawnEnemyRoutine2()
    {
        int count = 0;
        while (count < 6)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            Vector3 posToSpawnCA = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemyCA = Instantiate(_crazyAntPrefab, posToSpawnCA, Quaternion.identity);
            newEnemyCA.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
            count++;
        }

        yield return new WaitForSeconds(5.0f);

        wave3Text.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        WaveThree();
    }

    IEnumerator IncrementalSpawnEnemyRoutine3()
    {
        int count = 0;
        while (count < 12)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            Vector3 posToSpawnCA = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemyCA = Instantiate(_crazyAntPrefab, posToSpawnCA, Quaternion.identity);
            newEnemyCA.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
            count++;
        }

        bossWaveText.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        BossWave();
    }

    IEnumerator BossWaveRoutine()
    {
        int count = 0;
        while (count < 20)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            Vector3 posToSpawnCA = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemyCA = Instantiate(_crazyAntPrefab, posToSpawnCA, Quaternion.identity);
            newEnemyCA.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(3.0f);
            count++;
        }

        player.invidActive = true;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }
    }

    IEnumerator SpawnCrazyAntRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_crazyAntPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(7.0f);

        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            int randomPowerUp = Random.Range(0, 6);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    IEnumerator SpawnSecondaryFireRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            int secondaryPowerupID = 0; //Random.Range(0, 6);
            Instantiate(secondaryPowerups[secondaryPowerupID], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(45.0f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
