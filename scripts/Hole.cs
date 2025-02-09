using Godot;
using System;

public partial class Hole : StaticBody2D
{
	bool open = false;
	[Export]
	RayCast2D[] rays;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		for (int i = 0; i<rays.Length; i++){
			if (!open && rays[i].IsColliding()) FillerFound((Node2D)rays[i].GetCollider());
		}
	}

	public void FillerFound(Node2D filler){
		Player p = GetNode<PlayerTracker>("/root/PlayerTracker").player;
		if (p.grabbed == filler) p.grabbed = null;
		filler.GetParent().RemoveChild(filler);
		open = true;
		filler.Dispose();
		GetParent().RemoveChild(this);
		Dispose();
	}
}
