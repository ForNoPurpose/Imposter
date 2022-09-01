using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , IDamageable
{
    public HealthSystem enemyHealth;
    public Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
    }

    public void Damage(float amount)
    {
        enemyAnimator.SetTrigger("OnReceiveHit");
        enemyHealth.health -= amount / enemyHealth.resistance;
        if (enemyHealth.health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
