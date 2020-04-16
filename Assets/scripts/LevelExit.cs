using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

	private bool playerInZone;

	public string levelToLoad;

	void Start()
	{
		playerInZone = false;
	}

	void Update()
	{
		if (playerInZone)
		{
			Application.LoadLevel("Level 2");


		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			playerInZone = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			playerInZone = false;
		}
	}


}// end level exit
