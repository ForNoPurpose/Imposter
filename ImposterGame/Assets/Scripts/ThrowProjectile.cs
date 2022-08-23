using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    private PlayerMovement player;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        cooldownTimer = 0;
        ObjectPoolManager.instance.SpawnFromObjectPool("CoffeeMug", firePoint.position, Quaternion.identity);
    }
}
