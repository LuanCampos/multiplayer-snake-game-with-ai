using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
	public GameObject dotNormal, dotTimeTravel, dotPowerEngine, dotBatteringRam;
	public Material dotNormalMat, dotTimeTravelMat, dotPowerEngineMat, dotBatteringRamMat;
	
	private AIController aiController = null;
	private PlayerController playerController = null;
	private GameObject snakePair;
	private TMP_Text myScore;
	private bool isAlive = false;
	private bool isAI = true;
	
	private int snakeType = 0;
	private int direction = 0;
	private int velocity = 15;
	private int nextMove = 0;
	private int counter = 0;
	private int score = 0;
	
	private List<Transform> snake = new List<Transform>();
	private List<Vector3> timeTravel = new List<Vector3>();
	private List<int> batteringRam = new List<int>();
	
    void Start()
    {
		GetControllerType();
		GetSnakeDots();
		ResetCounter();
    }

    void FixedUpdate()
    {
		if (isAlive)
		{
			CountFrameAndMove();
		}
    }
	
	public void Grow(int appleType)
	{
		GameObject newDot = null;
		
		switch (appleType)
		{
			case 3:
				newDot = Instantiate(dotTimeTravel, gameObject.transform.GetChild(1).gameObject.transform.position, Quaternion.identity);
				
				if (timeTravel.Count > 0)
				{
					transform.GetChild(timeTravel.Count).gameObject.GetComponent<Renderer>().material = dotNormalMat;
					transform.GetChild(timeTravel.Count).gameObject.GetComponent<Renderer>().material.SetColor("_Color", snake[0].gameObject.GetComponent<Renderer>().material.color); 
				}
				
				timeTravel = new List<Vector3>();
				
				foreach(Transform dot in snake)
				{
				   timeTravel.Add(dot.position);
				}
				break;
			case 2:
				newDot = Instantiate(dotBatteringRam, gameObject.transform.GetChild(1).gameObject.transform.position, Quaternion.identity);
				batteringRam.Add(snake.Count);
				break;
			case 1:
				newDot = Instantiate(dotPowerEngine, gameObject.transform.GetChild(1).gameObject.transform.position, Quaternion.identity);
				SetVelocity(2);
				break;
			default:
				newDot = Instantiate(dotNormal, gameObject.transform.GetChild(1).gameObject.transform.position, Quaternion.identity);
				break;
		}
		
		newDot.GetComponent<Renderer>().material.SetColor("_Color", snake[0].gameObject.GetComponent<Renderer>().material.color);
		newDot.transform.parent = gameObject.transform;
		snake.Add(newDot.transform);
		SetVelocity(-1);
		SetScore(50);
	}
	
	public int SnakeDirection()
	{
		return direction;
	}
	
	public void SetIsAlive(bool info)
	{
		isAlive = info;
	}
	
	public bool GetIsAlive()
	{
		return isAlive;
	}
	
	public void HasCrash()
	{
		if (timeTravel.Count < 1 && batteringRam.Count < 1)
		{
			HasDied();
		}
		
		else
		{
			if (batteringRam.Count > 0 && batteringRam[batteringRam.Count -1] > timeTravel.Count)
			{
				Destroy(transform.GetChild(batteringRam[batteringRam.Count -1]).gameObject);
				snake.RemoveAt(batteringRam[batteringRam.Count -1]);
				batteringRam.RemoveAt(batteringRam.Count -1);
			}
			
			else
			{
				for (int i = snake.Count - timeTravel.Count; i > 0; i--)
				{
					Destroy(transform.GetChild(transform.childCount - 1).gameObject);
					snake.RemoveAt(snake.Count - 1);
				}
				
				for (int i = timeTravel.Count - 1; i >= 0; i--)
				{
					snake[i].position = timeTravel[i];
				}
				
				timeTravel = new List<Vector3>();			
			}
		}
	}
	
	public void SetSnakePair(GameObject otherSnake)
	{
		snakePair = otherSnake;
	}
	
	public void ChangeSnakeType()
	{
		switch (snakeType)
		{
			case 2:
				ChangeSnakeRenderer(dotNormalMat);
				batteringRam = new List<int>();
				snakeType = 0;
				break;
			case 1:
				ChangeSnakeRenderer(dotBatteringRamMat);
				batteringRam.Add(1);
				batteringRam.Add(2);
				batteringRam.Add(3);
				SetVelocity(-4);
				snakeType = 2;
				break;
			default:
				ChangeSnakeRenderer(dotPowerEngineMat);
				SetVelocity(4);
				snakeType = 1;
				break;
		}
	}
	
	public void SetScoreTextVariable(TMP_Text scoreText)
	{
		myScore = scoreText;
	}
	
	private void ChangeSnakeRenderer(Material mat)
	{
		for (int i = 3; i > 0; i--)
		{
			snake[i].gameObject.GetComponent<Renderer>().material = mat;
			snake[i].gameObject.GetComponent<Renderer>().material.SetColor("_Color", snake[0].gameObject.GetComponent<Renderer>().material.color);
		}
	}
	
	private void SetVelocity(int vel)
	{
		velocity += vel;
	}
	
	private void GetControllerType()
	{
		aiController = GetComponent<AIController>();
		
        if (GetComponent<AIController>() == null)
		{
			playerController = GetComponent<PlayerController>();
			isAI = false;
		}
	}
	
	private void GetSnakeDots()
	{
		foreach (Transform child in transform)
		{
			snake.Add(child);
		}
	}
	
	private void CountFrameAndMove()
	{
		if (counter <= 0)
		{
			GetDirection();
			MoveTheSnake();
			SetScore(1);
			ResetCounter();
		}
		
		else
		{
			counter --;
		}
	}
	
	private void GetDirection()
	{		
		if (isAI)
		{
			nextMove = aiController.GetNextMove();
			aiController.HasMoved();
		}
		
		else
		{
			nextMove = playerController.GetNextMove();
			playerController.HasMoved();
		}
		
		if (nextMove == 2)
		{
			direction -= 1;
			
			if (direction < 0)
			{
				direction = 3;
			}
		}
		
		else if (nextMove == 1)
		{
			direction += 1;
			
			if (direction > 3)
			{
				direction = 0;
			}
		}
	}
	
	private void MoveTheSnake()
	{
		for (int i = snake.Count -1; i > 0; i--)
		{
			snake[i].position = snake[i-1].position;
		}
		
		switch (direction)
		{
			case 3: //RIGHT
				snake[0].position = new Vector3(Mathf.RoundToInt(snake[0].position.x + 1), 0, Mathf.RoundToInt(snake[0].position.z));
				break;
			case 2: //DOWN
				snake[0].position = new Vector3(Mathf.RoundToInt(snake[0].position.x), 0, Mathf.RoundToInt(snake[0].position.z - 1));
				break;
			case 1: //LEFT
				snake[0].position = new Vector3(Mathf.RoundToInt(snake[0].position.x - 1), 0, Mathf.RoundToInt(snake[0].position.z));
				break;
			default: //UP
				snake[0].position = new Vector3(Mathf.RoundToInt(snake[0].position.x), 0, Mathf.RoundToInt(snake[0].position.z + 1));
				break;
		}
	}
	
	private void SetScore(int points)
	{
		score += points;
		
		if (!isAI)
		{
			myScore.text = "" + score;
		}
	}
	
	private void ResetCounter()
	{
		counter = 20 - velocity;
	}
	
	private void HasDied()
	{
		if (isAI)
		{
			if (snakePair.GetComponent<Snake>().GetIsAlive())
			{				
				for (int i = transform.childCount - 1; i >= 0; i--)
				{
					if (i < 3)
					{
						transform.GetChild(i).position = new Vector3 (17, 0, -17);
					}
					
					else
					{
						Destroy(transform.GetChild(i).gameObject);
						snake.RemoveAt(i);
					}
				}
				
				SetVelocity(-velocity + 15);
			}
			
			else
			{
				isAlive = false;
			}
		}
		
		else
		{
			isAlive = false;
			GameObject.Find("Game Manager").GetComponent<GameManager>().SnakeDie();
		}
	}
	
}
