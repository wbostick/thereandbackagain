using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;

    public UnityEvent OnDeath;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die.");
        OnDeath.Invoke();
    }
}
