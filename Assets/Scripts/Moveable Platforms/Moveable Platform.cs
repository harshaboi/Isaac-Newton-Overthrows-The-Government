using UnityEngine;

public class MoveablePlatform : MonoBehaviour{
    [SerializeField] private float ttm, yPos, xPos, zPos; //ttm is time to move
    private Vector3 direction;
    private CharacterController controller;
    private float iniY, iniX, iniZ;
    private bool goingTo = true;
    void Awake(){
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
    public Vector3 getMoveVector(){
        return direction;
    }
    public bool getDirection(){
        return goingTo;
    }
}