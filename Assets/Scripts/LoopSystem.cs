using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSystem : MonoBehaviour
{
    public static LoopSystem instance;
    List<Transform> EnemySpawnLocations = new List<Transform>();
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject TargetEnemyPrefab;
    List<GameObject> SpawnedEnemies = new List<GameObject>();

    [SerializeField] float cooldown = 4f;
    float startTime;
    private void Awake() {
        instance = this;
        foreach (Transform child in transform) {
            EnemySpawnLocations.Add(child);
        }
        ResetEnemies();
        startTime = Time.time;
    }

    public void EneteredPortal(GameObject portal) {
        if (Time.time - startTime > cooldown) {
            startTime = Time.time;
            GameManager.instance.MovePlayer(portal.GetComponent<Door>().GetPartner().GetTeleportPoint());
            
            ResetEnemies();
        }
    }

    void ResetEnemies() {
        while(SpawnedEnemies.Count > 0) {
            GameObject enemyToRemove = SpawnedEnemies[0];
            SpawnedEnemies.Remove(enemyToRemove);

            Destroy(enemyToRemove);
        }

        int targetEnemyIndex = Random.Range(0, EnemySpawnLocations.Count);
        for (int i = 0; i < EnemySpawnLocations.Count; i++) {
            GameObject spawnedEnemy;
            if (i == targetEnemyIndex) {
                spawnedEnemy = GameObject.Instantiate(TargetEnemyPrefab, EnemySpawnLocations[i].position, Quaternion.identity);
            }
            else {
                spawnedEnemy = GameObject.Instantiate(EnemyPrefab, EnemySpawnLocations[i].position, Quaternion.identity);
            }
            SpawnedEnemies.Add(spawnedEnemy);

        }

    }


}
