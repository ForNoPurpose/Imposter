using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : Controller, IDamageable
{
    public HealthSystem _enemyHealth;
    public Animator _enemyAnimator;
    public Rigidbody2D _enemyRB;

    public float _enemySpeed = 1.5f;

    public AIStateSO[] _possibleStates;

    [SerializeField] private float _detectionRadius = 1f;
    [SerializeField] private LayerMask _allowedLayer;
    private Collider2D[] _colliders = new Collider2D[3];
    private int _numFound;

    private void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemyRB = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        _numFound = Physics2D.OverlapCircleNonAlloc(transform.position, _detectionRadius, _colliders, _allowedLayer);
        if (_numFound == 0)
        {
            _possibleStates[0].State(this); 
        }
        else
        {
            _possibleStates[1].State(this);
        }

    }

    public void Damage(float amount)
    {
        transform.position = transform.position + new Vector3(-0.5f, 1f, 0);
        _enemyHealth.health -= amount / _enemyHealth.resistance;
        if (_enemyHealth.health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
