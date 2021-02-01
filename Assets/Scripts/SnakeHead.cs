using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
	private int countFrames;
	
	void FixedUpdate()
	{
		if (countFrames < 150)
		{
			countFrames ++;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (countFrames >= 150)
		{
			if (other.gameObject.tag != "Apple")
			{
				this.gameObject.transform.parent.gameObject.GetComponent<Snake>().HasCrash();
				countFrames = 0;
			}
		}
	}
}
