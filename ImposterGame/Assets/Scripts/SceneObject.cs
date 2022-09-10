using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using TMPro;

public class SceneObject : MonoBehaviour, IInteractable, ICopiable
{
    [SerializeField] private ItemDataSO _itemData;

    public static event Action OnInteract;
    public static event Action<ItemDataSO> OnPickup;

    public void Interact()
    {
        Debug.Log("You Interacted!");
        OnInteract?.Invoke();
    }

    public void CopyMechanic()
    {
        Debug.Log("You Copied this!");
        OnPickup?.Invoke(_itemData);
    }

    private void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = _itemData.Icon;
        if (_itemData.CanCopy)
        {
            gameObject.layer = LayerMask.NameToLayer("Interactables");
            var collider = gameObject.AddComponent<PolygonCollider2D>();
            collider.isTrigger = true;
        }
    }
}
