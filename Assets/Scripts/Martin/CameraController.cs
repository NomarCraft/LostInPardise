using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class CameraController : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private CharacterController _player;
	public CharacterController player { get { return _player; } }
	[SerializeField] private Transform _playerCenter;

	private Vector2 _lookMovement;

	[Space(10)]
	[Header("Metrics")]
	[SerializeField] private float _rotationRadius = 4f;
	public float rotationRadius { get { return _rotationRadius; } }
	[SerializeField] private float _heightOffset = 3f;
	public float heightOffset { get { return _heightOffset; } }
	[SerializeField] private float _minimumHeightOffset = 2f;
	public float minimumHeightOffset { get { return _minimumHeightOffset; } }
	[SerializeField] private float _maximumHeightOffset = 6f;
	public float maximumHeightOffset { get { return _maximumHeightOffset; } }
	[SerializeField] private float _angularSpeed;
	public float angularSpeed { get { return _angularSpeed; } }
	private float _angle = 0f;

	public void LookInput(InputAction.CallbackContext context)
	{
		_lookMovement = context.ReadValue<Vector2>();

		if (_lookMovement.x >= -0.25f && _lookMovement.x <= 0.25f)
			_lookMovement.x = 0;

		if (_lookMovement.y >= -0.25f && _lookMovement.y <= 0.25f)
			_lookMovement.y = 0;
	}

	private void Start()
	{
		Initialize();
	}

	private void FixedUpdate()
	{
		CameraMovement();
		AngleNormalize();
	}

	private void Initialize()
	{
		_player = GameObject.FindObjectOfType<CharacterController>();
		_playerCenter = _player.playerCenter;
		_player.camRef = this.transform;
	}

	private void CameraMovement()
	{
		Vector3 dir;
		dir = new Vector3(_playerCenter.position.x + Mathf.Cos(_angle) * rotationRadius, _playerCenter.position.y + heightOffset, _playerCenter.position.z + Mathf.Sin(_angle) * rotationRadius);

		_angle += _lookMovement.x * angularSpeed * Time.deltaTime;
		_heightOffset = Mathf.Clamp(_heightOffset + _lookMovement.y * angularSpeed * Time.deltaTime, minimumHeightOffset, maximumHeightOffset);

		transform.position = dir;
		transform.LookAt(_playerCenter);
	}

	private void AngleNormalize()
	{
		if (_angle >= 360f || _angle <= -360f)
			_angle = 0f;
	}
}
