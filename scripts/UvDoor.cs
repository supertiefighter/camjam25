using Godot;
using System;

public partial class UvDoor : Node2D
{
	Vector2 start;
	[Export]
	Vector2 end;
	public bool hasUV;
	Vector2 vel;
	[Export]
	float gamma=1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		start = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 target = start;
		if (hasUV){
			target = end;
		}
		vel -= 2*gamma*vel*(float)delta+gamma*gamma*(float)delta*(Position-target);
		Position+=vel*(float)delta;
		hasUV = false;
	}

	void Accelerate(double delta){

	}
}
