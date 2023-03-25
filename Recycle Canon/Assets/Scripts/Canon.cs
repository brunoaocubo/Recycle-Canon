using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float minRotationAngle = -90f;
    [SerializeField] private float maxRotationAngle = 90f;
    [SerializeField] private float rotateSpeed = 2f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Obtem o vetor de movimento normalizado
        Vector2 inputVector = playerInput.GetAimVectorNormalizedLeft();
        bool fire = playerInput.GetFireButton();
        /*
        float angle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -90f, 90f);
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        */

        // Calcula o ângulo em radianos
        float angle = Mathf.Atan2(inputVector.x, inputVector.y);

        // Converte para graus
        angle = angle * Mathf.Rad2Deg;

        // Limita o ângulo a uma faixa de -90 a 90 graus
        angle = Mathf.Clamp(angle, minRotationAngle, maxRotationAngle);

        // Cria uma rotação a partir do ângulo
        Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

        // Rotaciona o canhão suavemente em direção à rotação desejada
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

}
