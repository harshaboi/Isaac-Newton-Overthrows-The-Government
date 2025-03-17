using System.Collections;
using UnityEngine;

public class Flintlock : MonoBehaviour{
    private Vector3 shootVector;
    private PlayerMovement p;
    [SerializeField] private float speedFactor;
    private bool reloaded = true;
    private int reloadTime = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
     p = transform.parent.GetComponent<PlayerMovement>();   
    }

    // Update is called once per frame
    void Update(){
        shootVector = p.getForward();
        if(p.equipped == 1){
            if(Input.GetButtonDown("Fire1") && reloaded){
                p.getController().Move(shootVector * -1);
            }
        }
    }

    void FixedUpdate(){
        if(!reloaded){
            reloadTime++;
            if(reloadTime >= 500){
                reloaded = true;
            }
        }
    }

    private IEnumerator shot(){
        float startTime = Time.time;
        while(Time.time < startTime)
        yield return null;
    }
}
