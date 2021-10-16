using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;

    public UnityEvent OnDeath;

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath.Invoke();
    }
}
