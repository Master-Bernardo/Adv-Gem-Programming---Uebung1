using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool dead = false;

    public float sideMovementSpeed = 1;
    public float forwardMovementSpeed = 1;
    public float brake_boostSpeed = 1;
    public GameObject dragonsMouth;

    public int health = 10;
	
	// Update is called once per frame
	void Update () {
        if (!dead) { 
            float moveHorizontally = Input.GetAxis("Horizontal");
            float moveVertically = Input.GetAxis("Vertical");  //hier gibts später nur 2 zustände laufen und fliegen
            transform.position += new Vector3(moveHorizontally * (sideMovementSpeed / 10) * Time.deltaTime, 0f , forwardMovementSpeed/10 + moveVertically * (brake_boostSpeed / 10) * Time.deltaTime);
            SetMouthPosition();

        }
    }

    public void getDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Game Over");
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        GameController.Instance.SetState("GAMEOVER");
    }

    void SetMouthPosition()
    {
        //point with the mouse where to shoot

        float disX = 5f;
        float minZ = 2f;
        float maxZ = 12f;

        RaycastHit hit;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag != "Player") {
                Vector3 ziel = new Vector3(
                    // Mathf.Clamp(hit.point.x, transform.position.x - xLimit, transform.position.x + xLimit),
                hit.point.x,
                hit.point.y,
                hit.point.z
            );

                //clamp Ziel
                
                ziel = new Vector3(Mathf.Clamp(ziel.x, dragonsMouth.transform.position.x - disX, dragonsMouth.transform.position.x + disX), ziel.y, Mathf.Clamp(ziel.z, dragonsMouth.transform.position.z + minZ, dragonsMouth.transform.position.z + maxZ));
                Debug.Log(ziel);

                dragonsMouth.transform.rotation = Quaternion.LookRotation((ziel - transform.position));
        }

        }
    }
   

}
