    using UnityEngine;
using UnityEngine.TextCore.Text;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject player;
    private PlayerMovement p;
    private CharacterController controller;
    private Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
        direction = p.getForward();
    }

    // Update is called once per frame
    void Update(){
        controller.Move(direction * speed);
    }
}
