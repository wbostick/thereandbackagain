using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoopSystem : MonoBehaviour
{
    public static LoopSystem instance;
    List<Transform> EnemySpawnLocations = new List<Transform>();

    [System.Serializable]
    public class EnemySpawn {
        public GameObject Enemy;
        public int spawnChance;
    }
    [SerializeField] List<EnemySpawn> EnemySpawns = new List<EnemySpawn>();
    [SerializeField] GameObject TargetEnemyPrefab;
    List<GameObject> SpawnedEnemies = new List<GameObject>();

    [SerializeField] float cooldown = 4f;
    float startTime;

    [SerializeField] int targetEnemiesToCountReached = 5;
    int targetEnemiesKilled = 0;

    public UnityEvent onTargetEnemiesCountReached;

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
            Vector3 teleportPoint = portal.GetComponent<Door>().GetPartner().GetTeleportPoint();
            GameManager.instance.MovePlayer(teleportPoint, (teleportPoint - portal.transform.position), 0.1f);
            
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
            GameObject spawnedEnemy = null;
            if (i == targetEnemyIndex) {
                spawnedEnemy = GameObject.Instantiate(TargetEnemyPrefab, EnemySpawnLocations[i].position, Quaternion.identity);
                spawnedEnemy.GetComponent<Health>().OnDeath.AddListener(targetEnemyDeath);
            }
            else {
            
                int random = Random.Range(0, 100); 
                int lowLim;    
                int hiLim = 0;
                for (int l_enemy = 0; l_enemy < EnemySpawns.Count; l_enemy++)
                {
                    lowLim = hiLim; 
                    hiLim += EnemySpawns[l_enemy].spawnChance; 
                    if (random >= lowLim && random < hiLim)
                    { 
                        spawnedEnemy = GameObject.Instantiate(EnemySpawns[l_enemy].Enemy, EnemySpawnLocations[i].position, Quaternion.identity);
                    }
                }
            }
            SpawnedEnemies.Add(spawnedEnemy);

        }

    }

    void targetEnemyDeath()
    {
        targetEnemiesKilled++;
        if (targetEnemiesToCountReached == targetEnemiesKilled)
        {
            onTargetEnemiesCountReached.Invoke();
        }
    }


}
