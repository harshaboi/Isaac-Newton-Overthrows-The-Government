using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    private PlayerMovement p;
    private GameObject player;
    [SerializeField] private float speedFactor;
    [SerializeField] private int reloadTime; //In seconds
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shootPos;
    private int timer = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        player = GameObject.Find("player");
        p = transform.parent.GetComponent<PlayerMovement>();   
    }

    // Update is called once per frame
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
