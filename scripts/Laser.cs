using Godot;
using System;

public abstract partial class Laser : Node2D
{
	[Export]
	protected RayCast2D ray;
	[Export]
	protected Sprite2D debugSprite;
	protected PackedScene newReflectedLaser;

	protected Laser reflected;
	int depth = 0;

	[Export]
	AudioStreamPlayer2D audio;

	Vector2 playerPos {get {return GetNode<PlayerTracker>("/root/PlayerTracker").player.GlobalPosition;} }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (depth==0) CheckHit();
		Vector2 p = GlobalPosition;
		Vector2 dir = Vector2.Right.Rotated(GlobalRotation);
		float d = dir.Dot(playerPos-p);
		float length = ray.IsColliding()? (ray.GetCollisionPoint()-p).Length() : 10000000;
		d = Math.Clamp(d, 0, length);
		Vector2 s = p+dir*d;
		audio.GlobalPosition=s;
	}

	void ScaleLaser(float d){
		if (debugSprite is not null) debugSprite.Scale=new Vector2(d, debugSprite.Scale.Y);
	}

	protected virtual void OnHitObject(GodotObject obj){
		if (obj is Mirror m&& depth<20){
			Vector2 n = ray.GetCollisionNormal();
			Vector2 p = ray.GetCollisionPoint();
			if (reflected is null){
				reflected = newReflectedLaser.Instantiate<Laser>();
				AddChild(reflected);
				reflected.depth = depth+1;
			}
			reflected.GlobalPosition=p+0.01f*n;
			reflected.GlobalRotation = n.Angle()-(GlobalRotation-n.Angle())+Mathf.Pi;
			reflected.CheckHit();

		}
		else{
			RemoveReflectedLaser();
		}
	}

	void CheckHit(){
		ray.ForceRaycastUpdate();
		if (ray.IsColliding()){
			float d = (ray.GetCollisionPoint()-GlobalPosition).Length();
			ScaleLaser(d);
			OnHitObject(ray.GetCollider());
		}
		else {
			ScaleLaser(ray.TargetPosition.Length());
			RemoveReflectedLaser();
		}
	}

	void RemoveReflectedLaser(){
		if (reflected is null) return;
		RemoveChild(reflected);
		reflected.Dispose();
		reflected = null;
	}
}
