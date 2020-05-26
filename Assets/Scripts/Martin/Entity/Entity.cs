using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	[Header("Bools")]
	[SerializeField] private bool _invincible = false;
	public bool invincible { get { return _invincible; } }
	[SerializeField] private bool _canRespawn = false;
	public bool canRespawn { get { return _canRespawn; } }
	[SerializeField] private bool _respawnPilotedByScript = true;
	public bool respawnPilotedByScript { get { return _respawnPilotedByScript; } }

	[Space(10)]
	[Header("Metrics")]
	private int _startingLife;
	public int startingLife { get { return _startingLife; } }
	[SerializeField] private int _life = 5;
	public int life { get { return _life; } }
	private Vector3 _startPos;

	//Events
	public delegate void DefaultCallback();

	public DefaultCallback OnLifeChange;
	public DefaultCallback OnDeath;

	private void Start()
	{
		_startPos = transform.position;
		_startingLife = _life;
	}

	public void TakeDamage(int amount)
	{
		if (invincible)
			return;

		_life -= amount;

		OnLifeChange();

		if (_life <= 0)
			Death();
	}

	public void Death()
	{
		_life = 0;

		if (!canRespawn)
			return;

		OnLifeChange();
		OnDeath();

		if (!respawnPilotedByScript)
			Respawn();
	}

	public void Respawn()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.position = _startPos;

		_life = _startingLife;
		OnLifeChange();
	}
}
