using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {


	public float playerWalkingSpeed=5f;
	public float playerRunningSpeed = 15f;
	public float jumpingStrength=2f;

	float forwardMovement;
	float sidewaysMovement;

	float verticalVelocity; //for jumping

	float verticalRotation=0;
	public float verticalRotationLimit=0.010f;

    public FlashScreen flash;

    public AudioClip pickupSound;

    AudioSource source;

	CharacterController cc;

	void Awake(){

		cc = GetComponent<CharacterController> ();
        source = GetComponent<AudioSource>();
		Cursor.visible =false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update(){
	
		//Look Around

		//horizontal rotation
		float horizontalRotation=Input.GetAxis("Mouse X");
		transform.Rotate (0, horizontalRotation, 0);

		//vertical rotation
		verticalRotation -= Input.GetAxis("Mouse Y");
		verticalRotation = Mathf.Clamp (verticalRotation, -verticalRotationLimit, verticalRotationLimit);
		Camera.main.transform.localRotation=Quaternion.Euler(verticalRotation,0,0);

		//Movement
		if (cc.isGrounded) {
			forwardMovement = Input.GetAxis ("Vertical") * playerWalkingSpeed;
			sidewaysMovement = Input.GetAxis ("Horizontal") * playerWalkingSpeed;
			if (Input.GetKey (KeyCode.LeftShift)) {

				forwardMovement = Input.GetAxis ("Vertical") * playerRunningSpeed;
				sidewaysMovement = Input.GetAxis ("Horizontal") * playerRunningSpeed;
			}

			//check dynamic crosshair
			if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {

				if (Input.GetKey (KeyCode.LeftShift)) {

					DynamicCrossHair.spread = DynamicCrossHair.RUN_SPREAD;
				} else
					DynamicCrossHair.spread = DynamicCrossHair.WALK_SPREAD;
			}

		} else {

			DynamicCrossHair.spread = DynamicCrossHair.JUMP_SPREAD;
		}

		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		if (Input.GetButton ("Jump")&&cc.isGrounded) {

			verticalVelocity = jumpingStrength;
		}

		Vector3 playerMovement = new Vector3 (sidewaysMovement, verticalVelocity, forwardMovement);
		cc.Move (transform.rotation*playerMovement*Time.deltaTime);

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HpBonus"))
        {
            GetComponent<PlayerHealth>().AddHealth(20);
        }
        else if (other.CompareTag("ArmorBonus"))
        {
            GetComponent<PlayerHealth>().AddArmor(50);
        }
        else if (other.CompareTag("AmmoBonus"))
        {
            transform.Find("Weapons").Find("PistolHand").GetComponent<Pistol>().AddAmmo(15);
        }

        if (other.CompareTag("HpBonus") || other.CompareTag("ArmorBonus") || other.CompareTag("AmmoBonus"))
        {
            flash.PickedUpBonus();
            source.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
        } 
    }
}
