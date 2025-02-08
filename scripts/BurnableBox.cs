using Godot;
using System;

public partial class BurnableBox : StaticBody2D
{
	[Export]
	float burn_time = 1;
	[Export]
	bool burning;
	[Export]
	CpuParticles2D fire;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fire.Emitting = burning;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (burning){

			burn_time-=(float)delta;
			if (burn_time<delta*5) fire.Reparent(GetParent(), true);
			if (burn_time<0){
				fire.Emitting=false;
				
				GetParent().RemoveChild(this);
				Dispose();
			}
		}
	}

	public void Ignite(){
		burning = true;
		fire.Emitting=true;
	}
}
