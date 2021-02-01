using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Apple")
        {
			gameObject.transform.parent.gameObject.GetComponent<Snake>().HasDie();
		}
	}
}
