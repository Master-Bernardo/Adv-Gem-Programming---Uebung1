using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour {

    public GameObject farmPrefab;
    public GameObject towerPrefab;
    public GameObject rockPrefab;
    public float minimalDistance; //minimal distance Between Objects allowed to spawn //not for rocks
    List<GameObject> spawnedObjectsTransform;

    public int farmNumber = 5;
    public int rockNumber = 5;

    private float timeOnReload;  //for braking if we set too many

    // Use this for initialization
    void Start()
    {
        spawnedObjectsTransform = new List<GameObject>();
        Reload(0, 0);
    } 


    public void Reload(int farmNumberRaise, int rockNumberRaise)
    {
        farmNumber += farmNumberRaise;
        rockNumber += rockNumberRaise;

        Delete(spawnedObjectsTransform);
        spawnedObjectsTransform.Clear();

        timeOnReload = Time.realtimeSinceStartup;
        
        SpawnRocks(rockNumber);
        SpawnFarms(farmNumber);
    }

    void SpawnRocks(int _rockNumber)
    {
        for (int i = 0; i < _rockNumber; i++)
        {
            //calculate the position where we want to spawn
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), -1f, Random.Range(-1f, 1f));
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);


            GameObject spawnedObject = Instantiate(rockPrefab, rndPosWithin, transform.rotation);
            spawnedObjectsTransform.Add(spawnedObject);
            

        }
    }
    void SpawnFarms(int _farmNumber)
    {
            //the farms:
            for (int i = 0; i < _farmNumber; i++)
            {
            Restart:
                //calculate the position where we want to spawn
                Vector3 rndPosWithin;
                rndPosWithin = new Vector3(Random.Range(-1f, 1f), -1f, Random.Range(-1f, 1f));
                rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);


                //check for Collision with other spawned Objects with the minimalDistance
                if (spawnedObjectsTransform.Count > 0)
                {
                    for (int y = 0; y < spawnedObjectsTransform.Count; y++)
                    {
                        if (Time.realtimeSinceStartup > timeOnReload + 3f)
                        {
                            Debug.Log("Too many Objects in Spawnbox");
                            return;
                        }
                        if (Vector3.Distance(rndPosWithin, spawnedObjectsTransform[y].transform.position) < minimalDistance)
                        {
                            goto Restart;
                        }

                    }
                    GameObject spawnedObject = Instantiate(farmPrefab, rndPosWithin, transform.rotation);
                    spawnedObjectsTransform.Add(spawnedObject);

                }
                else
                {
                    GameObject spawnedObject = Instantiate(farmPrefab, rndPosWithin, transform.rotation);
                    spawnedObjectsTransform.Add(spawnedObject);
                }

            }

        
    }
    void Delete(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
        }
    }
}
