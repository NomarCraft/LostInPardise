using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class CharacterController : MonoBehaviour
{
	private Rigidbody _rb;
	public Rigidbody rb
	{
		get
		{
			if (!_rb)
				_rb = GetComponent<Rigidbody>();

			return _rb;
		}
	}

	private CapsuleCollider _cc;
	public CapsuleCollider cc
	{
		get
		{
			if (!_cc)
				_cc = GetComponent<CapsuleCollider>();

			return _cc;
		}
	}

	private Animator _anim;
	public Animator anim
	{
		get
		{
			if (!_anim)
				_anim = GetComponent<Animator>();

			return _anim;
		}
	}

	[Header("Components")]
	private Vector2 _movementInput;
	[SerializeField] private Transform _playerCenter;
	public Transform playerCenter { get { return _playerCenter; } }
	private Transform _camRef;
	public Transform camRef { get { return _camRef; } set { _camRef = value; } }

	[Space(10)]
	[Header("Bools")]
	[SerializeField] private bool _isRunning;
	[SerializeField] private bool _isGrounded;

	[Space(10)]
	[Header("Metrics")]
	[SerializeField] private float _speed = 6f;
	public float speed { get { return _speed; } }
	[SerializeField] private float _runningMultiplyFactor = 2f;
	public float runningMultiplyFactor { get { return _runningMultiplyFactor; } }
	[SerializeField] private float _rotSpeed = 7f;
	public float rotSpeed { get { return _rotSpeed; } }
	[SerializeField] private float _gravityStrength = 9.8f;
	public float gravityStrength { get { return _gravityStrength; } }
	[SerializeField] private float _fallSpeed = 4f;
	public float fallSpeed { get { return _fallSpeed; } }

	private void Awake()
	{
		
	}

	private void FixedUpdate()
	{
		GroundDetection();
		ComputeMovement();
	}

	#region INPUTS

	public void MovementInput (InputAction.CallbackContext context)
	{
		_movementInput = context.ReadValue<Vector2>();

		if (_movementInput.x >= -0.2f && _movementInput.x <= 0.2f)
			_movementInput.x = 0;

		if (_movementInput.y >= -0.2f && _movementInput.y <= 0.2f)
			_movementInput.y = 0;

		if (_movementInput != Vector2.zero)
			anim.SetFloat("Moving", 1);
		else
			anim.SetFloat("Moving", 0);

		if (_isRunning)
			return;

		anim.SetFloat("ForwardBlend", (Mathf.Abs(_movementInput.x) + Mathf.Abs(_movementInput.y)) / 2);
	}

	public void RunInput (InputAction.CallbackContext context)
	{
		if (context.started)
			_isRunning = true;
		else if (context.canceled)
			_isRunning = false;
	}

	#endregion

	#region BEHAVIOUR

	private void ComputeMovement()
	{
		Vector3 cameraForward = camRef.forward;
		Vector3 v = _movementInput.y * cameraForward;
		Vector3 h = _movementInput.x * camRef.right;

		Vector3 moveDir = (v + h).normalized;
		float moveAmount = Mathf.Clamp01(Mathf.Abs(_movementInput.x) + Mathf.Abs(_movementInput.y));

		Vector3 targetDir = moveDir;
		targetDir.y = 0;
		if (targetDir == Vector3.zero)
			targetDir = transform.forward;

		Quaternion lookDir = Quaternion.LookRotation(targetDir);
		Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * rotSpeed);
		transform.rotation = targetRot;

		float normalSpeed = ((Mathf.Abs(_movementInput.x) + Mathf.Abs(_movementInput.y)) / 2f) * speed;

		if (_isRunning)
		{
			normalSpeed *= runningMultiplyFactor;
			anim.SetFloat("ForwardBlend", 1);
		}

		Vector3 dir = transform.forward * (normalSpeed * moveAmount);
		dir.y = rb.velocity.y;

		rb.velocity = dir;
	}

	private float Gravity(float y)
	{
		float gravity = y;
		
		if (_isGrounded)
			return gravity = 0f;

		gravity -= gravityStrength * fallSpeed * Time.deltaTime;

		return gravity;
	}
	/*
	private void Rotate()
	{
		if (_movementInput == Vector2.zero)
			return;

		float angle;
		angle = Mathf.Atan2(-_movementInput.y, _movementInput.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
	}

	private void Move()
	{
		if (_movementInput == Vector2.zero)
			return;
			
		Vector3 playerMovement = Vector3.forward * speed * Time.deltaTime;
		transform.Translate(playerMovement);
	}*/

	#endregion

	#region PHYSICS

	private void GroundDetection()
	{
		if (_isGrounded)
			return;

		if (Physics.Raycast(transform.TransformPoint(cc.center + new Vector3(0, -cc.height / 2f, 0)), Vector3.down, 5f))
			_isGrounded = true;
		else
			_isGrounded = false;

		Debug.Log(_isGrounded);
	}

	private void OnCollisionEnter(Collision collision)
	{
		GroundDetection();
	}

	#endregion


}
