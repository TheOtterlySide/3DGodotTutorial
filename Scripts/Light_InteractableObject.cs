using Godot;
using System;

public partial class Light_InteractableObject : InteractableObject
{
    public override void Interact()
    {
        canInteract = false;
        var light = GetNode<Light3D>("Light3D");
        if (light != null)
        {
            light.Visible = !light.Visible;
        }
    }
}
