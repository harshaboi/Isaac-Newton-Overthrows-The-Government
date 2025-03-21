using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    private PlayerMovement p;
    [SerializeField] private float speedFactor;
    [SerializeField] private int maxTime;
    private int reloadTime = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
     p = transform.parent.GetComponent<PlayerMovement>();   
    }

    // Update is called once per frame
    void Update(){
        if(p.equipped == 1){
            if(Input.GetButtonDown("Fire1") && reloadTime >= maxTime){
                p.addImpact(p.getForward(), speedFactor);
                reloadTime = 0;
            }
        }
    }

    void FixedUpdate(){
        if(reloadTime < maxTime){
            reloadTime++;
        }
    }
}
