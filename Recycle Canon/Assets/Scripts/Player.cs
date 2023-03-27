using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CollectorContainer collectorContainer;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 15f;
    private float sphereCastRadius = 1.25f;
    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private bool isWalking;



    void Start()
    {
        collectorContainer = FindObjectOfType<CollectorContainer>();
    }

    void Update()
    {
        PlayerMovement();

        if(Input.touchCount > 0) 
        {
            Touch touch = playerInput.GetTouchScreen();
            if(touch.position.x > Screen.width/3 && touch.position.y < Screen.height/3 && touch.phase == TouchPhase.Began) 
            {
                PlayerCheckObjects();
            }
        }
    }

    private void PlayerMovement() 
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalizedRight();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        // Obter o tamanho da tela em pixels
        Vector2 screenSize = new Vector2(Screen.height, Screen.width);

        // Obter a largura da tela em unidades do mundo
        float screenWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;

        // Calcular o limite da tela em unidades do mundo
        float screenLimitX = screenWidth / 2f;
        float screenLimitZ = Camera.main.orthographicSize;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        // Restringir o movimento para dentro dos limites da tela
        Vector3 newPosition = transform.position + moveDirection * moveDistance;
        newPosition.x = Mathf.Clamp(newPosition.x, -screenLimitX, screenLimitX);
        newPosition.z = Mathf.Clamp(newPosition.z, -screenLimitZ, screenLimitZ);
        
        if (!canMove)
        {
            // Tentar movimentar nas direções pré-definidas
            Vector3[] directionsToCheck = new Vector3[] 
            {
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
            //transform.position += moveDirection * moveDistance;
            transform.position = newPosition;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        isWalking = moveDirection != Vector3.zero;
    }

    private void PlayerCheckObjects() 
    {
        Vector3 sphereCast = new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z);
        
        RaycastHit[] hits = Physics.SphereCastAll(sphereCast, sphereCastRadius, transform.position);
        foreach (RaycastHit hit in hits)
        {
            switch (hit.collider.tag)
            {
                case "TrashOrganic":
                    collectorContainer.AddTrashOrganic(Random.Range(1, 3));
                    Destroy(hit.collider.gameObject);
                    break;

                case "TrashPlastic":
                    collectorContainer.AddTrashPlastic(Random.Range(1, 3));
                    Destroy(hit.collider.gameObject);
                    break;

                case "TrashMetal":
                    collectorContainer.AddTrashMetal(Random.Range(1, 3));
                    Destroy(hit.collider.gameObject);
                    break;

                case "ContainerOrganic":
                    collectorContainer.ReloadOrganicAmmo();
                    break;

                case "ContainerPlastic":
                    collectorContainer.ReloadPlasticAmmo();
                    break;

                case "ContainerMetal":
                    collectorContainer.ReloadMetalAmmo();
                    break;

            }
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Vector3 sphereCast = new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z);
        float sphereCastRadius = 1.25f;
        Gizmos.DrawSphere(sphereCast, sphereCastRadius);
    }
    */
}

