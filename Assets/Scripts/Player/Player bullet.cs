using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour{
    [SerializeField] private GameObject explosion;
    [SerializeField] private string type;
    private GameObject player;
    private PlayerMovement p;
    private Rigidbody rb;
    private Vector3 direction;
    public float flintSpeed;
    public float musketSpeed;
    private float speed;
    private float initTime;

    void Awake(){
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerMovement>();
        direction = p.getForward();
        rb = GetComponent<Rigidbody>();
        initTime = Time.time;
        switch(type){
            case "Flintlock":
            speed = flintSpeed;
            break;

            case "Musket":
            speed = musketSpeed;
            rb.useGravity = false;
            break;
        }
    }
    void Start(){
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    void Update(){
        if(Time.time >= initTime + 250f) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision){
        if(speed == flintSpeed){
            if(collision.gameObject.tag != "Player"){
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}