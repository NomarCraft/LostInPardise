using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Interactor : MonoBehaviour
{
	public delegate void InteractCallBack();

	public InteractCallBack OnInteract;

	public List<Interactable> _interactables = new List<Interactable>();

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		Interactable interactable = other.GetComponent<Interactable>();

		if (interactable != null && interactable._isActive)
		{
			_interactables.Add(interactable);
			OnInteract();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();

		if (interactable != null && interactable._isActive)
		{
			foreach(Interactable inter in _interactables)
			{
				if (interactable == inter)
				{
					_interactables.Remove(inter);
					OnInteract();
					return;
				}
			}
		}
	}
}
