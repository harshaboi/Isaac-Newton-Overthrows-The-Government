using UnityEngine;

public class MoveablePlatform : MonoBehaviour{
    [SerializeField] private float ttm; //Time to move
    [SerializeField] private float yPos;
    [SerializeField] private float xPos;
    [SerializeField] private float zPos;
    private float iniY;
    private float iniX;
    private float iniZ;
    private Vector3 direction;
    private bool goingTo = true;
    private CharacterController controller;
    void Start(){
        controller = GetComponent<CharacterController>();
        iniY = transform.position.y;
        iniX = transform.position.x;
        iniZ = transform.position.z;
        direction = new Vector3(xPos / ttm, yPos / ttm, zPos / ttm) / 50;
    }

    void FixedUpdate(){   
        move();
    }

    private void move(){
        if(goingTo){
            if(transform.position.y >= iniY + yPos - 0.05 && transform.position.y <= iniY + yPos + 0.05
             && transform.position.x >= iniX + xPos - 0.05 && transform.position.x <= iniX + xPos + 0.05
              && transform.position.z >= iniZ + zPos - 0.05 &&  transform.position.z <= iniZ + zPos + 0.05){
                direction *= -1;
                goingTo = false;
            }else{
                controller.Move(direction);
            }
        }else{
            if(transform.position.y >= iniY - 0.05 && transform.position.y <= iniY + 0.05
             && transform.position.x >= iniX - 0.05 && transform.position.x <= iniX + 0.05
              && transform.position.z >= iniZ - 0.05 &&  transform.position.z <= iniZ + 0.05){
                direction *= -1;
                goingTo = true;
            }else{
                controller.Move(direction);
            }
        }
    }
    public bool getDirection(){
        return goingTo;
    }
    public Vector3 getMoveVector(){
        return direction;
    }
}
