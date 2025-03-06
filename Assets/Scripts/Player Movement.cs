using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 playerMoveInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private float yRot;

    private CharacterController controller;
    private Rigidbody rb;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    public float dashSpeed;
    private int dashCool;
    private readonly int dashThreshold = 100;
    private readonly float gravity = -9.81f;
    private int jumpCount = 0;
    private float horizAxis;
    private float vertAxis;

    public int dashTimes = 3;

    private void Start(){
        dashCool = dashThreshold;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();   
    }
    private void Update(){
        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");
        playerMoveInput = new Vector3(horizAxis, 0, vertAxis);
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MoveCam();
    }

    private void FixedUpdate(){
        dashCool++;
    }
    private void MovePlayer(){
        Vector3 moveVector = transform.TransformDirection(playerMoveInput) * speed;
        controller.Move(moveVector * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded){
            velocity.y = -1f;
            jumpCount = 0;
            if(Input.GetKeyDown(KeyCode.Space)){
                velocity.y = jumpForce;
                jumpCount++;
            }
        }else{
            if(Input.GetKeyDown(KeyCode.Space) && jumpCount == 1){
                velocity.y = jumpForce;
                jumpCount++;
            }
            velocity.y -= gravity * Time.deltaTime * -2f; 
        }
        if((horizAxis != 0 || vertAxis != 0) && Input.GetAxis("Dash") == 1 && dashCool >= dashThreshold){
            if(horizAxis !=0 && vertAxis != 0){
                dash(new Vector3(horizAxis, 0, vertAxis));
            }else if(horizAxis != 0){
                dash(new Vector3(horizAxis, 0, 0));
            }else{
                dash(new Vector3(0, 0, vertAxis));
            }
            dashCool = 0;
        }else if(Input.GetAxis("Dash") == 1 && dashCool >= dashThreshold){
            dash(transform.forward);
            dashCool = 0;
        }
    }

    private void MoveCam(){
        playerCam.transform.position = transform.position;
        xRot -= playerMouseInput.y * sensitivity;
        yRot -= playerMouseInput.x * sensitivity;
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCam.transform.localRotation = Quaternion.Euler(xRot, -yRot, 0);
    }

    private void dash(Vector3 vector){
        for(int i = 0; i < dashTimes; i++){
            controller.Move(vector * dashSpeed * Time.deltaTime);
        }
    }
}