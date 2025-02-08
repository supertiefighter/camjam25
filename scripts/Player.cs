using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export]
	Node2D rotating_stuff;
	[Export]
	RayCast2D grabRay;

	Node2D grabbed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<PlayerTracker>("/root/PlayerTracker").player = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		Vector2 currForce = Input.GetVector("Left", "Right", "Up", "Down");
		float forceMag = currForce.Length();
		currForce = currForce.Normalized();
		ApplyCentralForce(currForce * forceMag * 2000.0f);
		Vector2 aimVector = Input.GetVector("Aim_Left", "Aim_Right", "Aim_Up", "Aim_Down");
		if (aimVector.LengthSquared()>0.05){
		rotating_stuff.Rotation = aimVector.Angle()+Mathf.Pi/2;
		if (grabbed is not null){
			
			grabbed.GlobalRotation=aimVector.Angle()+Mathf.Pi/2;
		}
		}
		if (grabbed is not null)
			grabbed.GlobalPosition = GlobalPosition+(Vector2.Up*32).Rotated(rotating_stuff.GlobalRotation);
		
		if (Input.IsActionJustPressed("Grab")){
			if (grabbed is null){
				if (grabRay.IsColliding()){
					
					grabbed = (Node2D)grabRay.GetCollider();
				}
			}
			else{
				grabbed = null;
			}
			}
		
		
			
		
	}
}
