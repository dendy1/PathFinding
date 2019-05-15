using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    [SerializeField] private Statistic armor;
    [SerializeField] private Statistic damage;

    public int Damage => damage.Value;
    public int Armor => armor.Value;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.Value;
        damage = Mathf.Clamp(damage, 0, Int32.MaxValue);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died");
        CurrentHealth = maxHealth;
    }
}
