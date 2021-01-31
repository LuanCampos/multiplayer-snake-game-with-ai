using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public GameObject snakePrefab;
	public GameObject snakeAIPrefab;
	
	private GameObject apple, snakePlayer, snakeAI;
	private GameObject choseKeysPanel, pressEnterPanel;
	private TMP_Text leftKeyText, rightKeyText;
	private KeyCode leftKey, rightKey;
	private bool isOddNumber = true;
	private bool gameStarted = false;
	private bool isTheFirstSnake = true;
   
    void Start()
    {
		SetVariables();
		ResetPanels();
		ShowPanels();
    }

    void Update()
    {
        if (!gameStarted)
		{
			
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
		leftKeyText = GameObject.Find("Key 1").GetComponent<TMPro.TextMeshProUGUI>();
		rightKeyText = GameObject.Find("Key 2").GetComponent<TMPro.TextMeshProUGUI>();
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
