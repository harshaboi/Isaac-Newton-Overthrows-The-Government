using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float slowFactor = 3;

    [SerializeField] private Camera playerCamera;
    private float horizAxis;
    private float vertAxis;
    private float horizRotate;
    private float vertRotate;

    private float speedRatioX;
    private float speedRatioZ;

    private Vector3 moveDirection;

    private void Start(){
        playerCamera.transform.position = transform.position;
    }

    private void Update()
    {      
        playerCamera.transform.position = transform.position;
        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");
        horizRotate = Input.GetAxis("Mouse X");
        vertRotate = Input.GetAxis("Mouse Y");

        moveDirection = speed * vertAxis * (playerCamera.transform.forward - new Vector3(0, playerCamera.transform.forward.y, 0));
        transform.position += moveDirection * Time.deltaTime;

        
        playerCamera.transform.eulerAngles += speed * new Vector3(vertRotate, horizRotate, 0);
        transform.eulerAngles += speed * new Vector3(0, horizRotate, 0);
    }
}