using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;

    public UnityEvent OnDeath;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> {

    }

    public FloatEvent OnDamage;

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        OnDamage.Invoke(currentHealth);
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
