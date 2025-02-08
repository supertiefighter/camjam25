using Godot;
using System;

public partial class Player : Node2D, IGridObject
{
	bool moving = false;
	Vector2 moveTarget;
	[Export]
	int gridSize = 32;
	[Export]
	float gamma = 25;
	float c1;
	float c2;
	Vector2 vel;

	[Export]
	RayCast2D wallRaycast;
	[Export]
	RayCast2D pushableRaycast;
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
		wallRaycast.TargetPosition=movement;
		wallRaycast.ForceRaycastUpdate();
		if (!wallRaycast.IsColliding()){
			moveTarget = Position.Snapped(gridSize)+movement;
			GD.Print("moving to "+moveTarget); 
			moving = true;
		}
		else{
			GD.Print("Cannot move due to wall");
		}
	}

    public bool TryPush(IGridObject.Direction dir)
    {
        return false;
    }
}
