using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	private AIController aiController = null;
	private PlayerController playerController = null;
	private bool isAI = true;
	
	private int direction = 0;
	private int velocity = 15;
	private int nextMove = 0;
	private int counter = 0;
	
	private List<Transform> snake = new List<Transform>();
	
    void Start()
    {
		GetControllerType();
		GetSnakeDots();
		ResetCounter();
    }

    void FixedUpdate()
    {
		CountFrameAndMove();
    }
	
	public void Grow()
	{
		GameObject newDot = Instantiate(gameObject.transform.GetChild(1).gameObject, gameObject.transform.GetChild(1).gameObject.transform.position, Quaternion.identity);
		newDot.transform.parent = gameObject.transform;
		snake.Add(newDot.transform);
	}
	
	public int SnakeDirection()
	{
		return direction;
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
