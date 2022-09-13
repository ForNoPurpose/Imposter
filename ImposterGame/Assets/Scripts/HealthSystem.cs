using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public float maxHealth;
    public float health;
    public float resistance;

    public void Awake()
    {
        health = maxHealth;
    }

    public HealthSystem(float maxHealth, float resistance = 1f)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.resistance = resistance;
    }
}
