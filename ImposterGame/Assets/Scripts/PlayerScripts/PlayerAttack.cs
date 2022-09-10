using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomUtilities;
using System;
using Unity.VisualScripting;

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
    private ItemDataSO _projectileData;

    private MeleeWeapon meleeWeapon;

    public static event Action<ItemDataSO> OnThrow;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        tempSpeed = player.speed;

        playerAnimator = GetComponent<Animator>();

        meleeWeapon = GetComponentInChildren<MeleeWeapon>(true);

        inputActions = new PlayerInputActions();
        inputActions.Enable();
        inputActions.Player.Aim.started += ctx => {
            aiming = true;
            playerAnimator.SetBool("isAiming", true);
            GetProjectile();
        };
        inputActions.Player.Fire.performed += ctx => {
            if (aiming)
            {
                playerAnimator.SetTrigger("Throw");
                OnThrow?.Invoke(_projectileData);
            }
        };
        inputActions.Player.Aim.canceled += ctx => {
            aiming = false;
            playerAnimator.SetBool("isAiming", false);
            ResetSpeed();
            if (currentProjectile != null) 
            { 
                currentProjectile.held = false;
                //Destroy(currentProjectile.gameObject);
            }
        };

        inputActions.Player.Melee.performed += ctx => Melee();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (aiming)
        {
            player.FlipPlayer(true);
            if (currentProjectile != null && !currentProjectile.thrown) HoldProjectile();
            Utils.DrawTrajectory(firePoint);
        }
    }

    private void Throw()
    {
        if (cooldownTimer < attackCooldown || !aiming) return;
        cooldownTimer = 0;

        if (currentProjectile != null)
        {
            currentProjectile.held = false;
            currentProjectile.thrown = true;
            currentProjectile.MugFlightPath();
            currentProjectile = null;
        }
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

    private void GetMeleeWeapon()
    {
        meleeWeapon.gameObject.SetActive(true);
    }

    private void HideMeleeWeapon()
    {
        meleeWeapon.gameObject.SetActive(false);
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
        if ((_projectileData = PlayerController.instance.CurrentProjectile()) == null) return;
        var getProjectile = ObjectPoolManager.instance.SpawnFromObjectPool(_projectileData.ID, firePoint.position, Quaternion.identity);
        currentProjectile = getProjectile.GetComponent<Projectile>();
        currentProjectile.held = true;
        currentProjectile.origin = Projectile.ProjectileOrigin.Player;
    }

    private void HoldProjectile()
    {
        currentProjectile.transform.position = firePoint.position;
        currentProjectile.transform.rotation = firePoint.rotation;
    }
}
