using System;
using UnityEngine;

public class Explosion : MonoBehaviour{
    private int timer = 0;
    public int playerTimeDelete;//remove later
    private int timeDelete; // in seconds
    [SerializeField] private String type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        if(type == "Player F"){
            timeDelete = playerTimeDelete; //remove later
        }
    }


    void FixedUpdate(){
        if(timer >= (timeDelete * 50)){
            Destroy(gameObject);
        }
        timer++;
    }
}
