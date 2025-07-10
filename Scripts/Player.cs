using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Movement")]
    [Export] private float max_speed;
    [Export] private float acceleration;
    [Export] private float braking;
    [Export] private float air_acceleration;
    [Export] private float jump_force;
    [Export] private float gravity_modifier;
    [Export] private bool isRunning;

    [ExportGroup("Camera")]
    [Export] private float look_sensivity;
    [Export] private Vector2 camera_input;
    
    [Export] private Camera3D camera;
    [Export] private Variant gravity = ProjectSettings.GetSetting("physics/3d/default_gravity");

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
    }
}
