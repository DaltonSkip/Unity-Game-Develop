using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 1f;

	Rigidbody2D myRigidBody;

	//bool isFacingRight;


	// Use this for initialization
	void Start () {

		myRigidBody = GetComponent<Rigidbody2D>();


	}// end start
	
	// Update is called once per frame
	void Update () {

		if(IsFacingRight())
		{
			myRigidBody.velocity = new Vector2(moveSpeed, 0f);

		}// end if
		else
		{

			myRigidBody.velocity = new Vector2(-moveSpeed, 0f);

		}// end else
		


	}// end update

	bool IsFacingRight()
	{

		return transform.localScale.x > 0;

	}// facing right

	private void OnTriggerExit2D(Collider2D collision)
	{

		transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);


	}// end triggerexit


}// end enemymovement
