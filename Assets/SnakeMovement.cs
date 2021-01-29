using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
	private AIController aiController = null;
	private PlayerController playerController = null;
	private bool isAI = true;
	
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
				Debug.Log(nextMove);
				aiController.HasMoved();
			}
			else
			{
				nextMove = playerController.GetNextMove();
				Debug.Log(nextMove);
				playerController.HasMoved();
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
