using System.Data.Common;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float ttm; //Time to move
    [SerializeField] private float yPos;
    [SerializeField] private float xPos;
    [SerializeField] private float zPos;
    private float iniY;
    private float iniX;
    private float iniZ;
    private Vector3 direction;
    private bool goingTo = true;
    void Start()
    {
        iniY = transform.position.y;
        iniX = transform.position.x;
        iniZ = transform.position.z;
        direction = new Vector3(xPos / ttm, yPos / ttm, zPos / ttm) / 50;
    }

    // Update is called once per frame
    void FixedUpdate(){   
        move();
    }

    private void move(){
        if(goingTo){
            if(transform.position.y >= iniY + yPos - 0.01 && transform.position.y <= iniY + yPos + 0.01
             && transform.position.x >= iniX + xPos - 0.01 && transform.position.x <= iniX + xPos + 0.01
              && transform.position.z >= iniZ + zPos - 0.01 &&  transform.position.z <= iniZ + zPos + 0.01){
                direction *= -1;
                goingTo = false;
            }else{
                transform.Translate(direction);
            }
        }else{
            if(transform.position.y >= iniY - 0.01 && transform.position.y <= iniY + 0.01
             && transform.position.x >= iniX - 0.01 && transform.position.x <= iniX + 0.01
              && transform.position.z >= iniZ - 0.01 &&  transform.position.z <= iniZ + 0.01){
                direction *= -1;
                goingTo = true;
            }else{
                transform.Translate(direction);
            }
        }
    }
}
