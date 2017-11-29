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
    [SerializeField]
    bool rotateClockwise;

    // Variables to hold original position and rotation
    Vector3 startingPosition;
    Quaternion startingRotation;

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
    bool investigating;
    bool returnedHome;
    Vector3 investigationPoint;


    private bool justMoved;
    private int currentPathPoint;
    public List<GameObject> pathPoints;
    #endregion

    #region Start
    void Start()
    {
        // Get the instance of the player
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Set the initial position of the enemy (for investigation enemies)
        startingPosition = transform.position;
        startingRotation = transform.localRotation;
        returnedHome = true;

        // For Path Following Set current path point to it's first
        currentPathPoint = 0;
        justMoved = false;

        // If you are a Dhole, start the Dhole at it's first hole
        if(enemyType == EnemyType.Dhole)
        {
            transform.rotation = pathPoints[0].transform.rotation;
            transform.position = pathPoints[0].transform.position;
        }

        // If you are a beast (investigation unit) set the visual radius for the hearing distance
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
                // Check if the player is in the hearing radius and set investigating to true if so
                if((player.transform.position - transform.position).magnitude < hearDistance)
                {
                    // Save the spot to investigate
                    investigationPoint = player.transform.position;

                    // Set investigation bool to true and returnedHome (at initial position) to false
                    investigating = true;
                    returnedHome = false;
                }

                // Have the enemy investigate the sound heard
                if(investigating)
                {
                    InvestigateSound();
                    //Debug.Log("Investigating");
                }
                // If not at starting point and not investigating then return home
                else if (!returnedHome)
                {
                    MoveTowards(startingPosition);

                    // If the beast arrives back at it's staring position
                    if ((startingPosition - transform.position).magnitude < .1f)
                    {
                        returnedHome = true;
                        GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }
                // If at initial spot rotate looking direction to original look direction
                else if(returnedHome && !investigating)
                {
                    // Rotate the view of the enemy towards the new point
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, startingRotation, Time.deltaTime * rotationSpeed);
                    //Debug.Log("Rotating to initail rotation");
                }
                
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

        // Rotate Clockwise or CounterClockwise
        if (rotateClockwise) { zRotationVar += rotationSpeed; }
        else { zRotationVar -= rotationSpeed; }

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
        if ((transform.position - pathPoints[currentPathPoint].transform.position).magnitude <= .01f)
        {
            // Get the next hole number and set it as the new currentPathPoint
            int nextHoleNum = currentPathPoint + 1;
            nextHoleNum %= pathPoints.Count;
            currentPathPoint = nextHoleNum;
            GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        MoveTowards(pathPoints[currentPathPoint].transform.position);
    }

    /// <summary>
    /// Resets the just moved variable
    /// This is called with a Courotine to have a time delay between movement actions.
    /// </summary>
    void ResetMovementVariable()
    {
        // Set the justMoved Variable to false
        justMoved = false;
    }


    /// <summary>
    /// Investigation unit
    /// Used for: Beast
    /// </summary>
    void InvestigateSound()
    {
        MoveTowards(investigationPoint);

        // After it reaches the investigating point turn off investigation
        if((transform.position - investigationPoint).magnitude < .1f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            investigating = false;
        }
    }

    /// <summary>
    /// Turns and moves a unit towards an individual point
    /// Used for: Slug, Beast, Haunter
    /// </summary>
    void MoveTowards(Vector3 point)
    {
        // Rotate the view of the enemy towards the new point
        //transform.rotation = TurnTowardsPoint(point);


        // Rotate the view of the enemy towards the new point
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TurnTowardsPoint(point), Time.deltaTime * rotationSpeed);

        // Step 1: Find Desired Velocity
        // This is the vector pointing from myself to my target
        Vector2 desiredVelocity = point - transform.position;

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
