using Godot;
using System;

public partial class UvLaser : Laser
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		newReflectedLaser = ResourceLoader.Load<PackedScene>("res://scenes/UVLaser.tscn");
	}

    protected override void OnHitObject(GodotObject obj)
    {
        base.OnHitObject(obj);
		if (obj is UvDoor door){
			door.hasUV=true;
		}
    }
}
