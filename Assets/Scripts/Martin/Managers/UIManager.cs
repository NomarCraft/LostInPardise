using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[Header("Panels")]
	public Image _fadeImage;
	public GameObject[] allPanelsParent;
	public GameObject _playerStatusPanel;
	public GameObject _interactPanel;
	public GameObject _compendiumPanel;

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

	private Coroutine fadeCoroutine;

	private void Start()
	{

	}

	private void Update()
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

		if (scrollbar.size == 0)
			scrollbar.GetComponentInChildren<RectTransform>().gameObject.SetActive(false);
		else
			scrollbar.GetComponentInChildren<RectTransform>().gameObject.SetActive(true);
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

	public void StartFade(Image imageToFade, int mode)
	{
		if (fadeCoroutine != null)
			StopCoroutine(fadeCoroutine);

		fadeCoroutine = StartCoroutine(Fade(imageToFade, mode));
	}

	private IEnumerator Fade(Image imageToFade, int mode)
	{
		if (mode == 0)
		{
			for (float i = 1; i >= 0; i = imageToFade.color.a)
			{
				imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, Mathf.Lerp(imageToFade.color.a, 0, Time.deltaTime * 3f));
				yield return null;
			}
		}
		else if (mode == 1)
		{
			for (float i = 0; i <= 1; i = imageToFade.color.a)
			{
				imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, Mathf.Lerp(imageToFade.color.a, 1, Time.deltaTime * 3f));
				//imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, i);
				yield return null;
			}
		}
	}
}
