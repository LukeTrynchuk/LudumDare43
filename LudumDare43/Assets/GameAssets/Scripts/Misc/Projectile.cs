using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Core.Services;
using DogHouse.Services;

public class Projectile : MonoBehaviour
{
    private ServiceReference<ISceneManager> sceneMan = new ServiceReference<ISceneManager>();
    float lifeCounter = 1f;

    private void Update()
    {
        lifeCounter -= Time.deltaTime;
        if(lifeCounter<=0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            if (sceneMan.isRegistered())
            {
                sceneMan.Reference.LoadMainMenuScene();
            }
        }
        else if (other.tag == "Chicken")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
