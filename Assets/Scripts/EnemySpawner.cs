using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int MaxEnemies;
    public float RespawnDelay;
    public float MinimumDist = 25f;


    public Transform[] SpawnPoints;
    public Transform EnemyHolder;
    public GameObject EnemyPrefab;

    private List<Enemy> _enemies;
    private GameObject _player;

    public void Intitialize()
    {
        _enemies = new List<Enemy>();
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        int chunkLoad = 10;
        bool allEnemiesSpawned = false;
        int spawned = 0;
        
        while(!allEnemiesSpawned)
        {
            int rnd = UnityEngine.Random.Range(0, SpawnPoints.Length);

            GameObject go = GameObject.Instantiate(EnemyPrefab, SpawnPoints[rnd].position, Quaternion.identity, EnemyHolder);
            Enemy enemy = go.GetComponent<Enemy>();
            enemy.Kill += OnEnemyKilled;
            _enemies.Add(enemy);

            spawned++;

            if(_enemies.Count == MaxEnemies)
            {
                allEnemiesSpawned = true;
                break;
            }

            if(spawned == chunkLoad)
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

    }

    private void OnEnemyKilled(object sender, EventArgs args)
    {
        StartCoroutine(Respawn(sender as Enemy));
    }

   
    private IEnumerator Respawn(Enemy e)
    {
        yield return new WaitForSeconds(RespawnDelay);

        Transform t = GetSpawnPoint();

        e.transform.position = t.position;

        yield return new WaitForEndOfFrame();
       e.gameObject.SetActive(true);
        e.Respawn();
    }

    private Transform GetSpawnPoint()
    {
        Vector3 playerPos = _player.transform.position;
        Transform maxPoint = null;
        List<Transform> possiblePoints = new List<Transform>();
        float maxDist = 0;

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            float dist = Vector3.Distance(playerPos, SpawnPoints[i].position);
            if(dist > maxDist)
            {
                maxDist = dist;
                maxPoint = SpawnPoints[i];
            }
            if (dist > MinimumDist)
            {
                possiblePoints.Add(SpawnPoints[i]);
            }
        }

        if(possiblePoints.Count == 0)
        {
            //Debug.Log("** no point of ineterst");
            return maxPoint;
        }

        //Debug.Log("*** max: " + maxDist + " count: " + possiblePoints.Count);

        int rnd = UnityEngine.Random.Range(0, possiblePoints.Count);
        return possiblePoints[rnd];
    }
}
