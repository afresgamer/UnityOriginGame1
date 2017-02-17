using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public float Timer = 0.5f;
    public Transform tran;
    public GameObject[] Obj;
    public int range = 5;
    public float Plus_Timer_Count = 0.5f;
    public static bool StopSpawn = false;

    Vector3 RandomPos()
    {
        Vector3 S_Pos = Vector3.zero;
        int ran_Tran = Random.Range(-range, range);
        int ObjNum = Random.Range(0, Obj.Length);
        S_Pos = new Vector3(ran_Tran,tran.position.y,
                            Obj[ObjNum].transform.position.z);
        return S_Pos;
    }
    
    void Update () {
        if (!StopSpawn){
            Timer -= Time.deltaTime;
            if (Timer <= 0.0f){
                CreateCloud();
                Timer = Plus_Timer_Count;
            }
        }
	}

    void CreateCloud()
    {
        int ObjNum = Random.Range(0, Obj.Length);
        Instantiate(Obj[ObjNum], RandomPos(), Quaternion.identity);
    }
}
