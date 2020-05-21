using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[Header("Panels")]
	public GameObject[] allPanelsParent;
	public GameObject _playerStatusPanel;
	public GameObject _interactPanel;

	[Space(10)]
	[Header("PlayerStatus")]
	public Scrollbar _playerLifeScrollbar;

	[Space(10)]
	[Header("Interactable")]
	public Image _interactableCenterImage;
	public TextMeshProUGUI _interactableNameText;
	public Image[] _buttonsImage;
	public Image _xButtonImage;
	public Image _yButtonImage;
	public Image _bButtonImage;
	public TextMeshProUGUI[] _buttonsName;
	public TextMeshProUGUI[] _interactablesNameText;
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
		foreach (GameObject panel in allPanelsParent)
		{
			panel.SetActive(false);
		}
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

	public void ChangeText(TextMeshProUGUI textToChange, string text)
	{
		textToChange.text = text;
	}

	public void ChangeTextColor(TextMeshProUGUI textToChange, Color color)
	{
		textToChange.color = color;
	}

	public void UpdateImageAlpha(Image imageToChange, float alpha)
	{
		imageToChange.color = new Color(imageToChange.color.r, imageToChange.color.g, imageToChange.color.b, alpha);
	}

	public void InteractTextReset()
	{
		foreach (TextMeshProUGUI text in _interactablesNameText)
		{
			text.text = "";
		}
	}
}
