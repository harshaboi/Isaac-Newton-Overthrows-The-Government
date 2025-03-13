using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 playerMoveInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private float yRot;
    private MoveablePlatform m;
    private CharacterController controller;
    private Rigidbody rb;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    public float slowFactor;
    private float dashSpeed = 20;
    private int dashCool;
    private bool isDashing;
    private float dashTime = 0.25f;
    private readonly int dashThreshold = 50;
    private readonly float gravity = -9.81f;
    private int jumpCount = 0;
    private float horizAxis;
    private float vertAxis;
    private bool onPlatform = false;
    private float wallHangSpeed = 1;
    private bool hanging = false;
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
        if(onPlatform){
            controller.Move(m.getMoveVector());
        }
    }
    private void MovePlayer(){
        Vector3 moveVector = transform.TransformDirection(playerMoveInput) * speed;
        controller.Move(moveVector * Time.deltaTime * wallHangSpeed);
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded){
            if(!hanging){
                velocity.y = -1f;
            }else{
                velocity.y = 0;
            }
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
            if(!hanging){
                velocity.y -= gravity * Time.deltaTime * -2f; 
            }else{
                velocity.y = 0;
            }
        }
        if((horizAxis != 0 || vertAxis != 0) && Input.GetAxis("Dash") == 1 && dashCool >= dashThreshold){
            StartCoroutine(dash(new Vector3(horizAxis, 0, vertAxis)));
            dashCool = 0;
        }else if(Input.GetAxis("Dash") == 1 && dashCool >= dashThreshold){
            StartCoroutine(dash(transform.forward));
            dashCool = 0;
        }
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "moveable platform"){
            onPlatform = true;
            m = other.gameObject.GetComponent<MoveablePlatform>();
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "moveable platform"){
            onPlatform = false;
        }
    }
    void OnCollisionStay(Collision hit){
        if(hit.gameObject.tag == "wall"){
            if(Input.GetKey(KeyCode.Space)){
                wallHangSpeed = 0.5f;
                hanging = true;
            }
        }
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "wall"){
            wallHangSpeed = 1;
            hanging = false;
        }
    }
    private void MoveCam(){
        playerCam.transform.position = transform.position;
        xRot -= playerMouseInput.y * sensitivity;
        yRot -= playerMouseInput.x * sensitivity;
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCam.transform.localRotation = Quaternion.Euler(xRot, -yRot, 0);
    }

    private IEnumerator dash(Vector3 vector){
        float startTime = Time.time; // need to remember this to know how long to dash
        while(Time.time < startTime + dashTime)
        {
            controller.Move(vector * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
    }
}