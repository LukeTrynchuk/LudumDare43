using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Core.Services;
using DogHouse.Services;

public class EndGame : MonoBehaviour
{
    private ServiceReference<ISceneManager> sceneMan = new ServiceReference<ISceneManager>(); 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(sceneMan.isRegistered())
            {
                sceneMan.Reference.LoadMainMenuScene();
            }
        }
    }
}
