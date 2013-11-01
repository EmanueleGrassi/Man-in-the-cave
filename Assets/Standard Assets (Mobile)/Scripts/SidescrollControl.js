//////////////////////////////////////////////////////////////
// SidescrollControl.js
//
// SidescrollControl creates a 2D control scheme where the left
// pad is used to move the character, and the right pad is used
// to make the character jump.
//////////////////////////////////////////////////////////////

#pragma strict

@script RequireComponent( CharacterController )

// This script must be attached to a GameObject that has a CharacterController
var moveTouchPad : Joystick;
var jumpTouchPad : Joystick;

var forwardSpeed : float = 4;
var backwardSpeed : float = 4;
var jumpSpeed : float = 22;
var inAirMultiplier : float = 0.25;					// Limiter for ground speed while jumping

private var thisTransform : Transform;
private var character : CharacterController;
private var velocity : Vector3;						// Used for continuing momentum while in air
private var canJump = true;


var  footstep:AudioClip;
var  collected1 : AudioClip;
var  collected2 : AudioClip;
var  collected3 : AudioClip;
var random : int;
private var nextPlayAudio:float;

function Start()
{
	// Cache component lookup at startup instead of doing this every frame		
	thisTransform = GetComponent( Transform );
	character = GetComponent( CharacterController );	

	// Move the character to the correct start position in the level, if one exists
	var spawn = GameObject.Find( "PlayerSpawn" );
	if ( spawn )
		thisTransform.position = spawn.transform.position;
}

function OnEndGame()
{
	// Disable joystick when the game ends	
	moveTouchPad.Disable();	
	jumpTouchPad.Disable();	

	// Don't allow any more control changes when the game ends
	this.enabled = false;
}

function Update()
{
	var movement = Vector3.zero;
	thisTransform.transform.position.z = 7.5;
	// Apply movement from move joystick
	if ( moveTouchPad.position.x > 0 )
		movement = Vector3.right * forwardSpeed * moveTouchPad.position.x;
	else
		movement = Vector3.right * backwardSpeed * moveTouchPad.position.x;
	
	// Check for jump
	if ( character.isGrounded )//se sto a terra faccio questo
	{		
		var jump = false;
		var touchPad = jumpTouchPad;
			
		if ( !touchPad.IsFingerDown() )
			canJump = true;
		
	 	if ( canJump && touchPad.IsFingerDown() )
	 	{
			jump = true;
			canJump = false;
	 	}	
		
		if ( jump )
		{
			// Apply the current movement to launch velocity		
			velocity = character.velocity;
			velocity.y = jumpSpeed;	
		}
		else
		{
			if (Time.time>nextPlayAudio && moveTouchPad.position.x != 0) 
			{
				audio.PlayOneShot(footstep);
				nextPlayAudio=Time.time+0.3f;
			}
			else if(moveTouchPad.position.x == 0)
			{
				audio.Stop();
			}
		}
	}
	else// se sono in volo faccio questo
	{			
		// Apply gravity to our velocity to diminish it over time
		velocity.y += Physics.gravity.y * Time.deltaTime;
				
		// Adjust additional movement while in-air
		movement.x *= inAirMultiplier;
//		movement.z *= inAirMultiplier;
	}
		
	movement += velocity;	
	movement += Physics.gravity;
	movement *= Time.deltaTime;
	//movement.z=0;
	// Actually move the character	
	character.Move( movement );
	//freeez di z
	//transform.position.z=7.5;
	if ( character.isGrounded )
		// Remove any persistent velocity after landing	
		velocity = Vector3.zero;
}
function OnCollisionEnter(collision:Collision) 
{
 if(collision.gameObject.tag=="coinGold" || collision.gameObject.tag=="coinSilver")
 	{
 	/*if(collision.gameObject.tag=="coinGold") 	
 		CameraScript.coins =5f;
 	else
 		CameraScript.coins=2f;*/
 	
 		random=Random.Range(0,3);
 		switch(random)
 			{
 				case 0:
 					AudioSource.PlayClipAtPoint(collected1,transform.position);
 					break;
 				case 1:
 					AudioSource.PlayClipAtPoint(collected2,transform.position);
 					break;
 				case 2:
 					AudioSource.PlayClipAtPoint(collected3,transform.position);
 					break;
 			}
 	}
}