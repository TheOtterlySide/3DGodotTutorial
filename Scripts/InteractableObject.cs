using Godot;
using System;

public partial class InteractableObject : Node3D
{
    [Export] private string interaction_text = "Interact";
    [Export] public bool canInteract = true;

    public string GetInteractionText()
    {
        return interaction_text;
    }

    public virtual void Interact()
    {
        GD.Print("Interacted with " + Name);
    }
}
