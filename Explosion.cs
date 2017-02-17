using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public int Timer = 0;
    
    void Update()
    {
        Timer += 1 ;
        if(Timer >= 80) { DeathExplosion(); Timer = 0;  }
    }


    public void DeathExplosion()
    {
        Destroy(gameObject);
    }
}
