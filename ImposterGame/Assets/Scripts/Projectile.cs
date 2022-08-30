using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUtilities;
using System.Net;

public class Projectile : MonoBehaviour
{
    //[SerializeField] private float speed = 5f;
    //[SerializeField] private float verticalSpeed = 2f;
    private float _direction;
    private bool hit;
    private float lifetime = 1.5f;

    private BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public bool held = true;
    public bool thrown = false;

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
        print(Utils.GetTrajectory(transform).initialVelocity);
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
}
