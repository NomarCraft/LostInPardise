﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public enum InteractableAction
	{
		Gather,
		Chop,
		Mine,
		Craft,
		Store,
		Interact,
		None
	};

	public string _interactableName;
	public InteractableType[] _interactions;
	public bool _isActive = true;

	public virtual void Interaction()
	{
		Debug.Log("Interact Complete");
	}
}

[System.Serializable]
public class InteractableType
{
	public Interactable.InteractableAction _interaction;
	public bool _toolRequired = false;
}