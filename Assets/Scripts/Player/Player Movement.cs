using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 playerMoveInput;
    private Vector3 impact = Vector3.zero;
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
    [SerializeField] private float mass;
    public float slowFactor;
    private float dashSpeed = 20;
    private int dashCool;
    private bool isDashing;
    private float dashTime = 0.25f;
    private readonly int dashThreshold = 150;
    private readonly float gravity = -9.81f;
    private int jumpCount = 0;
    private int hangJumpCount = 0;
    private float horizAxis;
    private float vertAxis;
    private bool onPlatform = false;
    private float wallHangSpeed = 1;
    private bool hanging = false;
    private float unhangTimer = 0; //Gives a little bit of time after unhanging from a wall where u move a little slower
    public int equipped = 1;
    private void Start(){
        //playerCam = GameObject.Find("Main Camera").transform;
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
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            equipped = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            equipped = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            equipped = 3;
        }
        if(impact.magnitude > 0.2){
            controller.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    private void FixedUpdate(){
        if(dashCool < dashThreshold){
            dashCool++;
        }
        if(onPlatform){
            controller.Move(m.getMoveVector());
        }
        if(unhangTimer != 0){
            unhangTimer += 1;
            if(unhangTimer >= 25){
                unhangTimer = 0;
                wallHangSpeed = 1;
            }
        }
    }
    public void addImpact(Vector3 dir, float force){
        dir.Normalize();
        impact += -1 * dir.normalized * force / mass;
    }
    private void MovePlayer(){
        Vector3 moveVector = transform.TransformDirection(playerMoveInput) * speed;
        controller.Move(moveVector * Time.deltaTime * wallHangSpeed);
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded){
            hangJumpCount = 0;
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
            if(Input.GetKeyDown(KeyCode.Space) && jumpCount == 1 && hangJumpCount <= 3){
                velocity.y = jumpForce;
                jumpCount++;
            }
            if(hanging){
                jumpCount = 1;
                velocity.y = 0;
            }else{
                velocity.y -= gravity * Time.deltaTime * -2f; 
            }
        }
        if((horizAxis != 0 || vertAxis != 0) && Input.GetAxis("Dash") == 1 && dashCool >= dashThreshold){
            StartCoroutine(dash(transform.TransformDirection(new Vector3(horizAxis, 0, vertAxis))));
            dashCool = 0;
        }else if(Input.GetAxis("Dash") == 1 && dashCool >= dashThreshold){
            StartCoroutine(dash(transform.forward));
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
            hanging = false;
            hangJumpCount++;
        }
    }

    private IEnumerator dash(Vector3 vector){
        float startTime = Time.time; // need to remember this to know how long to dash
        while(Time.time < startTime + dashTime)
        {
            controller.Move(vector * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
    }
    public Vector3 getForward(){
        return playerCam.transform.forward;
    }
    public CharacterController getController(){
        return controller;
    }
    public float getXRot(){
        return xRot;
    }
}