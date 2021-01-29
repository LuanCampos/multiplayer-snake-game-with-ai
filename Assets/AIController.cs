using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	//private KeyCode right;
	//private KeyCode left;
	private int nextMove = 0;
	
    void Start()
    {
        FindOutMyInput();
    }

    void Update()
    {
		
    }
	
	public void HasMoved()
	{
		nextMove = 0;
	}
	
	public int GetNextMove()
	{
		return nextMove;
	}
	
	private void FindOutMyInput()
	{
		//right = KeyCode.D;
		//left = KeyCode.A;
	}
	
}
