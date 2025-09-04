using Godot;
using System;

public partial class InteractionController : RayCast3D
{
    [Export] private Label interaction_label;

    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        var interactObject = GetCollider();
        if (interactObject != null && interactObject is InteractableObject interactable)
        {
            if (!interactable.canInteract)
            {
                interaction_label.Visible = false;
                return;
            }
            
            interaction_label.Text = "[E] " + interactable.GetInteractionText();
            interaction_label.Visible = true;

            if (Input.IsActionJustPressed("interact"))
            {
                interactable.Interact();
            }
        }
        else
        {
            interaction_label.Visible = false;
        }
    }
}
