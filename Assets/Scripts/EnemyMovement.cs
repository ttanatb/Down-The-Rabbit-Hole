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
    public EnemyType enemyType;
    public List<GameObject> pathPoints;
    #endregion

    #region Update
    // Update is called once per frame
    void Update ()
    {
        EnemyMovement();
	}
    #endregion

    #region Enemy Movement Helper Method
    /// <summary>
    /// Movement method that is called for enemy Movement
    /// </summary>
    void EnemyMovement()
    {
        switch(enemyType)
        {
            case EnemyType.Keeper:
                // Rotate
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
}
