using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager != null)
        {
            manager.RemoveEnemy(gameObject);
        }
    }
}
