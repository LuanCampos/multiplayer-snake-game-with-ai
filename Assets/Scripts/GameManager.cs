using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public GameObject snakePrefab, snakeAIPrefab, applePrefab, scorePanel;
	
	private GameObject apple, snakePlayer, snakeAI;
	private GameObject choseKeysPanel, pressEnterPanel, resetButton;
	private InputField leftKeyText, rightKeyText;
	private KeyCode leftKey, rightKey;
	private List<char> keysUsing = new List<char>();
	private bool isOddNumber = true;
	private bool gameStarted = false;
	private bool isTheFirstSnake = true;
	private bool readingLeftKey = false;
	private bool readingRightKey = false;
	private int frameCount = 0;	
	private int snakeCount = 0;
	
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
			StartGame();
			ReadKeys();
			ChangeTheSnake();
			SetTheSnakeUp();
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
	
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void SnakeDie()
	{
		if (snakeCount == 1)
		{
			resetButton.SetActive(true);
		}
		
		else
		{
			snakeCount --;
		}
	}
	
	private void SetVariables()
	{
		choseKeysPanel = GameObject.Find("Chose Keys Panel");
		pressEnterPanel = GameObject.Find("Press Enter Panel");
		leftKeyText = GameObject.Find("InputField Left").GetComponent<InputField>();
		rightKeyText = GameObject.Find("InputField Right").GetComponent<InputField>();
		resetButton = GameObject.Find("Reset Button");
		resetButton.SetActive(false);
		leftKeyText.characterLimit = 1;
		rightKeyText.characterLimit = 1;
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
		
		readingLeftKey = true;
	}
	
	private void StartGame()
	{
		if (!isTheFirstSnake && !readingRightKey)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				leftKeyText.DeactivateInputField();
				rightKeyText.DeactivateInputField();
				choseKeysPanel.SetActive(false);
				pressEnterPanel.SetActive(false);
				
				foreach (GameObject snakeHead in GameObject.FindGameObjectsWithTag("SnakeHead"))
				{
					snakeHead.transform.parent.gameObject.GetComponent<Snake>().SetIsAlive(true);
				}
			}
		}
	}
	
	private void ReadKeys()
	{
		if (readingLeftKey)
		{
			leftKeyText.ActivateInputField();
			
			if (leftKeyText.text.Length > 0)
			{
				char c = leftKeyText.text[0];
				
				if ((char.IsDigit (c) || char.IsLetter (c)) && !keysUsing.Contains(c))
				{
					string cString = c + "";
					leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), cString.ToUpper());
					keysUsing.Add(c);
					
					leftKeyText.DeactivateInputField();
					rightKeyText.ActivateInputField();
					readingLeftKey = false;
					readingRightKey = true;
					
					SpawnTheSnake();
					SpawnScorePanel();
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
				
				if ((char.IsDigit (c) || char.IsLetter (c)) && !keysUsing.Contains(c))
				{
					string cString = c + "";
					rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), cString.ToUpper());
					keysUsing.Add(c);
					
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
	
	private void SpawnTheSnake()
	{
		Vector3 pos = new Vector3 (0, 0, 0);
			
		if (isOddNumber)
		{
			pos = new Vector3 (-15 + (snakeCount * 3), 0, 0);
		}
			
		else
		{
			pos = new Vector3 (15 - (snakeCount * 3), 0, 0);
		}
		
		snakePlayer = Instantiate(snakePrefab, new Vector3(0, 0, -12) + pos, Quaternion.identity);
		snakeAI = Instantiate(snakeAIPrefab, new Vector3(0, 0, 10) + pos, Quaternion.identity);
		apple = Instantiate(applePrefab, new Vector3(0, 0, 0) + pos, Quaternion.identity);		
		SetColor();
	}
	
	private void ChangeTheSnake()
	{
		if (!readingLeftKey && !readingRightKey)
		{
			if (Input.GetKey(rightKey))
			{
				if (frameCount > 50)
				{
					snakeAI.GetComponent<Snake>().ChangeSnakeType();
					snakePlayer.GetComponent<Snake>().ChangeSnakeType();
					frameCount = 0;
				}
				frameCount ++;
			}
		}
	}
	
	private void SetTheSnakeUp()
	{
		if (!readingLeftKey && !readingRightKey)
		{
			if (!Input.GetKey(rightKey))
			{
				snakePlayer.GetComponent<PlayerController>().SetMyInput(leftKey, rightKey);
				snakeAI.GetComponent<AIController>().ChangeMyApple(apple);
				snakeAI.GetComponent<Snake>().SetSnakePair(snakePlayer);
				apple.GetComponent<Apple>().SetMySnakes(snakePlayer.transform.GetChild(0).gameObject, snakeAI.transform.GetChild(0).gameObject);
				
				frameCount = 0;
				isTheFirstSnake = false;
				isOddNumber = !isOddNumber;
				snakeCount ++;
				ResetPanels();
				ShowPanels();
			}
		}
	}
	
	private void SpawnScorePanel()
	{
		GameObject newScorePanel;
		
		if (isOddNumber)
		{
			newScorePanel = Instantiate(scorePanel, new Vector3(-635, (snakeCount / 2) * -75, 0), Quaternion.identity) as GameObject;
		}
		
		else
		{
			newScorePanel = Instantiate(scorePanel, new Vector3(67, (snakeCount / 2) * -75, 0), Quaternion.identity) as GameObject;
		}
		
		newScorePanel.transform.SetParent(GameObject.Find("Canvas").transform, false);
		newScorePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Score Player " + (snakeCount + 1) + ":";
		newScorePanel.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
		snakePlayer.GetComponent<Snake>().SetScoreTextVariable(newScorePanel.transform.GetChild(1).GetComponent<TMP_Text>());
	}
	
	private void SetColor()
	{
		Color myColor = Color.white;
		
		switch (snakeCount % 6)
		{
			case 5:
				myColor = Color.red;
				break;
			case 4:
				myColor = Color.blue;
				break;
			case 3:
				myColor = Color.green;
				break;
			case 2:
				myColor = Color.magenta;
				break;
			case 1:
				myColor = Color.yellow;
				break;
			default:
				myColor = Color.cyan;
				break;
		}
		
		foreach(Transform child in snakePlayer.transform)
		{
			child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", myColor);
		}
		
		foreach(Transform child in snakeAI.transform)
		{
			child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", myColor);
		}

		apple.GetComponent<Renderer>().material.SetColor("_Color", myColor);
	}
	
}
