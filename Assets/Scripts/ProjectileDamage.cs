using UnityEngine;

public class ProjectileDamage : MonoBehaviour {

    public int damage = 10;
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().getDamage(damage);
        }
    }
}
