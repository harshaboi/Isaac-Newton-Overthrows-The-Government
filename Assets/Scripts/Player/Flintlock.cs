using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    [SerializeField] private GameObject bullet, shootPos;
    [SerializeField] private float speedFactor;
    [SerializeField] private int reloadTime; //In seconds
    private PlayerMovement p;
    private GameObject player;
    private ReloadManager r;
    private Vector3 axis;
    //private int timer = 500;
    void Awake(){
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerMovement>();
        r = player.GetComponent<ReloadManager>();
    }

    void Update(){
        // if(p.getEquipped() == 1){
        if(Input.GetButtonDown("Fire1") && r.getFlintTimer() >= (reloadTime * 50)){
            shoot();
        }
        // }
        /*test for rotation
        First gets the player position and then move the point of rotation to the right of the player position by getting the perpendicular vector and moving the point by that vector
        Not sure if I needed two cross products but I used it anyways.
        */
        //transform.RotateAround(player.transform.position - Vector3.Cross(player.transform.forward, Vector3.up).normalized * factor, Vector3.Cross(-Vector3.Cross(transform.forward, Vector3.up), Vector3.up) * 5, 1);
    }
    private void shoot(){
        Instantiate(bullet, shootPos.transform.position, p.transform.rotation);
        p.addImpact(p.getForward(), speedFactor);
        r.resetFlintTimer();
    }
    public int getFlintReloadTime(){
        return reloadTime;
    }
}