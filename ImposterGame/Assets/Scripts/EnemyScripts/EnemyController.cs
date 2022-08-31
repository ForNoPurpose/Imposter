using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , IDamageable
{
    public HealthSystem enemyHealth;

    private void Update()
    {
    }

    public void Damage(float amount)
    {
        enemyHealth.health -= amount / enemyHealth.resistance;
        if (enemyHealth.health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
