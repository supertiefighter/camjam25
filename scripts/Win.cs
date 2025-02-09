using Godot;
using System;

public partial class Win : Area2D
{
	[Export]
	Node2D Level;
	[Export]
	PackedScene nextLevel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void onBodyEntered(Node2D body){
		Level.AddSibling(nextLevel.Instantiate());
		Level.GetParent().RemoveChild(Level);
	}
}
