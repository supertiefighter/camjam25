using Godot;
using System;

public partial class UvLaser : Laser
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		newReflectedLaser = ResourceLoader.Load<PackedScene>("res://scenes/UVLaser.tscn");
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		Rumble r = GetNode<Rumble>("/root/Rumble");
		Player p = GetNode<PlayerTracker>("/root/PlayerTracker").player;
		float intensity = 300/Mathf.Pow((audio.GlobalPosition-p.GlobalPosition).Length(), 2f);
		r.AddIntensity(intensity);
    }
    protected override void OnHitObject(GodotObject obj)
    {
        base.OnHitObject(obj);
		if (obj is UvDoor door){
			door.hasUV=true;
		}
		if (obj is UvBox box){
			box.ApplyForce(Vector2.Right.Rotated(GlobalRotation));
		}
		if (obj is Keybox k){
			k.hasUV = true;
		}

    }
}
