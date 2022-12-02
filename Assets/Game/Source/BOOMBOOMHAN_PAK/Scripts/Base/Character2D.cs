using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Character2D : Character
{
	protected new Rigidbody2D rigidbody;

	public Rigidbody2D RBody
	{
		get
		{
			return rigidbody;
		}
	}
	
	#region INTERNAL
	
	private bool isGround = false;
	private float jumpBonusValidTime = .0f;
	
	#endregion
	
	
	[Header("Rigidbody设置")]
	
	[DisplayName("俯视角")]
	public bool IsOverlook;

	[SerializeField, DisplayName("质量")]
	protected float mass;

	[SerializeField, DisplayName("重力大小")]
	protected float gravityScale;

	[SerializeField, DisplayName("线性阻尼")]
	protected float linearDrag;

	[SerializeField, DisplayName("角阻尼")]
	protected float angularDrag;
	
	[SerializeField, DisplayName("跳跃力")]
	protected float jumpForce;

	[SerializeField, DisplayName("高跳力")]
	protected float jumpBonusForce;

	[SerializeField, DisplayName("高跳持续时间")]
	protected float jumpBonusDuration;
	
	[SerializeField, DisplayName("目标跳跃图层")]
	private LayerMask targetJumpingGround;

	[SerializeField, DisplayName("开启相机跟随(未实装)")]
	private bool enableCameraFollowing;

	public LayerMask TargetJumpingGround
	{
		get
		{
			return targetJumpingGround;
		}
		set
		{
			targetJumpingGround = value;
		}
	}
	
	protected bool still
	{
		get
		{
			return rigidbody.velocity.NearlyZero();
		}
	}

	public Character2D()
	{
		moveSpeed = 10.0f;
		rigidbody = null;
		IsOverlook = false;
		mass = 7.5f;
		linearDrag = 1f;
		angularDrag = 5f;
		gravityScale = 1f;
		jumpForce = 7.5f;
		jumpBonusForce = 0.3f;
		jumpBonusDuration = 0.2f;
	}

	private bool overlookFixed;
	protected virtual void Awake()
	{
		overlookFixed = IsOverlook;
		
		if (!TryGetComponent(out rigidbody))
		{
			rigidbody = gameObject.AddComponent<Rigidbody2D>();
		}
		rigidbody.mass = mass;
		rigidbody.drag = linearDrag;
		rigidbody.angularDrag = angularDrag;
		rigidbody.gravityScale = overlookFixed ? 0f : gravityScale;

		if (TargetJumpingGround == LayerMask.GetMask("Nothing"))
		{
			targetJumpingGround = LayerMask.GetMask("Default");
		}
	}

	protected virtual void Start()
	{
		
	}

	protected virtual void Update()
	{
		isGround = (!overlookFixed) && rigidbody.IsTouchingLayers(targetJumpingGround);
		if (enableCameraFollowing && followedCamera)
		{
			Vector2 delta = transform.position - followedCamera.transform.position;
			float x = Mathf.Lerp(0f, delta.x, lerpedPercentage);
			float y = Mathf.Lerp(0f, delta.y, lerpedPercentage);
			followedCamera.transform.position += new Vector3(x, y, 0f);
		}
	}

	protected virtual void FixedUpdate()
	{
		
	}

	protected virtual void OnDestroy()
	{
		
	}

	public override void MoveHorizontal(float val)
    {
	    rigidbody.AddForce(new Vector2(val * moveSpeed, 0f));
    }

    public override void MoveVertical(float val)
    {
	    rigidbody.AddForce(new Vector2(0f, val * moveSpeed));
    }

    public override void Jump()
    {
	    if (!isGround) return;
	    rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
	    jumpBonusValidTime = Time.time + jumpBonusDuration;
    }

    public override void JumpBonus()
    {
	    if (isGround || jumpBonusValidTime <= Time.time) return;
	    rigidbody.AddForce(new Vector2(0f, jumpBonusForce), ForceMode2D.Impulse);
    }
}
