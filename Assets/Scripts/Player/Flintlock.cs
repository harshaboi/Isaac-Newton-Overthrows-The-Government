using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    [SerializeField] private GameObject bullet, shootPos;
    [SerializeField] private float speedFactor;
    [SerializeField] private int reloadTime; //In seconds
    private PlayerMovement p;
    private GameObject player;
    private int timer = 500;
    void Awake(){
        player = GameObject.Find("player");
        p = transform.parent.GetComponent<PlayerMovement>();   
    }

    void Update(){
        if(p.getEquipped() == 1){
            if(Input.GetButtonDown("Fire1") && timer >= (reloadTime * 50)){
                shoot();
            }
        }
    }

    void FixedUpdate(){
        if(timer < (reloadTime * 50)){
            timer++;
        }
    }

    private void shoot(){
        Instantiate(bullet, shootPos.transform.position, p.transform.rotation);
        p.addImpact(p.getForward(), speedFactor);
        timer = 0;
    }
}