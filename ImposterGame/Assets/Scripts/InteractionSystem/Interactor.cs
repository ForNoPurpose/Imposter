using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _interactRadius;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private int _numFound;
    private Collider2D[] _colliders = new Collider2D[3];

    private PlayerInputActions _actions;

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Enable();
    }
    private void OnDisable()
    {
        _actions.Disable();
    }
    private void Update()
    {
        _numFound = Physics2D.OverlapCircleNonAlloc(_interactPoint.position, _interactRadius, _colliders, _layerMask);
        if(_numFound > 0)
        {
            _colliders[0].TryGetComponent<IInteractable>(out var interactable);
            _colliders[0].TryGetComponent<ICopiable>(out var copiable);
            if(interactable != null && _actions.Player.Interact.WasPerformedThisFrame())
            {
                interactable.Interact();
            }
            if(copiable != null && _actions.Player.CopyMechanic.WasPerformedThisFrame())
            {
                copiable.CopyMechanic();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactPoint.position, _interactRadius);
    }
}
