using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomUtilities;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform firePoint;
    private PlayerMovement player;
    private PlayerInputActions inputActions;
    private float cooldownTimer = Mathf.Infinity;
    private bool aiming = false;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        inputActions.Player.Aim.started += ctx => aiming = true;
        inputActions.Player.Fire.performed += ctx => Attack();
        inputActions.Player.Aim.canceled += ctx => aiming = false;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (aiming)
        {
            Utils.DrawTrajectory(firePoint);
        }
    }

    private void Attack()
    {
        if (cooldownTimer < attackCooldown || !aiming) return;
        print("Fire Event triggered?!");
        cooldownTimer = 0;
        ObjectPoolManager.instance.SpawnFromObjectPool("CoffeeMug", firePoint.position, Quaternion.identity);
    }
}
