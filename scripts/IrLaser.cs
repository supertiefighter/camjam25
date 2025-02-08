using Godot;
using System;

public partial class IrLaser : Laser
{

	AudioStreamGeneratorPlayback playback;

	float pulseHz = 440f;
	float sampleHz;
	float volumeChangeHz = 0.5f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		newReflectedLaser = ResourceLoader.Load<PackedScene>("res://scenes/IRLaser.tscn");
		/*
		if (Player.Stream is AudioStreamGenerator generator) // Type as a generator to access MixRate.
    	{
        	sampleHz = generator.MixRate;
        	Player.Play();
        	playback = (AudioStreamGeneratorPlayback)Player.GetStreamPlayback();
        	FillBuffer();
    	}
		*/
		
	}

    public override void _Process(double delta)
    {
		//if (!Player.Playing) Player.Play();
        base._Process(delta);
		
		/*
		if (Player.GetPlaybackPosition()>1) {
			Player.Seek(Player.GetPlaybackPosition()%1);
			FillBuffer();
			Player.StreamPaused=false;
			Player.Play();
		}
		*/
    }


    protected override void OnHitObject(GodotObject obj)
    {
        base.OnHitObject(obj);
		if (obj is BurnableBox box){
			box.Ignite();
		}
    }
	public void FillBuffer()
	{
    	double phase = 0.0;
		double volumePhase = 0;
    	float increment = pulseHz / sampleHz;
		float volumeIncrement = volumeChangeHz / sampleHz;
    	int framesAvailable = playback.GetFramesAvailable();

    	for (int i = 0; i < framesAvailable; i++)
    	{
    	    playback.PushFrame(Vector2.One * (float)Mathf.Sin(phase * Mathf.Tau)*(0.5f+0.5f*((float)Mathf.Sin(volumePhase*Mathf.Tau))));
	        phase = Mathf.PosMod(phase + increment, 1.0);
			volumePhase = Mathf.PosMod(volumePhase + volumeIncrement, 1.0);
    	}
	}

}
