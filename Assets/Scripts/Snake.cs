using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	public GameObject dotNormal, dotTimeTravel, dotPowerEngine, dotBatteringRam;
	
	private AIController aiController = null;
	private PlayerController playerController = null;
	private GameObject snakePair;
	private bool isAlive = false;
	private bool isAI = true;
	
	private int direction = 0;
	private int velocity = 15;
	private int nextMove = 0;
	private int counter = 0;
	
	private List<Transform> snake = new List<Transform>();
	private List<Transform> timeTravel = new List<Transform>();
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
				timeTravel = snake;
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
	
	public void HasDie()
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
		}
	}
	
	public void SetSnakePair(GameObject otherSnake)
	{
		snakePair = otherSnake;
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
	
	private void ResetCounter()
	{
		counter = 20 - velocity;
	}

}
