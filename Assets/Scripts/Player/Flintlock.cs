using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    private PlayerMovement p;
    [SerializeField] private float speedFactor;
    private int reloadTime = 500;
    public int maxTime;
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
        if(reloadTime < 1000){
            reloadTime++;
        }
    }
}
