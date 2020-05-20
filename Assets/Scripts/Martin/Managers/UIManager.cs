using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[Header("Panels")]
	public GameObject _allPanelsParent;
	public GameObject _playerStatusPanel;
	public GameObject _interactPanel;

	[Space(10)]
	[Header("PlayerStatus")]
	public Scrollbar _playerLifeScrollbar;

	[Space(10)]
	[Header("Interactable")]
	public Image _interactableCenterImage;
	public TextMeshProUGUI _interactableNameText;
	public Image _xButtonImage;
	public Image _yButtonImage;
	public Image _bButtonImage;
	public TextMeshProUGUI _xNameText;
	public TextMeshProUGUI _yNameText;
	public TextMeshProUGUI _bNameText;
	public TextMeshProUGUI _xInteractableNameText;
	public TextMeshProUGUI _yInteractableNameText;
	public TextMeshProUGUI _bInteractableNameText;


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
