using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerAvatar currentAvatar;
    public bool gameStarted = false;
    private float JumpSpeed
    {
        get
        {
            return 4 * GlobalData.GameData.JumpHeight / GlobalData.GameData.JumpDuration;
        }
    }

    private float JumpGravity
    {
        get
        {
            return 2 * JumpSpeed / GlobalData.GameData.JumpDuration;
        }
    }
    private CharacterController charController
    {
        get
        {
            return GetComponent<CharacterController>();
            //treturn currentAvatar.charController;
        }
    }

    // Use this for initialization
    void Start()
    {
        EventManager.OnStartGameEvent += () => { gameStarted = true; };
    }

    Vector3 moveDirection;

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gameStarted)
            MoveCharacter();
    }

    void MoveCharacter()
    {
        moveDirection.x = GlobalData.GameData.Speed;
        if (charController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Jump
                moveDirection.y = JumpSpeed;
                currentAvatar.Anim.SetTrigger("Jump");
            }
        }
        else
        {

        }

        moveDirection.y -= JumpGravity * Time.deltaTime;
        charController.Move(moveDirection * Time.deltaTime);
        currentAvatar.Anim.SetBool("IsGrounded", charController.isGrounded);
    }
}
