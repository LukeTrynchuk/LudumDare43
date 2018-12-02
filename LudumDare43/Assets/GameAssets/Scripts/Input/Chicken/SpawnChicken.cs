﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Core.Services;
using DogHouse.Services;
using Sirenix.OdinInspector;

public class SpawnChicken : MonoBehaviour
{
    int chickenCounter = 0;
    int maxChicken = 10;
    [SerializeField]
    GameObject chicken;

    private Vector3 finalPosition;

    private ServiceReference<IInputService> inputService = new ServiceReference<IInputService>();

    private void OnEnable()
    {
        inputService.AddRegistrationHandle(RegisterInput);
    }

    private void OnDisable()
    {
        if (inputService.isRegistered())
        {

        }
    }

    private void RegisterInput()
    {

    }

    [Button("H")]

    private void SpawnAChicken()
    {
        if(chickenCounter < maxChicken)
        {
            finalPosition = this.transform.position + (this.transform.forward * 3);

            GameObject c = Instantiate(chicken,this.transform.position,Quaternion.identity);

            c.GetComponent<LerpSpawn>().GetPosition(finalPosition);

            chickenCounter++;
        }
    }
}