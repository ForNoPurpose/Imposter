using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomUtilities;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 3f;
    [SerializeField] private float jumpSpeed = 4f;
    private Vector2 moveInput;
    public Rigidbody2D body;
    private BoxCollider2D playerCollider;
    private Animator playerAnimator;

    public PlayerInputActions inputActions;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump;
    }

    private void OnDestroy()
    {
        inputActions.Player.Jump.performed -= Jump;
    }

    private void Update()
    {
        Run();
        FlipPlayer();
    }

    public Vector2 GetPlayerPosition()
    {
        return new Vector2(body.position.x, body.position.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        RaycastHit2D feetOnGround = Physics2D.Raycast(playerCollider.bounds.center, Vector3.down, playerCollider.bounds.extents.y + 0.03f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(playerCollider.bounds.center, Vector3.down, Color.red, 2f);
        //Debug.Log(feetOnGround.collider.name);
        if (feetOnGround.collider == null)
        {
            Debug.Log("Can't Jump"); 
            return;
        }
        else
        {
            body.velocity += new Vector2(0, jumpSpeed);
            //Debug.DrawLine(playerCollider.bounds.center, playerCollider.bounds.center - new Vector3(0,playerCollider.bounds.extents.y + 0.03f, 0), Color.red, 5f);
        }
    }

    private void Run()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector2 playerVelocity = new(moveInput.x * speed, body.velocity.y);
        body.velocity = playerVelocity;

        if (Mathf.Abs(body.velocity.x) > Mathf.Epsilon)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
            playerAnimator.SetBool("isRunning", false);
    }

    public void FlipPlayer(bool faceCursor = false)
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed && !faceCursor)
        {
            float rot = body.velocity.x > Mathf.Epsilon ? 0 : 180;
            transform.rotation = new Quaternion(0, rot, 0, 1);
        }

        if (faceCursor)
        {
            float rot = Utils.GetMouseToWorldPosition().x > transform.position.x ? 0 : 180;
            transform.rotation = new Quaternion(0, rot, 0, 1);
        }
    }
}
