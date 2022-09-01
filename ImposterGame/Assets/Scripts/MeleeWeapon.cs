using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage = 20f;
    public PolygonCollider2D weaponCollider;

    public Transform firePoint;

    private void Awake()
    {
        weaponCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") return;

        collision.TryGetComponent(out IDamageable damageable);
        if(damageable != null)
        {
            damageable.Damage(damage);
        }
    }
}
