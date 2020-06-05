using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Interactable
{
	private InventoryObject _inv;
	public InventoryObject inv
	{
		get
		{
			if (!_inv)
			{
				_inv = GetComponent<InventoryObject>();
			}

			return _inv;
		}
	}

	public void Interaction(out InventoryObject inventory)
	{
		inventory = inv;
	}
}
