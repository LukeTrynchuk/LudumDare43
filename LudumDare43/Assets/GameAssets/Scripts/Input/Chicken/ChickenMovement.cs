using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Services;
using DogHouse.Core.Services;

public class ChickenMovement : MonoBehaviour
{
    private ServiceReference<IInputService> inputService = new ServiceReference<IInputService>();
    //private Rigidbody myBody;
    public float moveSpeed;
    private CharacterController myBody;
    private Vector3 moveDir;
    public float gravity;
    public float jumpForce;


    private void OnEnable()
    {
        inputService.AddRegistrationHandle(RegisterIntput);
        myBody = GetComponent<CharacterController>();

        
    }
    private void Start()
    {
        SpawnChicken(GameObject.FindGameObjectWithTag("Spawn"));
    }
    private void OnDisable()
    {
        if(inputService.isRegistered())
        {
            inputService.Reference.OnMovementVectorCalculated -= MoveChicken;
            inputService.Reference.OnJumpButtonPressed -= Jump;
        }
        
    }
    private void RegisterIntput()
    {
        inputService.Reference.OnMovementVectorCalculated -= MoveChicken;
        inputService.Reference.OnMovementVectorCalculated += MoveChicken;
        inputService.Reference.OnJumpButtonPressed -= Jump;
        inputService.Reference.OnJumpButtonPressed += Jump;   
    }
    private void MoveChicken(Vector2 move)
    {
        moveDir = new Vector3(move.x, 0f, move.y);

        moveDir.y = moveDir.y + (Physics.gravity.y * Time.deltaTime * gravity);

        myBody.Move(moveDir * Time.deltaTime * moveSpeed);
    }
    private void Jump()
    {
        moveDir.y = jumpForce;
        myBody.Move(moveDir);
    }

    private void Update()
    {
        moveDir = new Vector3(0f, moveDir.y, 0f);
        moveDir.y = moveDir.y + (Physics.gravity.y * Time.deltaTime * gravity);

        myBody.Move(moveDir * Time.deltaTime * moveSpeed);
    }

    private void SpawnChicken(GameObject spawnPoint)
    {
        this.transform.position = spawnPoint.transform.position;
    }
}
