using UnityEngine;

public class Musket : MonoBehaviour
{
    [SerializeField] private GameObject bullet, shootPos;
    [SerializeField] private int reloadTime; //In seconds
    public float factor;
    private PlayerMovement p;
    private GameObject player;
    //private int timer = 500;
    void Awake(){
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1") && p.getMusketTimer() >= (reloadTime * 50)){
            shoot();
        }
    }

    private void shoot(){
        Instantiate(bullet, shootPos.transform.position, p.transform.rotation);
        p.resetMusketTimer();
    }
    public int getMusketReloadTime(){
        return reloadTime;
    }
}
