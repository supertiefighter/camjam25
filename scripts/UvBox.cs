using Godot;
using System;

public partial class UvBox : StaticBody2D{
	

	Vector2 force;
	[Export]
	float speed = 32;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position+=force*(float)delta*32;
		force = Vector2.Zero;
	}

	public void ApplyForce(Vector2 f){
		force+=f;
	}

}
