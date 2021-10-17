using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;

    public UnityEvent OnDeath;

    [SerializeField] private AudioClip m_DamageSound;
    [SerializeField] private AudioClip m_DeathSound;
    private AudioSource m_AudioSource;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> {

    }

    public FloatEvent OnHeal;
    public FloatEvent OnDamage;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void Heal()
    {
        currentHealth = maxHealth;
        OnHeal.Invoke(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        OnDamage.Invoke(currentHealth);
        Debug.Log("Health: " + currentHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }
        else
        {
            m_AudioSource.clip = m_DamageSound;
            m_AudioSource.Play();
        }
    }

    void Die()
    {
        Debug.Log("Die.");
        OnDeath.Invoke();
        m_AudioSource.clip = m_DeathSound;
        m_AudioSource.Play();
    }
}
