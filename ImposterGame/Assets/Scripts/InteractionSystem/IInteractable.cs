using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public bool PromptToggle { get; set; }
    public bool CanCopy { get; }

    public string GetTag { get; }

    public bool Interact(Interactor interactor);

    public void DisplayPrompt();
}
