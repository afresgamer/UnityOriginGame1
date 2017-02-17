using UnityEngine;
using System.Collections;

public class Enemy_Des : MonoBehaviour {
    

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "CANDY" ||
            col.gameObject.tag == "STONE") Destroy(col.gameObject);      
    }

}
