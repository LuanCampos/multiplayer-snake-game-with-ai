﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
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
			Debug.Log(nextMove);
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
			case 3:
				snake[0].position = new Vector3(snake[0].position.x + 1, snake[0].position.y, snake[0].position.z);
				break;
			case 2:
				snake[0].position = new Vector3(snake[0].position.x, snake[0].position.y, snake[0].position.z - 1);
				break;
			case 1:
				snake[0].position = new Vector3(snake[0].position.x - 1, snake[0].position.y, snake[0].position.z);
				break;
			default:
				snake[0].position = new Vector3(snake[0].position.x, snake[0].position.y, snake[0].position.z + 1);
				break;
		}
	}
	
	private void ResetCounter()
	{
		counter = 20 - velocity;
	}

}
