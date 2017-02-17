using UnityEngine;
using System.Collections;

public class Spin_Obj : MonoBehaviour { 
    Rigidbody2D rb2D;
    public float Spin = 1;

	void Start () { 
        rb2D = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        rb2D.AddTorque(Spin);
    }

}
