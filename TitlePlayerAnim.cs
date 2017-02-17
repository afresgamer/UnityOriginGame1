using UnityEngine;
using System.Collections;

public class TitlePlayerAnim : MonoBehaviour {

    Rigidbody2D rb2D;
    public float _Speed;

	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        Tick_Move(_Speed);
	}

    public void Tick_Move(float Speed)
    {
        float y = Time.deltaTime * Speed;
        Vector2 move = new Vector2(0, y);
        rb2D.velocity = move * Speed;
    }
}
