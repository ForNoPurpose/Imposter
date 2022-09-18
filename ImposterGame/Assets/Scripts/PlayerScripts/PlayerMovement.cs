using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomUtilities;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] private float jumpSpeed = 4f;
    private Vector2 moveInput;
    public Rigidbody2D body;
    private Animator playerAnimator;
    public PlayerInputActions inputActions;

    Collider2D[] _onGround = new Collider2D[2];
    int _numFound = 0;
    [SerializeField] LayerMask _jumpableLayer;
    [SerializeField] float _jumpColliderRadius;
    [SerializeField] Vector3 _jumpColliderOffset;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
        _numFound = Physics2D.OverlapCircleNonAlloc(transform.position + _jumpColliderOffset, _jumpColliderRadius, _onGround, _jumpableLayer);
        Run();
        FlipPlayer();
    }

    public Vector2 GetPlayerPosition()
    {
        return new Vector2(body.position.x, body.position.y);
    }


    private void Jump(InputAction.CallbackContext context)
    {
        if (_numFound == 0)
        {
            Debug.Log("Can't Jump");
            return;
        }
        else
        {
            body.velocity += new Vector2(0, jumpSpeed);
            AudioManager.instance.PlaySound("JumpSound");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + _jumpColliderOffset, _jumpColliderRadius);
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

    private void FirstStep()
    {
        if (_numFound == 0)
        {
            AudioManager.instance.StopSound("FirstFootStep");
            return;
        }
        AudioManager.instance.PlaySound("FirstFootStep");
    }

    private void SecondStep()
    {
        if (_numFound == 0)
        {
            AudioManager.instance.StopSound("SecondFootStep");
            return;
        }
        AudioManager.instance.PlaySound("SecondFootStep");
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
