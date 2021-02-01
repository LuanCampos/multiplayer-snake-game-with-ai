using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	private bool gotCaught = false;
	private int appleType = 0;
	private GameObject mySnakePlayer, mySnakeAI;
	
    void Awake()
    {
        while (Physics.CheckSphere(transform.position, .8f))
		{
			transform.position = new Vector3(Random.Range(-19, 20), 0f, Random.Range(-19, 20));
		}
    }
	
	void OnTriggerEnter(Collider other)
	{		
		if (!gotCaught && (other.gameObject == mySnakePlayer || other.gameObject == mySnakeAI))
		{
			gotCaught = true;
			other.gameObject.transform.parent.gameObject.GetComponent<Snake>().Grow(appleType);
			
			GameObject newApple = Instantiate(gameObject, new Vector3(-20, 0, -20), Quaternion.identity);
			newApple.name = "Apple";
			newApple.GetComponent<Apple>().SetMySnakes(mySnakePlayer, mySnakeAI);
			mySnakeAI.transform.parent.gameObject.GetComponent<AIController>().ChangeMyApple(newApple);
			
			Destroy(this.gameObject);
		}
	}
	
	public void SetMySnakes(GameObject snakePlayer, GameObject snakeAI)
	{
		mySnakePlayer = snakePlayer;
		mySnakeAI = snakeAI;
	}
}
