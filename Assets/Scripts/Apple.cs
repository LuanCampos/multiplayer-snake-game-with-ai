using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	public Material dotNormal, dotTimeTravel, dotPowerEngine, dotBatteringRam;
	
	private bool gotCaught = false;
	private int appleType;
	private GameObject mySnakePlayer, mySnakeAI;
	
    void Awake()
    {
		DontSpawnInsideSnake();
		appleType = SetRandomAppleType();
		AdjustMaterial();
    }
	
	void Start()
	{
		AdjustColor();
	}
	
	public void SetMySnakes(GameObject snakePlayer, GameObject snakeAI)
	{
		mySnakePlayer = snakePlayer;
		mySnakeAI = snakeAI;
	}
	
	private void OnTriggerEnter(Collider other)
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
	
	private void DontSpawnInsideSnake()
	{
		while (Physics.CheckSphere(transform.position, .8f))
		{
			transform.position = new Vector3(Random.Range(-19, 20), 0f, Random.Range(-19, 20));
		}
	}
	
	private int SetRandomAppleType()
	{
		if (Random.Range(0, 2) == 0)
		{
			return Random.Range(0, 2);
		}
		
		else
		{
			return Random.Range(0, 4);
		}
	}
	
	private void AdjustMaterial()
	{
		switch (appleType)
		{
			case 3:
				GetComponent<MeshRenderer>().material = dotTimeTravel;
				break;
			case 2:
				GetComponent<MeshRenderer>().material = dotBatteringRam;
				break;
			case 1:
				GetComponent<MeshRenderer>().material = dotPowerEngine;
				break;
			default:
				GetComponent<MeshRenderer>().material = dotNormal;
				break;
		}
	}
	
	private void AdjustColor()
	{
		if (mySnakeAI != null)
		{
			GetComponent<Renderer>().material.SetColor("_Color", mySnakeAI.GetComponent<Renderer>().material.color);
		}
	}
}
