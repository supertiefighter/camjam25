using Godot;
using System;

public partial class Music : AudioStreamPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Stream=ResourceLoader.Load<AudioStream>("res://audio/bgmflp.ogg");
		Play();
		VolumeDb=15;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
