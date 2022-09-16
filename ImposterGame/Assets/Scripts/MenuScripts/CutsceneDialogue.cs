using System.Collections;
using UnityEngine;
using TMPro;

public sealed class CutsceneDialogue : MonoBehaviour
{
    public TMP_Text DialogueText;
    private WaitForSeconds _speachDelay = new(0.04f);
    public GameObject textBlock;

    private void OnEnable()
    {
        DisplayText();
    }
    public void DisplayText()
    {
        StartCoroutine(TypeWriter(DialogueText.text));
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