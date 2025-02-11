using Godot;
using System;

public partial class Emitter : Node2D
{
    [Export]
    bool active = true;
    Laser laser;
    [Export]
    PackedScene newLaser;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (active&&laser is null){
            laser = newLaser.Instantiate<Laser>();
            AddChild(laser);
        } 
	}

    
}
