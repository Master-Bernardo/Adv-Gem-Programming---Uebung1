using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlamesLauncher : MonoBehaviour {

    [SerializeField]
    int damage = 5;
    [SerializeField]
    ParticleSystem flames;
    [SerializeField]
    ParticleSystem flameEmbers;

    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            flameEmbers.Emit(1);
            //flames.Emit(1); //Emits one Particle every Frame
            //this lloks better, because we need a faster rate than 1 particle per frame
            flames.Play();  
        }
    }

    private void OnParticleCollision(GameObject other){ 
        
        ParticlePhysicsExtensions.GetCollisionEvents(flames, other, collisionEvents); //now our list will store a list of collision Events Data

        /*for (int i = 0; i < collisionEvents.Count; i++)
        {
            //EmitAtLocation(collisionEvents[i]);
        }*/  //- if i would like to spawn smth on the collision location

        if(other.tag == "Farm")
        {
            Debug.Log("Farm hit");
            other.GetComponent<Farm>().getDamage(damage);
        }
            
        
    }

    /*emits Fire when houses start burning  - not used so far
    //void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        //woodBurning.transform.position = particleCollisionEvent.intersection;
        //woodBurning.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        //woodBurning.transform.rotation = Quaternion.LookRotation(new Vector3(0, 1, 0));
        //woodBurning.Emit(1);

        Instantiate(woodBurning, particleCollisionEvent.intersection, Quaternion.LookRotation(new Vector3(0, 1, 0)));
    }*/

    

    

}
