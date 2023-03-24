using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private float moveSpeed = 7f;

    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private bool isWalking;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalizedRight();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // Tentar movimentar nas direções pré-definidas
            Vector3[] directionsToCheck = new Vector3[] {
            new Vector3(moveDirection.x, 0f, moveDirection.z),
            new Vector3(moveDirection.z, 0f, moveDirection.x),
            new Vector3(-moveDirection.x, 0f, -moveDirection.z),
            new Vector3(-moveDirection.z, 0f, -moveDirection.x)
        };

            for (int i = 0; i < directionsToCheck.Length; i++)
            {
                Vector3 direction = directionsToCheck[i].normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, direction, moveDistance);

                if (canMove)
                {
                    // Movimentar na direção encontrada
                    moveDirection = direction;
                    break;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
}

