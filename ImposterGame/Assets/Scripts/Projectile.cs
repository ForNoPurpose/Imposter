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
        Debug.Log("Spawn coffee mug");
        Invoke("Disable", lifetime);
        MugFlightPath();
    }

    private void Update()
    {

    }
    private void MugFlightPath()
    {      
        rb.velocity = new Vector2(speed, verticalSpeed);
        transform.rotation = Quaternion.identity;
    }
    private void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
