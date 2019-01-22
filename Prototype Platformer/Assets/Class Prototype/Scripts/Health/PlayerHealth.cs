using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;

    public UnityEvent damageEvent;
    public UnityEvent deathEvent;

    private int _currentHealth;
    private bool _canTakeDamage = true;

    public void Start ()
    {
        _currentHealth = startingHealth;
    }

    public void DealDamage (int damage)
    {
        if (!_canTakeDamage) return;

        _currentHealth -= damage;

        print("PLAYER HEALTH: " + _currentHealth);

        damageEvent.Invoke();

        if (_currentHealth <= 0)
        {
            PlayerDeath();
            _currentHealth = 0;
        }
    }

    public void PlayerDeath ()
    {
        print("PLAYER DEAD");
        deathEvent.Invoke();
        _canTakeDamage = false;
    }
}
