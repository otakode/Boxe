using UnityEngine;
using System.Collections;

public class life : MonoBehaviour 
{
	public int max_life = 50;
	private	int current_life;
	// Use this for initialization
	void Start ()
	{
		current_life = max_life;
	}

	public void take_dommage()
	{
		current_life -= 1;
		Debug.Log("aouch");
	}

	// Update is called once per frame
	void Update () 
	{
	//	Debug.Log(current_life);

	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		if (other.tag == "enemy")
			take_dommage();
	}
}
