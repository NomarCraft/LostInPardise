using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialoguable : Interactable
{
	[Space(10)]
	[Header("Dialogue")]
	public int _logId;
	public string _dialogueText;

	public void Interaction(out string text)
	{
		text = _dialogueText;
	}
}
