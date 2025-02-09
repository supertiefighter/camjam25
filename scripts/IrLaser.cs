using Godot;
using System;

public partial class IrLaser : Laser
{



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		newReflectedLaser = ResourceLoader.Load<PackedScene>("res://scenes/IRLaser.tscn");
		
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		
    }


    protected override void OnHitObject(GodotObject obj)
    {
        base.OnHitObject(obj);
		if (obj is BurnableBox box){
			box.Ignite();
		}
		if (obj is Keybox k){
			k.hasIR = true;
		}
    }

}
