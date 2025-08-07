using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Movement")] [Export] private float max_speed;
    [Export] private float max_run_speed;
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
            Velocity -= new Vector3(0, (float)(gravity.AsSingle() * delta), 0);
        }

        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            Velocity = new Vector3(0, jump_force, 0);
        }

        var move_input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        var move_dir = Transform.Basis * new Vector3(move_input.X, move_input.Y, 0).Normalized();

        var is_running = Input.IsActionPressed("sprint");
        var target_speed = max_speed;

        if (is_running)
        {
            target_speed = max_run_speed;
            var run_dot = -move_dir.Dot(Transform.Basis.Z);
            run_dot = Mathf.Clamp(run_dot, 0, 1);
            move_dir *= run_dot;
        }

        var current_smoothing = acceleration;

        if (!IsOnFloor())
        {
            current_smoothing = air_acceleration;
        }

        if (move_dir == Vector3.Zero)
        {
            current_smoothing = braking;
        }

        var targetVelocity = move_dir * target_speed;
        Velocity = new Vector3((float)Mathf.Lerp(Velocity.X, targetVelocity.X, current_smoothing * delta),
            (float)Mathf.Lerp(Velocity.Z, targetVelocity.Z, current_smoothing * delta),
            0);
        MoveAndSlide();

        //Cameras
        RotateY(-camera_input.X * look_sensivity);
        camera.RotateX(-camera_input.Y * look_sensivity);
        camera.Rotation = new Vector3((float)Mathf.Clamp(camera.Rotation.X, -1.5, 1.5), 0, 0);
        camera_input.Y = 0;
        
        //Mouse
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            if (Input.GetMouseMode() == Input.MouseModeEnum.Visible)
            {
                Input.SetMouseMode(Input.MouseModeEnum.Captured);
            }
            else
            {
                Input.SetMouseMode(Input.MouseModeEnum.Captured);
            }
        }
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