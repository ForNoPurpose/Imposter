using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float verticalSpeed = 2f;
    private float _direction;
    private bool hit;
    private float lifetime = 1.5f;

    private BoxCollider2D boxCollider;
    public Rigidbody2D rb;



    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        //Debug.Log("Spawn coffee mug");
        Invoke("Disable", lifetime);
        SetDirection();
        MugFlightPath();
    }

    private void Update()
    {

    }
    private void MugFlightPath()
    {
        float movementSpeed = speed * _direction;
        rb.velocity = new Vector2(movementSpeed, verticalSpeed);
        transform.rotation = Quaternion.identity;
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
}
