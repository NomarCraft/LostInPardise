﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[Header("Panels")]
	public GameObject _allPanelsParent;
	public GameObject _playerStatusPanel;

	[Header("Fields")]
	public Scrollbar _playerLifeScrollbar;

	private void Start()
	{

	}

	public void HideAllElements()
	{
		if (_allPanelsParent.activeSelf != false)
			_allPanelsParent.SetActive(false);
	}

	public void DisplayElement(GameObject obj)
	{
		if (obj != null && obj.activeSelf != true)
			obj.SetActive(true);
	}

	public void HideElement(GameObject obj)
	{
		if (obj != null && obj.activeSelf != false)
			obj.SetActive(false);
	}

	public void UpdateScrollbarValue(float totalAmount, float amount, Scrollbar scrollbar)
	{
		scrollbar.size = amount / totalAmount;
	}

}
