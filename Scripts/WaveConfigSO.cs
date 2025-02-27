using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawn = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawn - spawnTimeVariance, 
            timeBetweenEnemySpawn + spawnTimeVariance
            );

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
    public GameObject GetEnemyPrefab(int index) { return enemyPrefabs[index]; }
    public int GetEnemyCount() { return enemyPrefabs.Count; }
    public Transform GetStartingWaypoint() { return pathPrefab.GetChild(0); }
    public float GetMoveSpeed() { return moveSpeed; }
}
