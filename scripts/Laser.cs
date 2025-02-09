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

	protected Laser split1;
	protected Laser split2;

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

		if (obj is Splitter s && depth<20){
			Vector2 p = ray.GetCollisionPoint();
			Vector2 c = s.GlobalPosition;
			Vector2 r = p-c;
			if (split1 is null){
				split1 = newReflectedLaser.Instantiate<Laser>();
				AddChild(split1);
				split1.depth=depth+1;
			}
			if (split2 is null){
				split2 = newReflectedLaser.Instantiate<Laser>();
				AddChild(split2);
				split2.depth=depth+1;
			}
			split1.GlobalPosition = c + r.Rotated(2*Mathf.Pi/3);
			split2.GlobalPosition = c + r.Rotated(-2*Mathf.Pi/3);
			split1.GlobalRotation = r.Angle()+2*Mathf.Pi/3;
			split2.GlobalRotation = r.Angle()-2*Mathf.Pi/3;

			split1.CheckHit();
			split2.CheckHit();
		}
		else{ RemoveSplitLasers();}
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
			RemoveSplitLasers();
		}
	}

	void RemoveReflectedLaser(){
		if (reflected is null) return;
		RemoveChild(reflected);
		reflected.Dispose();
		reflected = null;
	}
	void RemoveSplitLasers(){
		if (split1 is not null){
		RemoveChild(split1);
		split1.Dispose();
		split1 = null;
		}
		if (split2 is not null){
			RemoveChild(split2);
			split2.Dispose();
			split2 = null;
		}
	}
}
