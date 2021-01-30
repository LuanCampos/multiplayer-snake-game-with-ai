using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	private int nextMove = 0;
	private bool justMove = true;
	private float forwardDistance, rightDistance, leftDistance;
	private Vector3 snakeHead, apple, moveForward, moveRight, moveLeft;

    void Update()
    {
		if (justMove)
		{
			GetVectors();
			CalculateDistance();
			SetTheMove();
		}
    }
	
	public void HasMoved()
	{
		justMove = true;
	}
	
	public int GetNextMove()
	{
		return nextMove;
	}
	
	private void GetVectors()
	{
		snakeHead = gameObject.transform.GetChild(0).gameObject.transform.position;
		apple = GameObject.Find("Apple").transform.position;
		
		Vector3 goUp = new Vector3(0, 0, 1);
		Vector3 goLeft = new Vector3(-1, 0, 0);
		Vector3 goRight = new Vector3(1, 0, 0);
		Vector3 goDown = new Vector3(0, 0, -1);
		
		if (GetComponent<Snake>() != null)
		{		
			switch (GetComponent<Snake>().SnakeDirection())
			{
				case 3: //RIGHT
					moveForward = snakeHead + goRight;
					moveRight = snakeHead + goDown;
					moveLeft = snakeHead + goUp;
					break;
				case 2: //DOWN
					moveForward = snakeHead + goDown;
					moveRight = snakeHead + goLeft;
					moveLeft = snakeHead + goRight;
					break;
				case 1: //LEFT
					moveForward = snakeHead + goLeft;
					moveRight = snakeHead + goUp;
					moveLeft = snakeHead + goDown;
					break;
				default: //UP
					moveForward = snakeHead + goUp;
					moveRight = snakeHead + goRight;
					moveLeft = snakeHead + goLeft;
					break;
			}
		}
	}
	
	private void CalculateDistance()
	{
		forwardDistance = GetTheDistance(moveForward);
		rightDistance = GetTheDistance(moveRight);
		leftDistance = GetTheDistance(moveLeft);
	}
	
	private float GetTheDistance(Vector3 movePosition)
	{
		if (!Physics.CheckSphere(movePosition, .2f))
		{
			var otherSnakes = GameObject.FindGameObjectsWithTag("SnakeHead");
			var dangerOfCollision = false;
			
			foreach (GameObject head in otherSnakes)
			{
				if (head.transform.position != snakeHead && Vector3.Distance(movePosition, head.transform.position) < 2)
				{
					dangerOfCollision = true;
				}
			}
			
			if (!dangerOfCollision)
			{
				return Vector3.Distance(movePosition, apple);
			}
			
			else
			{
				return 10000;
			}
		}
		
		else
		{
			if (Vector3.Distance(movePosition, apple) > 1)
			{
				return 10000;
			}
			
			else
			{
				return 0;
			}
		}
	}
	
	private void SetTheMove()
	{
		float minorDistance = Mathf.Min(forwardDistance, rightDistance, leftDistance);
		
		if (minorDistance == forwardDistance)
		{
			nextMove = 0; //FORWARD
		}
		
		else if (minorDistance == leftDistance)
		{
			nextMove = 1; //LEFT
		}
		
		else
		{
			nextMove = 2; //RIGHT
		}
		
	}
	
}
