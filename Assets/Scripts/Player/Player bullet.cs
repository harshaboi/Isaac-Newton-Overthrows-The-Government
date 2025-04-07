using UnityEngine;

public class PlayerBullet : MonoBehaviour{
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosion;
    private GameObject player;
    private PlayerMovement p;
    private Rigidbody rb;
    private Vector3 direction;
    void Start(){
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerMovement>();
        direction = p.getForward();
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }
    void OnCollisionEnter(Collision collision){
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag != "Player"){
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
