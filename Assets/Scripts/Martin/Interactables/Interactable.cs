using System.Collections;
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
		None
	};

	public string _interactableName;
	public InteractableType[] _interactions;
}

[System.Serializable]
public class InteractableType
{
	public Interactable.InteractableAction _interaction;
	public bool _toolRequired = false;
}