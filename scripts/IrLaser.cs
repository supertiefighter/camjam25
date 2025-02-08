using Godot;
using System;

public partial class IrLaser : Laser
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		newReflectedLaser = ResourceLoader.Load<PackedScene>("res://scenes/IRLaser.tscn");
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		if (Input.IsActionPressed("Left")){Rotation+=(float)delta*0.1f;}
		if (Input.IsActionPressed("Right")) {Rotation+=(float)delta*-0.1f;}
    }

    protected override void OnHitObject(GodotObject obj)
    {
        base.OnHitObject(obj);
		if (obj is BurnableBox box){
			box.Ignite();
		}
    }
}
