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
	private float _rotationRadiusChecked;
	[SerializeField] private float _heightOffset = 3f;
	public float heightOffset { get { return _heightOffset; } }
	private float _heightCorrection;
	[SerializeField] private float _minimumHeightOffset = 2f;
	public float minimumHeightOffset { get { return _minimumHeightOffset; } }
	[SerializeField] private float _maximumHeightOffset = 6f;
	public float maximumHeightOffset { get { return _maximumHeightOffset; } }
	[SerializeField] private float _angularSpeed;
	public float angularSpeed { get { return _angularSpeed; } }
	private float _angle = 0f;
	private float _angleCorrection;
	private float _previousAngle;

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
		_previousAngle = _angle;
	}

	private void CameraMovement()
	{
		Vector3 dir;
		Vector3 cameraOriginalPosition = new Vector3(_playerCenter.position.x + Mathf.Cos(_angle) * (rotationRadius + _rotationRadiusChecked), _playerCenter.position.y + heightOffset, _playerCenter.position.z + Mathf.Sin(_angle) * (rotationRadius + _rotationRadiusChecked));

		if (SightCheck(transform.position))
		{
			Vector3 relativePoint = _playerCenter.InverseTransformPoint(transform.position);

			if (relativePoint.x > 0)
				_angle -= angularSpeed * Time.deltaTime;
			else if (relativePoint.x < 0)
				_angle += angularSpeed * Time.deltaTime;
		}
		AngleCheck();
		HeightCheck();

		if (!DirCheck(_playerCenter.position, Vector3.up, 10)) 
		{
			_rotationRadiusChecked = _rotationRadius / 2.25f;
			if (_heightOffset > _maximumHeightOffset / 2)
				_heightOffset = 3;
		}
		else
		{
			if (DirCheck(transform.position, Vector3.up, 10))
				_rotationRadiusChecked = 0;
		}

		_angle += (_angleCorrection + _lookMovement.x) * angularSpeed * Time.deltaTime;
		_previousAngle = _angle;
		_heightOffset = Mathf.Clamp(_heightOffset + _heightCorrection + _lookMovement.y * angularSpeed * Time.deltaTime, minimumHeightOffset, maximumHeightOffset);

		dir = new Vector3(_playerCenter.position.x + Mathf.Cos(_angle) * (rotationRadius - _rotationRadiusChecked), _playerCenter.position.y + heightOffset, _playerCenter.position.z + Mathf.Sin(_angle) * (rotationRadius - _rotationRadiusChecked));
		transform.position = Vector3.Lerp(transform.position, dir, angularSpeed * 3 * Time.deltaTime);
		transform.LookAt(_playerCenter);
	}

	private void AngleNormalize()
	{
		if (_angle >= 360f || _angle <= -360f)
			_angle = 0f;
	}

	private void AngleCheck()
	{
		
		if (!DirCheck(transform.position, -transform.right, 1.5f))
		{
			if (_lookMovement.x < 0f)
			{
				_lookMovement.x = 0;
			}
			return;
		}
		if (!DirCheck(transform.position, transform.right, 1.5f))
		{
			if (_lookMovement.x > 0f)
			{
				_lookMovement.x = 0;
			}
			return;
		}

		_angleCorrection = 0;
		return;
		
	}

	private void HeightCheck()
	{
		if (!DirCheck(transform.position, Vector3.up, 1f))
		{
			_lookMovement.y = 0;
			_heightCorrection -= angularSpeed / 10 * Time.deltaTime;
			return;
		}
		if (!DirCheck(transform.position, Vector3.down, 1f))
		{
			_lookMovement.y = 0;
			_heightCorrection += angularSpeed / 10 * Time.deltaTime;
			return;
		}

		_heightCorrection = 0f;
		return;
	}

	private bool SightCheck(Vector3 pos)
	{
		RaycastHit hit;
		Vector3 dir = (_playerCenter.position - pos).normalized;

		if (Physics.Raycast(pos, dir, out hit, Vector3.Distance(pos, _playerCenter.position)))
			if (hit.transform.gameObject.GetComponent<CharacterController>() == null)
			{
				return true;
			}
			else
			{
				Vector3 inverseDir = (pos - _playerCenter.position).normalized;
				if (Physics.Raycast(_playerCenter.position, inverseDir, out hit, Vector3.Distance(_playerCenter.position, pos)))
				{
					Debug.Log(hit.transform.name);
					if (hit.transform.gameObject.GetComponent<CameraController>() == null)
						return true;
					else
						return false;
				}
				return false;
			}

		return false;
	}

	private bool DirCheck(Vector3 origin, Vector3 dir, float dist)
	{
		if (Physics.Raycast(origin, dir, dist))
		{
			return false;
		}

		return true;
	}
}
