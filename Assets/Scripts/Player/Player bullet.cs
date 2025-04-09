using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour{
    [SerializeField] private GameObject explosion;
    [SerializeField] public float flintSpeed;
    [SerializeField] public float musketSpeed;
    private float speed;
    [SerializeField] private string type;
    private GameObject player;
    private PlayerMovement p;
    private Rigidbody rb;
    private Vector3 direction;
    void Awake(){
        switch(type){
            case "Flintlock":
            speed = flintSpeed;
            break;

            case "Musket":
            speed = musketSpeed;
            break;
        }
    }
    void Start(){
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerMovement>();
        direction = p.getForward();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    // void Update(){
    //     rb.AddForce(direction * speed, ForceMode.Impulse);
    // }
    void OnCollisionEnter(Collision collision){
        if(speed == flintSpeed){
            if(collision.gameObject.tag != "Player"){
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}