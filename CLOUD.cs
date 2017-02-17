using UnityEngine;
using System.Collections;

public class CLOUD : MonoBehaviour {

    public float C_Speed = 0.5f;
    [Range(0,1)]
    public float Sway = 0.2f; 

    void Update () {
        Moveing_Cloud();
    }

    public void Moveing_Cloud()
    {
        float Ran_Sway = Random.Range(0, Sway);
        float x = transform.position.x + Mathf.Clamp(Time.time, -Ran_Sway, Ran_Sway);
        float d = C_Speed * Time.deltaTime * (Mathf.Sin(60 / C_Speed)- Mathf.Cos(60 / C_Speed));
        float y = transform.position.y - d;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
