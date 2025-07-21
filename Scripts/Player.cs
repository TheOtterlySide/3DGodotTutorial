using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Movement")] [Export] private float max_speed;
    [Export] private float acceleration;
    [Export] private float braking;
    [Export] private float air_acceleration;
    [Export] private float jump_force;
    [Export] private float gravity_modifier;
    [Export] private bool isRunning;

    [ExportGroup("Camera")] [Export] private float look_sensivity;
    [Export] private Vector2 camera_input;

    [Export] private Camera3D camera;
    [Export] private Variant gravity = ProjectSettings.GetSetting("physics/3d/default_gravity");

    public override void _Ready()
    {
       Input.SetMouseMode(Input.MouseModeEnum.Captured);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            Velocity -= new Vector3(0, (float)(gravity.AsSingle() * delta) ,0); 
        }

        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            Velocity = new Vector3(0, jump_force ,0); 
        }
        
        var move_input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        var move_dir = Transform.Basis * new Vector3(move_input.X, move_input.Y, 0).Normalized();
        
        Velocity = move_dir * max_speed;
        MoveAndSlide();
        
        //Camera
        RotateY(-camera_input.X * look_sensivity);
        camera.RotateX(-camera_input.Y * look_sensivity);
        camera.Rotation = new Vector3((float)Mathf.Clamp(camera.Rotation.X, -1.5, 1.5),0,0);
        camera_input.Y = 0;
        
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            camera_input = mouseMotion.Relative;
        }

        base._UnhandledInput(@event);
    }
}