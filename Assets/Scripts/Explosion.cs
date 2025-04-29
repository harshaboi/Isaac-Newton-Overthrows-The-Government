using System;
using UnityEngine;

public class Explosion : MonoBehaviour{
    [SerializeField] private string type;
    private int timer = 0;
    public int playerTimeDelete; //remove later
    private int timeDelete; //in seconds

    void Awake(){
        switch(type){
            case "Player Flintlock":
                timeDelete = playerTimeDelete;
                break;
        }
    }

    void FixedUpdate(){
        if(timer >= (timeDelete * 50)) Destroy(gameObject);
        timer++;
    }
}