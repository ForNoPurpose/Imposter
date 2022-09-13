using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public sealed class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue = new();
    private Queue<string> _lines = new();

    public TMP_Text NameText;
    public TMP_Text DialogueText;

    private Canvas _canvas;
    private bool dialogActive = false;
    private WaitForSeconds _speachDelay = new(0.05f);

    private void Start()
    {
        _canvas = GetComponentInChildren<Canvas>(true);
    }

    private void Update()
    {
        if (dialogActive && Keyboard.current.fKey.wasPressedThisFrame)
        {
            StopAllCoroutines();
            DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogActive = true;
        _canvas.gameObject.SetActive(true);
        StartDialogue();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogActive = false;
        _canvas.gameObject.SetActive(false);
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
        DialogueText.text = "";
        foreach (var letter in line)
        {
            DialogueText.text += letter;
            yield return _speachDelay;
        }
    }
}
