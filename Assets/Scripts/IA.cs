using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour 
{
	private float	timer;
	public int		max_life = 100;
	private int		current_life;
	public int		max_endurence = 100;
	public int		current_endurence;
	// Use this for initialization
	void Start () 
	{
		current_life = max_life;
		current_endurence = max_endurence;
	}

	public void	hit_body()
	{
		current_endurence -= 1;
		current_life -= 1;

	}
	public void	hit_head()
	{
		current_life -= 2;
	}

	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			this.GetComponent<Animator>().SetFloat("ia_random", Random.Range(0.0f, 6.0f));
			this.GetComponent<Animator>().speed = (current_endurence * 0.01f);
			timer = 0.5f;
		}
	}
}
