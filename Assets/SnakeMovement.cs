using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
	private AIController aiController = null;
	private PlayerController playerController = null;
	private bool isAI = true;
	
	private int direction = 0;
	private int nextMove = 0;
	private int counter = 60;
	
    void Start()
    {
		FindOutController();
    }

    void FixedUpdate()
    {
		if (counter <= 0)
		{
			Debug.Log("Move");
			
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
			}
			
			else if (nextMove == 1)
			{
				direction += 1;
			}
			
			switch (direction)
			{
				case 3:
					print ("Move RIGHT");
					break;
				case 2:
					print ("Move DOWN");
					break;
				case 1:
					print ("Move LEFT");
					break;
				default:
					print ("Move UP");
					break;
			}
			
			counter = 60;
		}
		
		else
		{
			counter --;
		}

    }
	
	private void FindOutController()
	{
		aiController = GetComponent<AIController>();
		
        if (GetComponent<AIController>() == null)
		{
			playerController = GetComponent<PlayerController>();
			isAI = false;
		}
	}

}
