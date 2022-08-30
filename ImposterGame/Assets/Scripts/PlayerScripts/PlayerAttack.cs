using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomUtilities;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform firePoint;
    private PlayerMovement player;
    private Animator playerAnimator;
    private PlayerInputActions inputActions;
    private float cooldownTimer = Mathf.Infinity;
    private bool aiming = false;

    private float tempSpeed;

    private Projectile currentProjectile;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        tempSpeed = player.speed;

        playerAnimator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        inputActions.Enable();
        inputActions.Player.Aim.started += ctx => {
            aiming = true;
            playerAnimator.SetBool("isAiming", true);
            GetProjectile();
        };
        inputActions.Player.Fire.performed += ctx => {
            if (aiming) playerAnimator.SetTrigger("Throw");
        };
        inputActions.Player.Aim.canceled += ctx => {
            aiming = false;
            playerAnimator.SetBool("isAiming", false);
            ResetSpeed();
            currentProjectile.held = false;
        };

        inputActions.Player.Melee.performed += ctx => Melee();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (aiming)
        {
            player.FlipPlayer(true);
            if (!currentProjectile.thrown) HoldProjectile();
            Utils.DrawTrajectory(firePoint);
        }
    }

    private void Throw()
    {
        if (cooldownTimer < attackCooldown || !aiming) return;
        cooldownTimer = 0;
        //ObjectPoolManager.instance.SpawnFromObjectPool("CoffeeMug", firePoint.position, Quaternion.identity);
        currentProjectile.held = false;
        currentProjectile.thrown = true;
        currentProjectile.MugFlightPath();
        if (aiming)
        {
            GetProjectile();
        }
    }

    private void Melee()
    {
        if (!aiming)
        {
            playerAnimator.SetTrigger("Attack");
        }
    }

    private void SetSpeed(float value)
    {
        player.speed = value;
    }

    private void SetSpeedHalf()
    {
        player.speed = tempSpeed / 2;
    }

    private void ResetSpeed()
    {
        player.speed = tempSpeed;
    }

    private void GetProjectile()
    {
        var getProjectile = ObjectPoolManager.instance.SpawnFromObjectPool("CoffeeMug", firePoint.position, Quaternion.identity);
        currentProjectile = getProjectile.GetComponent<Projectile>();
        currentProjectile.held = true;
    }

    private void HoldProjectile()
    {
        currentProjectile.transform.position = firePoint.position;
        currentProjectile.transform.rotation = firePoint.rotation;
    }
}
