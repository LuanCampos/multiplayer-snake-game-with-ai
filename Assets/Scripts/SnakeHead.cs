using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag != "Apple")
        {
			Destroy(gameObject.transform.parent.gameObject.GetComponent<Snake>());
		}
	}
}
