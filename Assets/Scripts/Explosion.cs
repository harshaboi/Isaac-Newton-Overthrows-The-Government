using System;
using UnityEngine;

public class Explosion : MonoBehaviour{
    private int timer = 0;
    public int playerTimeDelete; //remove later
    private int timeDelete; //in seconds
    [SerializeField] private string type;

    void Start(){
        switch(type){
            case "Player Flintlock":
                timeDelete = playerTimeDelete;
                break;
        }
    }

    void FixedUpdate(){
        if(timer >= (timeDelete * 50)){
            Destroy(gameObject);
        }
        timer++;
    }
}
