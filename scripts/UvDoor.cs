using Godot;
using System;

public partial class UvDoor : Node2D
{
	Vector2 start;
	[Export]
	Vector2 end;
	public bool hasUV;
	float vel;
	[Export]
	float gamma=1;
	float x = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		start = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (hasUV){
			vel -= 2*gamma*vel*(float)delta+gamma*gamma*(float)delta*(x-1);
			GD.Print("moving");
		}
		else{
			vel -= 2*gamma*vel*(float)delta+gamma*gamma*(float)delta*x;
		}
		x+=vel*(float)delta;
		Position = x*end+(1-x)*start;
		hasUV = false;
	}

	void Accelerate(double delta){

	}
}
