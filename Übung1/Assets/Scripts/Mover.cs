using UnityEngine;

public class Mover : MonoBehaviour {

    public float movSpeed;
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += transform.forward*(movSpeed/10);
	}
}
