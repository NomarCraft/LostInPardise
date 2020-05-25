using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : Interactable
{
	[Space(10)]
	[Header("Gather")]
	public int _gatheredItemId;
	public int _gatheredItemAmount;

	public override void Interaction()
	{
		Debug.Log("C'est la même mais là c'est un Gatherable");
	}
}
