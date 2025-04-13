using System;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class ReloadManager : MonoBehaviour{
    private Flintlock f;
    private Musket m;
    private int flintTimer, musketTimer;
    void Awake(){
        f = transform.GetChild(0).gameObject.GetComponent<Flintlock>();
        m = transform.GetChild(1).gameObject.GetComponent<Musket>();
        flintTimer = musketTimer = 10000;
    }

    void FixedUpdate(){
        if(flintTimer < (f.getFlintReloadTime() * 50)){
            flintTimer++;
        }
        if(musketTimer < (m.getMusketReloadTime() * 50)){
            musketTimer++;
        }
    }
    public int getFlintTimer(){
        return flintTimer;
    }
    public void resetFlintTimer(){
        flintTimer = 0;
    }
    public int getMusketTimer(){
        return musketTimer;
    }
    public void resetMusketTimer(){
        musketTimer = 0;
    }
}
