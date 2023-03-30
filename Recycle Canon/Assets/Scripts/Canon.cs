using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType 
{
    Organic, 
    Plastic, 
    Metal 
}

public class Canon : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CollectorContainer collectorContainer;
    
    [SerializeField] private float minRotationAngle = -90f;
    [SerializeField] private float maxRotationAngle = 90f;
    [SerializeField] private float rotateSpeed = 2f;
    private Quaternion lastRotation;

    [SerializeField] Transform pivotCanon;
    [SerializeField] private GameObject[] bulletsPrefab;
    private AmmoType selectedAmmoType;

    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        lastRotation = transform.rotation;

        collectorContainer = FindObjectOfType<CollectorContainer>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    void Update()
    {
        RotationCanon();
        
        if (Input.touchCount > 0) 
        {
            Touch touch = playerInput.GetTouchScreen();

            if (touch.position.x < screenWidth/2 && touch.position.y < screenHeight/2 && touch.phase == TouchPhase.Began)
            {
                Fire();
            }          
        }
 
    }
    public void SelectAmmoType(AmmoType ammoType)
    {
        selectedAmmoType = ammoType;
    }

    private void Fire() 
    {
        switch (selectedAmmoType)
        {
            case AmmoType.Organic:
                if(collectorContainer.OrganicAmmo > 0) 
                {

                    Instantiate(bulletsPrefab[0], pivotCanon.position, pivotCanon.rotation);
                    collectorContainer.DecreaseOrganicAmmo();
                }
                break;

            case AmmoType.Plastic:
                if(collectorContainer.PlasticAmmo > 0) 
                {
                    Instantiate(bulletsPrefab[1], pivotCanon.position, pivotCanon.rotation);
                    collectorContainer.DecreasePlasticAmmo();
                }
                break;

            case AmmoType.Metal:
                if(collectorContainer.MetalAmmo > 0) 
                {                 
                    Instantiate(bulletsPrefab[2], pivotCanon.position, pivotCanon.rotation);
                    collectorContainer.DecreaseMetalAmmo();
                }
                break;
        }
    }

    private void RotationCanon() 
    {
        // Obtem o vetor de movimento normalizado
        Vector2 inputVector = playerInput.GetAimVectorNormalizedLeft();

        // Se houver movimento no joystick
        if (inputVector.magnitude > 0.1f)
        {
            // Calcula o ângulo em radianos
            float angle = Mathf.Atan2(inputVector.x, inputVector.y);

            // Converte para graus
            angle = angle * Mathf.Rad2Deg;

            // Limita o ângulo a uma faixa de -90 a 90 graus
            angle = Mathf.Clamp(angle, minRotationAngle, maxRotationAngle);

            // Cria uma rotação a partir do ângulo
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

            // Armazena a última rotação
            lastRotation = targetRotation;

            // Rotaciona o canhão suavemente em direção à rotação desejada
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime * 100f);
        }
        else // Se não houver movimento no joystick, mantém a última rotação
        {
            transform.rotation = lastRotation;
        }
    }
}
