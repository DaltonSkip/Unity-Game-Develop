using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {
	
	// config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;
	[SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

	//state 
	bool isAlive = true;

	//cached component refrences
	Rigidbody2D myRigidBody;
	Animator myAnimator;
	CapsuleCollider2D myBodyCollider;
	BoxCollider2D myFeet;
	float gravityScaleAtStart;

	// Use this for initialization
	
	void Start () {

		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider = GetComponent<CapsuleCollider2D>();
		myFeet = GetComponent<BoxCollider2D>();
		gravityScaleAtStart = myRigidBody.gravityScale;


	}// end start
	
	// Update is called once per frame
	void Update () {

		if (!isAlive) { return; }
		Run();
		Jump();
		FlipSprite();
		ClimbLadder();
		Die();

	}// end updat

	private void Run()
	{
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 b/t +1
		Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;
		//print(playerVelocity);

		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool("running", playerHasHorizontalSpeed);

	}// end run

	private void ClimbLadder()
	{

		if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
		{
			myAnimator.SetBool("climbing", false);
			myRigidBody.gravityScale = gravityScaleAtStart;
			return;
		}// end first if

		float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // -1 b/t +1
		Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
		myRigidBody.velocity = climbVelocity;
		myRigidBody.gravityScale = 0f;

		bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
		myAnimator.SetBool("climbing", playerHasVerticalSpeed);


	}//end climbladder


	private void Jump()
	{

		if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			return;
		}// end first if


		if (CrossPlatformInputManager.GetButtonDown("Jump")) 
		{

			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
			myRigidBody.velocity += jumpVelocityToAdd;


		}// end second if


	}// end Jump


	private void Die()
	{

		if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
		{
			isAlive = false;
			myAnimator.SetTrigger("Dying");
			GetComponent<Rigidbody2D>().velocity = deathKick;

			Application.LoadLevel(Application.loadedLevel);

		}// end if

	}// end die


	private void FlipSprite()
	{
		// if player moves horiz 
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
		{
			// reverse the current scaling of x axis
			transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);


		}// end if 
	}// end flipSprite


}// end PLayer
