using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public GameObject snakePrefab;
	public GameObject snakeAIPrefab;
	
	private GameObject apple, snakePlayer, snakeAI;
	private GameObject choseKeysPanel, pressEnterPanel;
	private InputField leftKeyText, rightKeyText;
	private KeyCode leftKey, rightKey;
	private bool isOddNumber = true;
	private bool gameStarted = false;
	private bool isTheFirstSnake = true;
	private bool readingLeftKey = false;
	private bool readingRightKey = false;
	
    void Start()
    {
		SetVariables();
		ResetPanels();
		ShowPanels();
		
		leftKeyText.characterLimit = 1;
		rightKeyText.characterLimit = 1;
		
		readingLeftKey = true;
    }
	
    void Update()
    {
        if (!gameStarted)
		{
			if (readingLeftKey)
			{
				leftKeyText.ActivateInputField();
				
				if (leftKeyText.text.Length > 0)
				{
					char c = leftKeyText.text[0];
					
					if (char.IsDigit (c) || char.IsLetter (c))
					{
						string cString = c + "";
						leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), cString.ToUpper());
						leftKeyText.DeactivateInputField();
						rightKeyText.ActivateInputField();
						readingLeftKey = false;
						readingRightKey = true;
					}
					
					else
					{
						leftKeyText.text = "";
					}
				}
			}
			
			if (readingRightKey)
			{
				rightKeyText.ActivateInputField();
				
				if (rightKeyText.text.Length > 0)
				{
					char c = rightKeyText.text[0];
					
					if (char.IsDigit (c) || char.IsLetter (c))
					{
						string cString = c + "";
						rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), cString.ToUpper());
						rightKeyText.DeactivateInputField();
						readingRightKey = false;
					}
					
					else
					{
						rightKeyText.text = "";
					}
				}
			}			
		}
    }
	
	public KeyCode GetLeftKey()
	{
		return leftKey;
	}
	
	public KeyCode GetRightKey()
	{
		return rightKey;
	}
	
	public GameObject GetMyApple()
	{
		return apple;
	}
	
	private void SetVariables()
	{
		choseKeysPanel = GameObject.Find("Chose Keys Panel");
		pressEnterPanel = GameObject.Find("Press Enter Panel");
		leftKeyText = GameObject.Find("InputField Left").GetComponent<InputField>();
		rightKeyText = GameObject.Find("InputField Right").GetComponent<InputField>();
	}
	
	private void ResetPanels()
	{
		leftKeyText.text = "";
		rightKeyText.text = "";
		choseKeysPanel.SetActive(false);
		pressEnterPanel.SetActive(false);
	}
	
	private void ShowPanels()
	{		
		if (!isTheFirstSnake)
		{
			pressEnterPanel.SetActive(true);
		}
		
		choseKeysPanel.SetActive(true);
		
		if (isOddNumber)
		{
			if (!isTheFirstSnake)
			{
				choseKeysPanel.GetComponent<RectTransform>().localPosition += new Vector3(-160, 0, 0);
			}
		}
		
		else
		{
			choseKeysPanel.GetComponent<RectTransform>().localPosition += new Vector3(160, 0, 0);
		}
	}
	
}
