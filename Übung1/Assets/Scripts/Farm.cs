using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    [SerializeField]
    int health = 10;
    [SerializeField]
    int pointsForDestroying = 1;
    [SerializeField]
    float transitionSpeed = 0.5f;
    [SerializeField]
    public GameObject fireEffect;


    bool dead = false;
    Renderer rend;
    float transitionProgress = 0f;
    
    private void Start()
    {
        //Get the renderer form the farmModel for the Shader change
        rend = gameObject.transform.GetChild(0).GetComponent<Renderer>();
    }

    private void Update()
    {
        if (dead)
        {
            //animate shader here
            float burnFactor = Mathf.Lerp(1.1f, 0f, transitionProgress);
            transitionProgress += transitionSpeed * Time.deltaTime;
            rend.material.SetFloat("_BurnFactor", burnFactor);
        }
    }

    public void getDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            dead = true;
            //with some Fire Effects
            fireEffect.SetActive(true);

            GameController.Instance.AddScore(pointsForDestroying);
        }
    }
}
