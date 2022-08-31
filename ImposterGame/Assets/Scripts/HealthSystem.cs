using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthSystem", menuName = "ScriptableObjects/HealthSystem", order = 1)]
public class HealthSystem : ScriptableObject
{
    public float maxHealth;
    public float health;
    public float resistance;


    public void OnEnable()
    {
        health = maxHealth;
    }

    public void Awake()
    {
        health = maxHealth;
    }
}
