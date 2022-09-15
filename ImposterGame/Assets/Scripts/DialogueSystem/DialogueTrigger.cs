using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;
using System.Text;

public sealed class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue = new();
    private Queue<string> _lines = new();

    public TMP_Text NameText;
    public TMP_Text DialogueText;

    private Canvas _canvas;
    private bool dialogActive = false;
    private WaitForSeconds _speachDelay = new(0.05f);

    private Collider2D _targetCollider;

    [SerializeField] private Vector3 _offset = new Vector3(1.3f, 1f, 0);
    private Vector3 _startPos;
    private float _deactivateRange = 5f;

    [SerializeField] private bool _repeatDialogue = false;
    private bool _visited = false;

    private void Start()
    {
        _canvas = GetComponentInChildren<Canvas>(true);
        _startPos = transform.position;
    }

    private void Update()
    {

        if (dialogActive)
        {
            _canvas.transform.position = _targetCollider.transform.position + _offset;
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                StopAllCoroutines();
                DisplayNextSentence(); 
            }

            if(Vector3.Distance(_canvas.transform.position, _startPos) > _deactivateRange)
            {
                dialogActive = false;
                _canvas.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!_visited)
        {
            _targetCollider = collision;
            dialogActive = true;
            _canvas.gameObject.SetActive(true);
            StartDialogue(); 
        }
        if(!_repeatDialogue) _visited = true;
        else _visited = false;
    }

    public void StartDialogue()
    {
        _lines.Clear();

        foreach (var line in _dialogue.Lines)
        {
            _lines.Enqueue(line);
        }

        NameText.text = _dialogue.Name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_lines.Count == 0)
        {
            dialogActive = false;
            _canvas.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(TypeWriter(_lines.Dequeue()));
    }

    IEnumerator TypeWriter(string line)
    {
        DialogueText.text = line;
        for (int i = 1; i <= line.Length; i++)
        {
            DialogueText.maxVisibleCharacters = i;
            yield return _speachDelay;
        }
    }
}
