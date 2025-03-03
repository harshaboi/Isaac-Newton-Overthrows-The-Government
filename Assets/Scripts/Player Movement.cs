using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Camera playerCamera;
    private float horizAxis;
    private float vertAxis;
    private float horizRotate;
    private float vertRotate;
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

        //transform.eulerAngles += speed * new Vector3(vertRotate, 0, horizRotate);
        playerCamera.transform.eulerAngles += speed * new Vector3(vertRotate, horizRotate, 0);
    }
    private void FixedUpdate()
    {
        
    }
}