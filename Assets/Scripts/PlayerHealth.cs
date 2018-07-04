using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int MaxHealth = 100;
    public int currentHealth;
    public Texture healthEmpty;
    public Texture healthFull;

    void Start () {
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
        manager.ResetLevel();
        currentHealth = MaxHealth;
    }

    void OnGUI()
    {
        for (int e = 0; e < MaxHealth; e++)
        {
            GUI.DrawTexture(new Rect((9 * e) + 3, Screen.height - 11, 9, 8), healthEmpty);
        }
        for (int f = 0; f < currentHealth; f++)
        {
            GUI.DrawTexture(new Rect((9 * f) + 3, Screen.height - 11, 9, 8), healthFull);
        }
    }
}
