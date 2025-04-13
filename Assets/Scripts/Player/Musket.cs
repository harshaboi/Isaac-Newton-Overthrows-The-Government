using UnityEngine;

public class Musket : MonoBehaviour
{
    [SerializeField] private GameObject bullet, shootPos;
    [SerializeField] private int reloadTime; //In seconds
    private ReloadManager r;
    private GameObject player;
    //private int timer = 500;
    void Awake(){
        player = GameObject.Find("Player");
        r = player.GetComponent<ReloadManager>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1") && r.getMusketTimer() >= (reloadTime * 50)){
            shoot();
        }
    }

    private void shoot(){
        Instantiate(bullet, shootPos.transform.position, player.transform.rotation);
        r.resetMusketTimer();
    }
    public int getMusketReloadTime(){
        return reloadTime;
    }
}
