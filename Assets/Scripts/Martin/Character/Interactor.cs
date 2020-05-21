using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Interactor : MonoBehaviour
{
	public List<Interactable> _interactables;

	private void OnTriggerEnter(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();

		if (interactable != null)
			_interactables.Add(interactable);
	}

	private void OnTriggerExit(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();

		if (interactable != null)
		{
			foreach(Interactable inter in _interactables)
			{
				if (interactable == inter)
				{
					_interactables.Remove(inter);
				}
			}
		}
	}
}
