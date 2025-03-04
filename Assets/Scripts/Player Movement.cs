using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 playerMoveInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private float yRot;

    private CharacterController controller;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    private float gravity = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();   
    }
    private void Update()
    {
        playerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MoveCam();
    }

    private void MovePlayer(){
        Vector3 moveVector = transform.TransformDirection(playerMoveInput) * speed;
        controller.Move(moveVector * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded){
            velocity.y = -1f;
            if(Input.GetKeyDown(KeyCode.Space)){
                velocity.y = jumpForce;
            }
        }else{
            velocity.y -= gravity * Time.deltaTime * -2f; 
        }
    }

    private void MoveCam(){
        playerCam.transform.position = transform.position;
        xRot -= playerMouseInput.y * sensitivity;
        yRot -= playerMouseInput.x * sensitivity;
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCam.transform.localRotation = Quaternion.Euler(xRot, -yRot, 0);
    }
}