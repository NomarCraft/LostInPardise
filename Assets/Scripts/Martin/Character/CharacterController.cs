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
	private Vector3 _direction;
	[SerializeField] private Transform _playerCenter;
	public Transform playerCenter { get { return _playerCenter; } }
	private Transform _camRef;
	public Transform camRef { get { return _camRef; } set { _camRef = value; } }

	[Space(10)]
	[Header("Bools")]
	[SerializeField] private bool _isRunning;
	[SerializeField] private bool _isGrounded;
	[SerializeField] private bool _isJumping;
	private bool _jumpSafety = false;
	[SerializeField] private bool _isFalling = false;
	public bool isFalling { get { return _isFalling; } }

	[Space(10)]
	[Header("Metrics")]
	[SerializeField] private float _speed = 6f;
	public float speed { get { return _speed; } }
	[SerializeField] private float _runningMultiplyFactor = 2f;
	public float runningMultiplyFactor { get { return _runningMultiplyFactor; } }
	[SerializeField] private float _rotSpeed = 7f;
	public float rotSpeed { get { return _rotSpeed; } }
	[SerializeField] private float _groundDetectionRayLength = 0.5f;
	public float groundDetectionRayLength { get { return _groundDetectionRayLength; } }
	[SerializeField] private float _slopeCorrectorForce = 2f;
	public float slopeCorrectorForce { get { return _slopeCorrectorForce; } }
	[SerializeField] private float _jumpStrength = 6f;
	private float jumpStrength { get { return _jumpStrength; } }
	[SerializeField] private float _fallSpeed = 4f;
	public float fallSpeed { get { return _fallSpeed; } }
	[SerializeField] private float _fallingTreshold = 10f;
	public float fallingTreshold { get { return _fallingTreshold; } }

	private Coroutine jumpCoroutine;

	private void Awake()
	{
		
	}

	private void FixedUpdate()
	{
		Move();
		Debug.Log(OnGround());
	}

	#region INPUTS

	public void MovementInput (InputAction.CallbackContext context)
	{
		_movementInput = context.ReadValue<Vector2>();

		if (Mathf.Abs(_movementInput.x) <= 0.1f)
			_movementInput.x = 0;

		if (Mathf.Abs(_movementInput.y) <= 0.1f)
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

	public void JumpInput (InputAction.CallbackContext context)
	{
		Debug.Log("hit");
		if (context.started)
			Jump();
	}

	#endregion

	#region BEHAVIOUR

	private void Move()
	{
		anim.SetBool("IsGrounded", OnGround());
		anim.SetBool("isFalling", _isFalling);

		if (OnGround() && !_isJumping)
		{
			ComputeMovement();
			return;
		}

		if (!OnGround() && _isJumping)
		{
			Jumping();
			return;
		}

		if (_isFalling)
		{
			Gravity();
			return;
		}
	}

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

		_direction = transform.forward * (normalSpeed * moveAmount);

		_direction.y = rb.velocity.y;

		if ((Mathf.Abs(_movementInput.x) > 0.1f || Mathf.Abs(_movementInput.y) > 0.1f) && OnSlope())
			_direction += Vector3.down * cc.height * slopeCorrectorForce * Time.deltaTime;

		rb.velocity = _direction;
	}

	private void Gravity()
	{
		rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, 0, fallSpeed * Time.deltaTime), rb.velocity.y, Mathf.Lerp(rb.velocity.z, 0, fallSpeed * Time.deltaTime));
		rb.velocity -= new Vector3(0, fallSpeed * 2 * Time.deltaTime, 0);
	}

	private void Jump()
	{
		if (!_isGrounded)
			return;

		if (jumpCoroutine != null)
			StopCoroutine(jumpCoroutine);
		jumpCoroutine = StartCoroutine(JumpCoroutine());

		Debug.Log("Jumping");
		rb.AddForce(Vector3.up * _jumpStrength);
		_isJumping = true;
		_isGrounded = false;
		anim.SetTrigger("Jump");
	}

	private void Jumping()
	{
		if (rb.velocity.y < -fallingTreshold)
		{
			_isJumping = false;
			_isFalling = true;
			return;
		}
	}

	private IEnumerator JumpCoroutine()
	{
		_jumpSafety = true;
		yield return new WaitForSeconds(0.1f);
		_jumpSafety = false;
	}

	#endregion

	#region PHYSICS
	
	private bool OnSlope()
	{
		if (_isJumping)
			return false;

		RaycastHit hit;

		if (Physics.Raycast(transform.TransformPoint(cc.center), Vector3.down, out hit, cc.height / 2 * groundDetectionRayLength))
			if (hit.normal != Vector3.up)
				return true;

		return false;
	}

	private bool OnGround()
	{
		if (_jumpSafety)
			return false;

		RaycastHit hit;

		if (Physics.Raycast(transform.TransformPoint(cc.center), Vector3.down, out hit, cc.height / 2 * groundDetectionRayLength))
		{
			if (_isJumping)
			{
				_isJumping = false;
				_isGrounded = true;
				return true;
			}
			_isFalling = false;
			_isGrounded = true;
			return true;
		}

		if (!_isJumping)
			_isFalling = true;

		_isGrounded = false;
		return false;
	}

	#endregion


}
