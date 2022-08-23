using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float verticalSpeed = 2f;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;



    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        float movementSpeed = speed * direction;
        rb.velocity = new Vector2(movementSpeed, verticalSpeed);
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5) Deactivate();
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        lifetime = 0;
        gameObject.SetActive(false);
    }
}
