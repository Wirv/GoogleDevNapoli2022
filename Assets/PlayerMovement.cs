using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 20f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public Collider swordColl;

    private void Start()
    {
        if (GetComponent<CharacterController>()) controller = GetComponent<CharacterController>();
        else controller = gameObject.AddComponent<CharacterController>();

        if (GetComponent<Animator>()) anim = GetComponent<Animator>();
    }

    void Update()
    {
        float playerVerticalInput = Input.GetAxis("Vertical");
        float playerHorizontalInput = Input.GetAxis("Horizontal");

        if (playerVerticalInput != 0 || playerHorizontalInput != 0) anim.SetBool("Movement", true);
        else anim.SetBool("Movement", false);

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;
        
        Vector3 forwardRelativeVerticalInput = playerVerticalInput * forward;
        Vector3 rightRelativeVerticalInput = playerHorizontalInput * right;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = forwardRelativeVerticalInput + rightRelativeVerticalInput;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(Input.GetButtonDown("Fire1") )
        {
            anim.SetBool("Attack", true);

           
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Attack", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Slash")) swordColl.enabled = true;
        else swordColl.enabled = false;
    }
}
