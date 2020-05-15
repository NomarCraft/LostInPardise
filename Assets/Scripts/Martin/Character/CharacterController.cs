using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
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
	[SerializeField] private Transform _playerCenter;
	public Transform playerCenter { get { return _playerCenter; } }
	private Transform _camRef;
	public Transform camRef { get { return _camRef; } set { _camRef = value; } }

	[Space(10)]
	[Header("Metrics")]
	[SerializeField] private float _speed;
	public float speed { get { return _speed; } }
	[SerializeField] private float _rotSpeed = 7f;
	public float rotSpeed { get { return _rotSpeed; } }

	private Vector2 _movementInput;

	private void Awake()
	{
		
	}

	private void FixedUpdate()
	{
		Debug.Log(_movementInput);
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

		anim.SetFloat("ForwardBlend", (Mathf.Abs(_movementInput.x) + Mathf.Abs(_movementInput.y)) / 2);
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
		Vector3 dir = transform.forward * (speed * moveAmount);
		rb.velocity = dir;
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


}
