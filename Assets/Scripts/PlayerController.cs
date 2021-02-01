using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private KeyCode right;
	private KeyCode left;
	private int nextMove = 0;

    void Update()
    {
		if (Input.GetKeyDown(left))
		{
			nextMove = 1;
		}
		
        if (Input.GetKeyDown(right))
		{
			nextMove = 2;
		}
    }
	
	public void HasMoved()
	{
		nextMove = 0;
	}
	
	public int GetNextMove()
	{
		return nextMove;
	}
	
	public void SetMyInput(KeyCode myLeft, KeyCode myRight)
	{
		left = myLeft;
		right = myRight;
	}
}
