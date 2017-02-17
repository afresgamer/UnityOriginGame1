using UnityEngine;
using System.Collections;

public class Bullet_Des : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col2D)
    {
        if (col2D.gameObject.tag == "BULLET")
        {
            //Debug.Log("BULLET");
            Destroy(col2D.gameObject);
        }
      
    }

}
