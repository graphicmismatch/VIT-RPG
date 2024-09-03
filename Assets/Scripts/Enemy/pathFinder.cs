using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    public float detectionRange = 10f;
    public int updateInterval = 30; 

    public float playerRadius = 1f;
    private Transform playerTransform;
    private Vector2 targetPosition;
    private int frameCounter = 0;
    private bool isPlayerInRange = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null){
            playerTransform = playerObject.transform;
        } else {
            Debug.LogError("Player not found");
        }

        targetPosition = transform.position;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            isPlayerInRange = distanceToPlayer <= detectionRange;

            frameCounter++;
            if (frameCounter >= updateInterval && distanceToPlayer >= playerRadius){
                UpdateTargetPosition();
                frameCounter = 0;
            }

            if (isPlayerInRange){
                Vector2 currentPosition = transform.position;
                transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    void UpdateTargetPosition()
    {
        if (!isPlayerInRange){
            return;
        }

        Vector2 enemyPosition = transform.position;
        Vector2 playerPosition = playerTransform.position;

        Vector2 directionToPlayer = playerPosition - enemyPosition;

        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y)){
            targetPosition = enemyPosition + new Vector2(Mathf.Sign(directionToPlayer.x) * gridSize, 0);
        } else {
            targetPosition = enemyPosition + new Vector2(0, Mathf.Sign(directionToPlayer.y) * gridSize);
        }
    }
}