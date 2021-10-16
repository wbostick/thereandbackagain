using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEnemy : MonoBehaviour
{
    List<GameObject> enemiesToAlert = new List<GameObject>();

    private void Start() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 7.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy") {
                enemiesToAlert.Add(hitCollider.gameObject);
            }
        }
    }

    public void AddEnemy(GameObject enemy) {
        enemiesToAlert.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) {
        enemiesToAlert.Remove(enemy);
    }

    public void AlertEnemies() {
        foreach (GameObject enemy in enemiesToAlert) {
            if (enemy != null) {
                enemy.GetComponent<AggroOnSight>().ForceAggro();
            }
        }

    }

}
