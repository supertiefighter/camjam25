using Godot;
using System;

public partial class Rumble : Node
{
	float intensity = 0;
	float t =0.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		t+=(float)delta*3;
		Input.StopJoyVibration(Input.GetConnectedJoypads()[0]);
		Input.StartJoyVibration(Input.GetConnectedJoypads()[0], intensity*Mathf.Sin(t)*Mathf.Sin(t), intensity*Mathf.Cos(t)*Mathf.Cos(t), 0.2f);
		intensity = 0;
	}

	public void AddIntensity(float x){
		intensity = Mathf.Min(Mathf.Max(x, intensity),1);
	}
}
