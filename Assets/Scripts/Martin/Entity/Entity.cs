﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	[Header("Bools")]
	[SerializeField] private bool _invincible = false;
	public bool invincible { get { return _invincible; } }
	[SerializeField] private bool _canRespawn = false;
	public bool canRespawn { get { return _canRespawn; } }

	[Space(10)]
	[Header("Metrics")]
	private int _startingLife;
	[SerializeField] private int _life = 5;
	public int life { get { return _life; } }

	private Vector3 _startPos;

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
		//Play Anim and Sound
		Debug.Log(life);

		if (_life <= 0)
			Death();
	}

	public void Death()
	{
		Debug.Log("AIE");
		if (!canRespawn)
			return;

		Respawn();
	}

	public void Respawn()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.position = _startPos;
	}
}
