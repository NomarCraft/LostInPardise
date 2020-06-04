using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : Interactable
{
	[Space(10)]
	[Header("Gather")]
	public int _gatheredItemId;
	public int _gatheredItemAmount;

	[Header("Tool")]
	public int _toolRequiredId;

	public void Interaction(out ItemData item)
	{
		GameManager.Instance.inv.AddItem(_gatheredItemId, _gatheredItemAmount);
		item = GameManager.Instance.comp.GetItemReference(_gatheredItemId);
	}
}
