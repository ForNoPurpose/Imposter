using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider2D[] _colliders = new Collider2D[3];
    [SerializeField] private int _numFound;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if(_numFound > 0)
        {
            _colliders[0].TryGetComponent<IInteractable>(out var interactable);

            if(interactable != null)
            {
                interactable.PromptToggle = true;
                if(Keyboard.current.rKey.wasPressedThisFrame && interactable.CanCopy)
                {
                    var addToPlayerProjectileList = Instantiate(Resources.Load($"Assets/Prefabs/{interactable.GetTag}.prefab")) as Projectile;
                    addToPlayerProjectileList.gameObject.SetActive(false);
                    if(!(_playerController.playerBufferCurrentSize + addToPlayerProjectileList.BufferRequired > _playerController.playerBufferMaxSize))
                    {
                        _playerController.playerBuffer.Add(addToPlayerProjectileList);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
