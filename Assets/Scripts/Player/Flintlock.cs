using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    private PlayerMovement p;
    private GameObject player;
    [SerializeField] private float speedFactor;
    [SerializeField] private int maxTime;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shootPos;
    private int reloadTime = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        player = GameObject.Find("player");
        p = transform.parent.GetComponent<PlayerMovement>();   
    }

    // Update is called once per frame
    void Update(){
        if(p.equipped == 1){
            if(Input.GetButtonDown("Fire1") && reloadTime >= maxTime){
                shoot();
            }
        }
    }

    void FixedUpdate(){
        if(reloadTime < maxTime){
            reloadTime++;
        }
    }

    private void shoot(){
        p.addImpact(p.getForward(), speedFactor);
        reloadTime = 0;
        Instantiate(bullet, shootPos.transform.position, p.transform.rotation);
        Debug.Log(p.transform.rotation);
    }
}
