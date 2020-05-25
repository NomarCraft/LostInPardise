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

	private Entity _ent;
	public Entity ent
	{
		get
		{
			if (!_ent)
				_ent = GetComponent<Entity>();

			return _ent;
		}
	}

	private GameManager _gm;
	public GameManager gm
	{
		get
		{
			if (!_gm)
				_gm = GameManager.Instance;

			return _gm;
		}
	}

	private Interactor _interactor;
	public Interactor interactor
	{
		get
		{
			if (!_interactor)
				_interactor = GetComponentInChildren<Interactor>();

			return _interactor;
		}
	}

	[Header("References")]
	private UIManager _ui;

	[Space(10)]
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
	private bool _isFastFalling = false;
	private bool _isDeadlyFalling = false;
	private bool _invinsibility = false;

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
	[SerializeField] private float _fallingDamageTreshold = 20f;
	public float fallingDamageTreshold { get { return _fallingDamageTreshold; } }
	[SerializeField] private float _fallingDamageLethalTreshold = 30f;
	public float fallingDamageLethalTreshold { get { return _fallingDamageLethalTreshold; } }
	[SerializeField] private float _respawnTime = 5f;
	public float respawnTime { get { return _respawnTime; } }
	private float _freezeMovement = 1f;

	private Coroutine jumpCoroutine;
	private Coroutine respawnCoroutine;
	private Coroutine invinsibilityCoroutine;

	#region UNITY

	private void Start()
	{
		Initialize();
	}

	private void FixedUpdate()
	{
		if (gm._gamePaused)
			return;

		if (!_invinsibility)
			VelocityCheck();

		Move();
	}

	#endregion

	#region INPUTS

	public void MovementInput (InputAction.CallbackContext context)
	{
		if (gm._gamePaused)
			return;

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
		if (gm._gamePaused)
			return;

		Debug.Log("hit");
		if (context.started)
			Jump();
	}

	public void InteractInput (InputAction.CallbackContext context)
	{
		if (gm._gamePaused)
			return;

		if (context.started)
			Interact();
	}

	#endregion

	#region BEHAVIOUR

	private void Initialize()
	{
		if (gm.uiManager != null)
		{
			_ui = gm.uiManager;
			InitializeUI();
		}


		ent.OnLifeChange += UpdateLifeBar;
		ent.OnDeath += Respawn;
		interactor.OnInteract += UpdateInteraction;
	}

	private void InitializeUI()
	{
		_ui.HideAllElements();
		_ui.DisplayElement(_ui._playerStatusPanel);
	}

	private void Move()
	{
		_isGrounded = OnGround();
		anim.SetBool("IsGrounded",_isGrounded);
		anim.SetBool("isFalling", _isFalling);

		if (_isGrounded && !_isJumping)
		{
			ComputeMovement();
			return;
		}

		if (!_isGrounded && _isJumping)
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

		if (_freezeMovement != 0)
			transform.rotation = targetRot;

		float normalSpeed = ((Mathf.Abs(_movementInput.x) + Mathf.Abs(_movementInput.y)) / 2f) * speed;

		if (_isRunning && normalSpeed / speed >= 0.4f)
		{
			normalSpeed *= runningMultiplyFactor;
			anim.SetFloat("ForwardBlend", 1);
		}

		_direction = transform.forward * (normalSpeed * moveAmount);

		_direction.y = rb.velocity.y;

		if ((Mathf.Abs(_movementInput.x) > 0.1f || Mathf.Abs(_movementInput.y) > 0.1f) && OnSlope())
			_direction += Vector3.down * cc.height * slopeCorrectorForce * Time.deltaTime;

		rb.velocity = _direction * _freezeMovement;
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

		Vector3 jumpVel = rb.velocity;
		jumpVel.y = jumpStrength;

		rb.velocity = jumpVel;
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

	private void VelocityCheck()
	{
		if (rb.velocity.y < -fallingDamageTreshold && !_isFastFalling)
			_isFastFalling = true;

		if (rb.velocity.y < -fallingDamageLethalTreshold && !_isDeadlyFalling)
			_isDeadlyFalling = true;
	}

	private void CheckFallDamage()
	{
		if (_isDeadlyFalling)
		{
			_isDeadlyFalling = false;
			_isFastFalling = false;
			ent.Death();

			if (invinsibilityCoroutine != null)
				StopCoroutine(invinsibilityCoroutine);
			invinsibilityCoroutine = StartCoroutine(Invinsibility());

			return;
		}
		else if (_isFastFalling)
		{
			_isFastFalling = false;
			ent.TakeDamage(1);

			if (invinsibilityCoroutine != null)
				StopCoroutine(invinsibilityCoroutine);
			invinsibilityCoroutine = StartCoroutine(Invinsibility());

			return;
		}
		else
			return;
	}

	private IEnumerator Invinsibility()
	{
		_invinsibility = true;
		yield return new WaitForSeconds(0.5f);
		_invinsibility = false;
	}

	private void Interact()
	{
		if (interactor._interactables.Count == 0)
			return;

		Interactable interactable = interactor._interactables[0];

		if (interactable._interactions[0]._toolRequired)
		{
			Debug.Log("Pas le bon outil");
		}
		else
		{
			interactable.Interaction();
			interactor._interactables.Remove(interactable);
			UpdateInteraction();
		}
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

		/*RaycastHit hit;

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
		}*/

		Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(cc.center) - new Vector3(0, cc.height / 2, 0), cc.radius);
		
		foreach (Collider col in colliders)
		{
			if (col != cc)
			{
				if (_isJumping)
				{
					_isJumping = false;
					_isGrounded = true;
					return true;
				}

				CheckFallDamage();
				_isFastFalling = false;
				_isDeadlyFalling = false;
				_isFalling = false;
				_isGrounded = true;
				return true;
			}
		}

		if (!_isJumping)
			_isFalling = true;

		_isGrounded = false;
		return false;
	}

	#endregion

	#region UI

	private void UpdateLifeBar()
	{
		if (_ui == null)
			return;

		_ui.UpdateScrollbarValue(ent.startingLife, ent.life, _ui._playerLifeScrollbar);
	}

	private void Respawn()
	{
		_ui.StartFade(_ui._fadeImage, 1);
		_freezeMovement = 0f;

		if (respawnCoroutine != null)
			StopCoroutine(respawnCoroutine);

		respawnCoroutine = StartCoroutine(RespawnTimer(respawnTime));
	}

	private IEnumerator RespawnTimer(float respawnTime)
	{
		yield return new WaitForSeconds(respawnTime);
		ent.Respawn();
		yield return new WaitForSeconds(0.5f);
		_ui.StartFade(_ui._fadeImage, 0);
		_freezeMovement = 1f;
	}

	private void UpdateInteraction()
	{
		if (_ui == null)
			return;

		if (interactor._interactables.Count == 0)
		{
			_ui.HideElement(_ui._interactPanel);
			return;
		}

		InteractableType[] interaction = interactor._interactables[0]._interactions;

		_ui.DisplayElement(_ui._interactPanel);
		_ui.UpdateImageAlpha(_ui._interactableCenterImage, 1f);
		_ui.ChangeText(_ui._interactableNameText, interactor._interactables[0]._interactableName);
		_ui.InteractTextReset();

		for (int i = 0; i < interaction.Length; i++)
		{
			_ui.ChangeText(_ui._interactablesNameText[i], interaction[i]._interaction.ToString());

			if (interaction[i]._toolRequired)
			{
				_ui.ChangeTextColor(_ui._interactablesNameText[i], Color.red);
			}
			else
			{
				_ui.ChangeTextColor(_ui._interactablesNameText[i], Color.white);
			}
		}
	}

	#endregion

}
