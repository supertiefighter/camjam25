using Godot;
using System;

public partial class Player : Node2D
{
	bool moving = false;
	Vector2 moveTarget;
	[Export]
	int gridSize = 32;
	[Export]
	float gamma;
	float c1;
	float c2;
	Vector2 vel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = Position.Snapped(gridSize);
		GD.Print(Position);
		GD.Print(Position.Snapped(gridSize));
		moving = false;
		c1 = 2*gamma;
		c2 = gamma*gamma;
		GD.Print(c1);
		GD.Print(c2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!moving){
			if (Input.IsActionPressed("Left"))
			{
				BeginMove(gridSize*Vector2.Left);
			}
			if (Input.IsActionPressed("Right")){
				BeginMove(gridSize*Vector2.Right);
			}
			if (Input.IsActionPressed("Up"))
			{
				BeginMove(gridSize*Vector2.Up);
			}
			if (Input.IsActionPressed("Down")){
				BeginMove(gridSize*Vector2.Down);
			}
		}

		vel -= (c1*vel+c2*(Position-moveTarget))*(float)delta; 
		Position+=vel*(float)delta;
		if (Position.Snapped(2f)==moveTarget){
			moving = false;
		}
		
	}

	private void BeginMove(Vector2 movement){
		moveTarget = Position.Snapped(gridSize)+movement;
		GD.Print("moving to "+moveTarget); 
		moving = true;
	}
}
