using UnityEngine;
using System.Collections;

public class CloudDes : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col2D)
    {
        if(col2D.gameObject.tag == "CLOUD")
        {
            //Debug.Log("CLOUD");
            Destroy(col2D.gameObject);
        }
    }
}
