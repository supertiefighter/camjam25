using Godot;
using System;

public partial class KeyDoor : StaticBody2D
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
			if (!open && rays[i].IsColliding()) KeyFound((Node2D)rays[i].GetCollider());
		}
	}

	public void KeyFound(Node2D key){
		Player p = GetNode<PlayerTracker>("/root/PlayerTracker").player;
		if (p.grabbed == key) p.grabbed = null;
		key.GetParent().RemoveChild(key);
		open = true;
		key.Dispose();
		GetParent().RemoveChild(this);
		Dispose();
	}
}
