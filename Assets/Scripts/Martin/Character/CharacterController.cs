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

	[Space(10)]
	[Header("Metrics")]
	[SerializeField] private float _speed;
	public float speed { get { return _speed; } }

	private Vector2 _movementInput;

	private void Awake()
	{
		
	}

	private void FixedUpdate()
	{
		Debug.Log(_movementInput);
		Move();
	}

	#region INPUTS

	public void MovementInput (InputAction.CallbackContext context)
	{
		_movementInput = context.ReadValue<Vector2>();
	}

	#endregion

	#region BEHAVIOUR

	private void Move()
	{
		Vector3 dir;
		dir = new Vector3(_movementInput.x, rb.velocity.y, _movementInput.y);

		Vector3 playerMovement = dir * speed * Time.deltaTime;
		transform.Translate(playerMovement);
	}

	#endregion


}
