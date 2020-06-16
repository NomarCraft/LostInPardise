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
	public GameObject _displayMessagePanel;
	public GameObject _dialoguePanel;
	public GameObject _craftPanel;
	public GameObject _chestPanel;
	public GameObject _climbPanel;

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

	[Space(10)]
	[Header("Compendium/Inventory")]
	public int _currentCompendiumPanelSelected = 0;
	public GameObject _inventoryPanel;
	public GameObject _compendiumInventoryPanel;
	public GameObject[] _compendiumPanels;
	public GameObject _itemCompendiumPanel;
	public GameObject _recipeCompendiumPanel;
	public GameObject _logCompendiumPanel;
	public GameObject _amountSelectionPanel;
	public TextMeshProUGUI _compendiumObjName;
	public TextMeshProUGUI _compendiumObjDescription;
	public ButtonSelection _selectedButton;
	public bool _amountSelectionning;

	[Space(10)]
	[Header("Craft")]
	public RectTransform _craftWindow;
	public TextMeshProUGUI _recipeName;
	public TextMeshProUGUI _recipeDescription;
	public TextMeshProUGUI[] _ingredientsNames;
	public TextMeshProUGUI[] _ingredientsAmount;

	[Space(10)]
	[Header("Dialogue")]
	public float _dialogueSpeed;
	public TextMeshProUGUI _dialogueText;
	public bool _textIsDisplayed = false;

	[Space(10)]
	[Header("DisplayMessage")]
	public float _messageDisplayTime = 0.05f;
	public TextMeshProUGUI _itemDisplayMessageText;

	private Coroutine fadeCoroutine;
	private Coroutine displayMessageBuffer;
	private Coroutine dialogueDisplay;

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

	public void DisplayTemporaryMessageWithColor(TextMeshProUGUI textToDisplay, string text, Color color)
	{
		ChangeText(textToDisplay, text);
		ChangeTextColor(textToDisplay, color);

		if (displayMessageBuffer != null)
			StopCoroutine(displayMessageBuffer);

		displayMessageBuffer = StartCoroutine(DisplayMessage(textToDisplay));
	}

	public void DisplayDialogue(TextMeshProUGUI textToDisplay, string text)
	{
		DisplayElement(_dialoguePanel);

		if (dialogueDisplay != null)
			StopCoroutine(dialogueDisplay);

		StartCoroutine(DisplayTextLetterByLetter(textToDisplay, text));
	}

	public void UpdateCompendiumText(CompendiumData compendiumData)
	{
		ChangeText(_compendiumObjName, compendiumData.itemName);
		ChangeText(_compendiumObjDescription, compendiumData.description);
	}

	public void UpdateRecipeText(RecipeData recipeData)
	{
		ChangeText(_recipeName, recipeData.itemName);
		ChangeText(_recipeDescription, recipeData.description);

		foreach (TextMeshProUGUI text in _ingredientsNames)
		{
			ChangeText(text, "");
		}
		foreach (TextMeshProUGUI text in _ingredientsAmount)
		{
			ChangeText(text, "");
		}

		for (int i = 0; i < recipeData.ingredients.Count; i++)
		{
			ChangeText(_ingredientsAmount[i], recipeData.ingredients[i].amount.ToString());
			ChangeText(_ingredientsNames[i], recipeData.ingredients[i].ingredient.itemName);
		}
	}

	public void ChangeSelectedButton(ButtonSelection buttonSelection)
	{
		_selectedButton = buttonSelection;
	}

	public void ShowAmountSelectionPanel(){
		_amountSelectionPanel.SetActive(true);
		_amountSelectionning = true;
		_amountSelectionPanel.GetComponent<TransferAmountSelection>().amount = 1;
	}

	public void TransferItems(int amount){
		
		if (_amountSelectionPanel != null)
		{
			_amountSelectionPanel.SetActive(false);
			_amountSelectionning = false;
		}

		GameManager.Instance.invDis.TransferItems(_selectedButton ,amount);
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
				yield return null;
			}
		}
	}

	private IEnumerator DisplayMessage(TextMeshProUGUI textToDisplay)
	{
		yield return new WaitForSeconds(_messageDisplayTime);
		ChangeText(textToDisplay, null);
	}

	private IEnumerator DisplayTextLetterByLetter(TextMeshProUGUI textTodisplay, string text)
	{
		string currentText;
		for (int i = 0; i < text.Length + 1; i++)
		{
			currentText = text.Substring(0, i);
			textTodisplay.text = currentText;
			yield return new WaitForSeconds(_dialogueSpeed);
		}

		_textIsDisplayed = true;
	}
}
