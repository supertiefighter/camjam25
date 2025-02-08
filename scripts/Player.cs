using Godot;
using System;

public partial class Player : Node2D, IGridObject
{
	bool moving = false;
	Vector2? movingDir = null;
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

	Vector2? queuedMoveInput = null;
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
		moveTarget = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (queuedMoveInput is not null && !moving){
			TryStartMove((Vector2)queuedMoveInput);
		}
		if (Input.IsActionPressed("Left"))
		{
			TryStartMove(gridSize*Vector2.Left);
		}
		if (Input.IsActionPressed("Right")){
			TryStartMove(gridSize*Vector2.Right);
		}
		if (Input.IsActionPressed("Up"))
		{
			TryStartMove(gridSize*Vector2.Up);
		}
		if (Input.IsActionPressed("Down")){
			TryStartMove(gridSize*Vector2.Down);
		}
		
		if (moving){
			vel -= (c1*vel+c2*(Position-moveTarget))*(float)delta; 
			Position+=vel*(float)delta;
		}
		if (Position.Snapped(2f)==moveTarget){
			Position=moveTarget;
			moving = false;
			movingDir = null;
		}
		
	}

	private void TryStartMove(Vector2 movement){
		if (moving){
			if (((Vector2)movingDir-movement).LengthSquared()>0.1&&(Position-moveTarget).LengthSquared()>256){
				queuedMoveInput = movement;
			}
			return;
		}
		wallRaycast.TargetPosition=movement;
		wallRaycast.ForceRaycastUpdate();
		if (!wallRaycast.IsColliding()){
			pushableRaycast.TargetPosition=movement;
			pushableRaycast.ForceRaycastUpdate();
			bool canMove = true;
			if (pushableRaycast.IsColliding()){
				GodotObject collider = pushableRaycast.GetCollider();
				if (collider is Node node){
					if (node.GetParent() is IGridObject gridObject){
						canMove = gridObject.TryPush(movement);
					}
				}
			}
			if (canMove){
				moveTarget = Position.Snapped(gridSize)+movement;
				GD.Print("moving to "+moveTarget); 
				moving = true;
				movingDir = movement;
			}
			else{
				GD.Print("Cannot move due to IGridObject");
			}
		}
		else{
			GD.Print("Cannot move due to wall");
		}
		queuedMoveInput=null;
	}

    public bool TryPush(Vector2 dir)
    {
        return false;
    }
}
