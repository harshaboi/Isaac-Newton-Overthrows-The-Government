using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    [SerializeField] private float speedFactor;
    [SerializeField] private int reloadTime; //In seconds
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shootPos;
    private PlayerMovement p;
    private GameObject player;
    private int timer = 500;
    void Start(){
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
        p.addImpact(p.getForward(), speedFactor);
        timer = 0;
        Instantiate(bullet, shootPos.transform.position, p.transform.rotation);
    }
}
