using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	private bool gotCaught = false;
	
    void Awake()
    {
        while (Physics.CheckSphere(transform.position, .8f))
		{
			transform.position = new Vector3(Random.Range(-19, 20), 0f, Random.Range(-19, 20));
		}
    }
	
	void OnCollisionEnter(Collision other)
	{
		if (!gotCaught)
		{
			gotCaught = true;
			other.gameObject.transform.parent.gameObject.GetComponent<Snake>().Grow();
			GameObject newApple = Instantiate(gameObject, new Vector3(-20, 0, -20), Quaternion.identity);
			newApple.name = "Apple";
			Destroy(this.gameObject);
		}
	}
}
