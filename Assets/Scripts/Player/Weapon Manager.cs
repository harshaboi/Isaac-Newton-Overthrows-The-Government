using UnityEngine;

public class WeaponManager : MonoBehaviour{
    private int equipped = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            equipped = 1;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(2).gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            equipped = 2;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            //transform.GetChild(2).gameObject.SetActive(false);
        }
        // if(Input.GetKeyDown(KeyCode.Alpha3)){
        //     equipped = 3;
        //transform.GetChild(2).gameObject.SetActive(true);
        //     transform.GetChild(0).gameObject.SetActive(false);
        //     transform.GetChild(1).gameObject.SetActive(false);
        // }
    }
    public int getEquipped(){
        return equipped;
    }
}
