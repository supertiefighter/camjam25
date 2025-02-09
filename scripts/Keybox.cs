using Godot;
using System;

public partial class Keybox : StaticBody2D
{
	public bool hasUV;
	public bool hasIR;
	[Export]
	CpuParticles2D fire;
	[Export]
	float burnTime = 2;
	bool burning = false;

	[Export]
	PackedScene key;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (hasUV && hasIR){
			burning = true;
			fire.Emitting = true;
		}
		if (burning){
			burnTime-=(float)delta;
		}
		if (burnTime<0){
				fire.Emitting=false;
				Key k = key.Instantiate<Key>();
				k.GlobalPosition = GlobalPosition;
				AddSibling(k);
				GetParent().RemoveChild(this);
				Dispose();
			}
		hasUV=false;
		hasIR=false;
	}

}
