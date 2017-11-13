using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region EnemyType
    // Enum for the EnemyType Variable
    enum EnemyType
    {
        Keeper, // Eye creature that rotates
        Dhole, // Moves to direct path spots
        Slug, // Slow path
        Beast, // Path or stationary, has a hearing radius
        Haunter,
    }
    #endregion

    #region Variables
    // Set this 
    [SerializeField]
    EnemyType enemyType;

    // Rotation Variables
    [SerializeField]
    float rotationSpeed;

    // Pathing Variables
    [SerializeField]
    float timeBetweenPoints;

    private bool justMoved;
    private int currentPathPoint;
    public List<GameObject> pathPoints;
    #endregion


    void Start()
    {
        // Set current path point to it's first
        currentPathPoint = 0;
        justMoved = false;

        Debug.Log("start");
    }

    #region Update
    // Update is called once per frame
    void Update ()
    {
        Move();
	}
    #endregion

    #region Enemy Movement Helper Method
    /// <summary>
    /// Movement method that is called for enemy Movement
    /// </summary>
    void Move()
    {
        switch(enemyType)
        {
            case EnemyType.Keeper:
                KeeperRotate();
                break;

            case EnemyType.Dhole:
                // Only move if the Dhole hasn't just moved.
                if (justMoved == false)
                {
                    Invoke("DholeSwitchHoles", timeBetweenPoints);
                    justMoved = true;
                }
                break;

            case EnemyType.Slug:
                // Path follow
                break;

            case EnemyType.Beast:
                // Path follow if given a path
                // Stationary otherwise
                break;

            case EnemyType.Haunter:
                // Stationary?
                break;
        }
    }
    #endregion


    #region Specific Movement Methods
    /// <summary>
    /// Rotates the enemy towards the player if sensed
    /// </summary>
    void TurnTowardsPoint(Vector3 point)
    {
        Vector3 dir = point - transform.position;
        float angleOfRotation = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }

    /// <summary>
    /// Rotates the enemy unit slowly made for the keeper specifically
    /// </summary>
    void KeeperRotate()
    {
        float zRotationVar = transform.eulerAngles.z;
        zRotationVar += rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, zRotationVar));
    }

    /// <summary>
    /// Rotates the enemy unit slowly
    /// </summary>
    void DholeSwitchHoles()
    {
        // Get the next hole number
        int nextHoleNum = currentPathPoint+1;
        nextHoleNum %= pathPoints.Count;

        // Turn the Dhole towards the next spot
        TurnTowardsPoint(pathPoints[nextHoleNum].transform.position);

        // Move the dhole and update the currentPathPoint
        currentPathPoint = nextHoleNum;
        transform.position = pathPoints[currentPathPoint].transform.position;

        Debug.Log(transform.position);

        Invoke("ResetMovementVariable", timeBetweenPoints);
    }

    /// <summary>
    /// Keeps the player from spamming the shoot button
    /// </summary>
    void ResetMovementVariable()
    {
        // Player Can't shoot for .5 seconds
        //Debug.Log("We Just Shot!");
        justMoved = false;
    }
    #endregion
}
