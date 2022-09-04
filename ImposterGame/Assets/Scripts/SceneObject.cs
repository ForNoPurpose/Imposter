using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using TMPro;

public class SceneObject : MonoBehaviour, IInteractable
{
    public bool _canCopy = false;
    public bool CanCopy { get => _canCopy; }

    [SerializeField] private string _name = "";
    [SerializeField] private string _prompt;
    private SpriteRenderer _spriteRenderer;

    public string InteractionPrompt => _prompt;

    private bool _displayPrompt;
    public bool PromptToggle { get => _displayPrompt; set => _displayPrompt = value; }

    Canvas _textBox;

    public int bufferRequired;

    [SerializeField] private string _tag;
    public string GetTag => _tag;

    void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateName();
    }

    private void Awake()
    {
        if (_canCopy) 
        { 
            _textBox = GetComponentInChildren<Canvas>(true);
            gameObject.layer = LayerMask.NameToLayer("Interactables");
            AddSpriteCollider();
        }
    }

    private void Update()
    {
        DisplayPrompt();
    }

    private void AddSpriteCollider()
    {
        var polyCollider = gameObject.AddComponent<PolygonCollider2D>();
        polyCollider.isTrigger = true;
        var sprite = _spriteRenderer.sprite;

        if (polyCollider == null || sprite == null) return;

        List<Vector2> path = new();
        for (int i = 0; i < polyCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polyCollider.SetPath(i, path.ToArray());
        }
    }

    private void UpdateName()
    {
        if (_name == null || _name == String.Empty)
        {
            string tempName = _spriteRenderer.sprite.name;
            gameObject.name = $"{tempName}";
        }
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("You interacted.");
        return true;
    }

    public void DisplayPrompt()
    {
        if (_textBox == null) return;
        if (_displayPrompt)
        {
            _textBox.gameObject.SetActive(true);
        }
        else
        {
            _textBox.gameObject.SetActive(false);
        }
        _displayPrompt = false;
    }
}
