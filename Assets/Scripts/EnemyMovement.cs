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

    [SerializeField]
    float rotationSpeed;

    public List<GameObject> pathPoints;
    #endregion


    void Start()
    {
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
                // Go to different spots, rotate slightly?
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

    /// <summary>
    /// Rotates the enemy towards the player if sensed
    /// </summary>
    void HearsPlayer(Vector3 playerPosition)
    {
        Vector3 dir = playerPosition - transform.position;
        float angleOfRotation = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }

    /// <summary>
    /// Rotates the enemy unit slowly
    /// </summary>
    void KeeperRotate()
    {
        float zRotationVar = transform.eulerAngles.z;
        zRotationVar += rotationSpeed;
        Debug.Log(zRotationVar);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, zRotationVar));
    }
}
