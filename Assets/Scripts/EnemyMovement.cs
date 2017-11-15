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
    // Instance of the player
    GameObject player;

    // Determines the type of enemy
    [SerializeField]
    EnemyType enemyType;

    // Rotation Variables
    [SerializeField]
    float rotationSpeed;

    // Movement Variables
    [SerializeField]
    float movementSpeed;

    // Pathing Variables
    [SerializeField]
    float timeBetweenPoints;

    // Beast Hearing Variables
    [SerializeField]
    float hearDistance;
    [SerializeField]
    float investigationTime;


    private bool justMoved;
    private int currentPathPoint;
    public List<GameObject> pathPoints;
    #endregion

    #region Start
    void Start()
    {
        // Get the instance of the player
        player = GameObject.FindGameObjectWithTag("Player");

        // For Path Following Set current path point to it's first
        currentPathPoint = 0;
        justMoved = false;

        // If you are a Dhole, start the Dhole at it's first hole
        if(enemyType == EnemyType.Dhole)
        {
            transform.rotation = pathPoints[0].transform.rotation;
            transform.position = pathPoints[0].transform.position;
        }

        if(enemyType == EnemyType.Beast)
        {
            GameObject HearingRadius = transform.GetChild(0).gameObject;
            HearingRadius.transform.localScale = new Vector3(hearDistance * 1.5f, hearDistance * 1.5f, 0);
        }

    }
    #endregion

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
                RotateInPlace();
                break;

            case EnemyType.Dhole:
                // Only move if the Dhole hasn't just moved.
                if (justMoved == false)
                {
                    Invoke("SwitchLocations", timeBetweenPoints);
                    justMoved = true;
                }
                break;

            case EnemyType.Slug:
                // Path follow
                PathFollow();
                break;

            case EnemyType.Beast:
                if((player.transform.position - transform.position).magnitude < hearDistance)
                {
                    InvestigateSound();
                }
                //else if()
                
                
                // Path follow if given a path


                //PathFollow();



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
    /// Used for: Looking towards paths for path following and the Beast Hearing Method
    /// </summary>
    Quaternion TurnTowardsPoint(Vector3 point)
    {
        Vector3 dir = point - transform.position;
        float angleOfRotation = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        return Quaternion.Euler(0, 0, angleOfRotation);
    }

    /// <summary>
    /// Rotates the enemy unit slowly made for the keeper specifically
    /// Used for: Keeper
    /// </summary>
    void RotateInPlace()
    {
        float zRotationVar = transform.eulerAngles.z;
        zRotationVar += rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, zRotationVar));
    }

    /// <summary>
    /// Moves an enemy from one location to the next based on a list of locations
    /// Used for: Dhole
    /// </summary>
    void SwitchLocations()
    {
        // Get the next hole number
        int nextHoleNum = currentPathPoint+1;
        nextHoleNum %= pathPoints.Count;

        // Move the dhole, rotate the dhole to the right direction, and update the currentPathPoint
        currentPathPoint = nextHoleNum;
        transform.rotation = pathPoints[currentPathPoint].transform.rotation;
        transform.position = pathPoints[currentPathPoint].transform.position;

        Invoke("ResetMovementVariable", Time.deltaTime * timeBetweenPoints);
    }

    /// <summary>
    /// Basic Path Following
    /// Used for: Slug, Beast, and the Haunter
    /// </summary>
    void PathFollow()
    {
        // If the enemy unit gets close enough to the path point it advances the current path point
        if ((transform.position - pathPoints[currentPathPoint].transform.position).magnitude < .5f)
        {
            // Get the next hole number and set it as the new currentPathPoint
            int nextHoleNum = currentPathPoint + 1;
            nextHoleNum %= pathPoints.Count;
            currentPathPoint = nextHoleNum;
        }

        // Rotate the view of the enemy towards the new point
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TurnTowardsPoint(pathPoints[currentPathPoint].transform.position), Time.deltaTime * rotationSpeed);

        // Step 1: Find Desired Velocity
        // This is the vector pointing from myself to my target
        Vector2 desiredVelocity = pathPoints[currentPathPoint].transform.position - transform.position;

        // Step 2: Scale Desired to maximum speed
        //         so I move as fast as possible
        desiredVelocity.Normalize();
        desiredVelocity *= movementSpeed;

        // Step 3: Calculate your Steering Force
        Vector2 steeringForce = desiredVelocity - GetComponent<Rigidbody2D>().velocity;

        // Clamp the ultimate force by the maximum force
        //Vector3.ClampMagnitude(steeringForce, maxForce);

        // Apply the force
        GetComponent<Rigidbody2D>().AddForce(steeringForce);

        
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

    void InvestigateSound()
    {
        // Rotate the view of the enemy towards the new point
        transform.rotation = TurnTowardsPoint(player.transform.position);

        // Step 1: Find Desired Velocity
        // This is the vector pointing from myself to my target
        Vector2 desiredVelocity = player.transform.position - transform.position;

        // Step 2: Scale Desired to maximum speed
        //         so I move as fast as possible
        desiredVelocity.Normalize();
        desiredVelocity *= movementSpeed;

        // Step 3: Calculate your Steering Force
        Vector2 steeringForce = desiredVelocity - GetComponent<Rigidbody2D>().velocity;

        // Clamp the ultimate force by the maximum force
        //Vector3.ClampMagnitude(steeringForce, maxForce);

        // Apply the force
        GetComponent<Rigidbody2D>().AddForce(steeringForce);
    }
    #endregion


}
