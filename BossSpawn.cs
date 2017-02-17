using UnityEngine;
using System.Collections;

public class BossSpawn : MonoBehaviour {

    public static bool BossStart = false;
    public GameObject Boss;
	
	void Update () {
        //Debug.Log(BossStart);
        if (BossStart) Boss_Spawn();
    }

    void Boss_Spawn()
    {
        //Debug.Log(BossStart);
        Instantiate( Boss, Boss.transform.position, Quaternion.identity);
        BossStart = false;
        
    }
}
