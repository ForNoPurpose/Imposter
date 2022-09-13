using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUtilities;

public class Projectile : MonoBehaviour
{
    //[SerializeField] private float speed = 5f;
    //[SerializeField] private float verticalSpeed = 2f;
    private float _direction;
    private bool hit;
    private float lifetime = 5f;

    private BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public bool held = true;
    public bool thrown = false;

    public DamageSystem damage;

    public enum ProjectileOrigin
    {
        Player,
        Enemy,
        Friendly
    }

    public ProjectileOrigin origin;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //Debug.Log("Spawn coffee mug");
        //Invoke("Disable", lifetime);
        thrown = false;
        SetDirection();
        //MugFlightPath();
    }

    private void Update()
    {
        if (held)
        {
            rb.simulated = false;
        }
        else
        {
            rb.simulated = true;
            if(!thrown) Disable();
        }
    }
    public void MugFlightPath()
    {
        rb.simulated = true;
        //float movementSpeed = speed * _direction;
        //print(Utils.GetTrajectory(transform).initialVelocity);
        rb.velocity = Utils.GetTrajectory(transform).initialVelocity;
        transform.rotation = Quaternion.identity;
        Invoke("Disable", lifetime);
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
    }

    private void SetDirection()
    {
        float sign = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().transform.rotation.y;
        _direction = sign > Mathf.Epsilon ? -1 : 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == origin.ToString()) return;
        //Debug.Log($"{collision.collider.name}");

        collision.collider.TryGetComponent(out IDamageable damageable);
        if(damageable != null)
        {
            damageable.Damage(damage.damage);
            Disable();
        }
    }
}
