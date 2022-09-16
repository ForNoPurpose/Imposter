using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : Controller, IDamageable
{
    public HealthSystem _enemyHealth = new(100f);
    public Animator _enemyAnimator;
    public Rigidbody2D _enemyRB;

    public float _enemySpeed = 1.5f;

    private float _damage = 10f;

    public AIStateSO[] _possibleStates;

    [SerializeField] private float _detectionRadius = 1f;
    [SerializeField] private LayerMask _allowedLayer;
    private Collider2D[] _colliders = new Collider2D[3];
    private int _numFound = 0;

    private void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemyRB = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if (_numFound == 0)
        {
            _numFound = Physics2D.OverlapCircleNonAlloc(transform.position, _detectionRadius, _colliders, _allowedLayer);
            _possibleStates[0].State(this); 
        }
        else
        {
            _numFound = 1;
            _possibleStates[1].State(this);
        }

    }

    public void Damage(float amount)
    {
        AudioManager.instance.PlaySound("DamageSound");
        //transform.position = transform.position + new Vector3(-0.5f, 1f, 0);
        _enemyHealth.health -= amount / _enemyHealth.resistance;
        if (_enemyHealth.health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player") return;

        collision.collider.TryGetComponent(out IDamageable damageable);
        if (damageable != null)
        {
            damageable.Damage(_damage);
        }
    }
}
