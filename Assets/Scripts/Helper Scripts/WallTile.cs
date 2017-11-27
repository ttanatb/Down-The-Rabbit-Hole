using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : MonoBehaviour
{
    //enum for wall color
    public enum WallColor
    {
        Red,
        Blue,
        Purple
    }

    //sprites for wall visuals
    public Sprite wallSide;
    public Sprite wallOutCorner;
    public Sprite wallInCorner;

    //prefab for floor
    public GameObject floorPrefab;

    //colors
    public WallColor color = WallColor.Red;

    //static variables for list of all walls and if floors were generated
    private static List<WallTile> walls;
    private static bool createdFloor;

    //each wall has an adjacent tile (makes it a graph)
    private WallTile[] adjacentTiles = new WallTile[4];

    //helper for dealing with
    private SpriteRenderer thisSpriteRenderer;

    // Awake
    void Awake()
    {
        //if walls was not created, create a new wall
        if (walls == null)
            walls = new List<WallTile>();

        //adds this script to the wall
        walls.Add(this);

        CheckAdjacency();
    }

    // Start
    private void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();

        //sets the color of the sprite renderer
        switch (color)
        {
            case WallColor.Red:
                break;
            case WallColor.Blue:
                break;
            case WallColor.Purple:
                break;
        }

        PlaceWalls();
        GenerateFloorObjs();
    }

    // OnDestroy
    private void OnDestroy()
    {
        walls.Remove(this);
        if (walls.Count == 0)
        {
            walls = null;
            createdFloor = false;
        }
    }

    /// <summary>
    /// Checks the tiles that are adjacent to this and add to adjacency array
    /// </summary>
    private void CheckAdjacency()
    {
        //loop through other walls in the list
        for (int i = 0; i < walls.Count - 1; i++)
        {
            //check ifor up
            if (!adjacentTiles[0])
            {
                Vector3 upPos = transform.position + Vector3.up * 2;
                if ((walls[i].transform.position - upPos).sqrMagnitude < 0.3f)
                {
                    adjacentTiles[0] = walls[i];
                    walls[i].adjacentTiles[2] = this;
                    continue;
                }
            }

            //check for right
            if (!adjacentTiles[1])
            {
                Vector3 rightPos = transform.position + Vector3.right * 2;
                if ((walls[i].transform.position - rightPos).sqrMagnitude < 0.3f)
                {
                    adjacentTiles[1] = walls[i];
                    walls[i].adjacentTiles[3] = this;
                    continue;
                }
            }

            //check for down
            if (!adjacentTiles[2])
            {
                Vector3 downPos = transform.position - Vector3.up * 2;
                if ((walls[i].transform.position - downPos).sqrMagnitude < 0.3f)
                {
                    adjacentTiles[2] = walls[i];
                    walls[i].adjacentTiles[0] = this;
                    continue;
                }
            }

            //check for left
            if (!adjacentTiles[3])
            {
                Vector3 leftPos = transform.position - Vector3.right * 2;
                if ((walls[i].transform.position - leftPos).sqrMagnitude < 0.3f)
                {
                    adjacentTiles[3] = walls[i];
                    walls[i].adjacentTiles[1] = this;
                    continue;
                }
            }
        }
    }

    /// <summary>
    /// Use adjacency array to place side/corner walls
    /// </summary>
    private void PlaceWalls()
    {
        //generate the side walls
        for (int i = 0; i < adjacentTiles.Length; i++)
        {
            //the next index
            int next = i + 1;
            if (i + 1 >= adjacentTiles.Length)
                next = 0;

            //chcek if there's something adjacent in that way
            if (!adjacentTiles[i])
            {
                //create a side wall
                GameObject side = new GameObject();
                side.transform.SetParent(transform);
                side.name = "Wall-Side";

                //sets its position/rotation
                switch (i)
                {
                    case 0:
                        side.transform.localPosition = new Vector3(0, 0.918f, 0);
                        side.transform.localRotation = Quaternion.Euler(0, 0, 90f);
                        break;
                    case 1:
                        side.transform.localPosition = new Vector3(0.918f, 0, 0);
                        break;
                    case 2:
                        side.transform.localPosition = new Vector3(0, -0.918f, 0);
                        side.transform.localRotation = Quaternion.Euler(0, 0, 270f);
                        break;
                    default:
                        side.transform.localPosition = new Vector3(-0.918f, 0, 0);
                        side.transform.localRotation = Quaternion.Euler(0, 0, 180f);
                        break;
                }

                SetUpWall(side, wallSide, false);

                //check if there's no wall on another adjacent spot (out-corner)
                if (!adjacentTiles[next])
                {
                    //create a corner
                    GameObject corner = new GameObject();
                    corner.transform.SetParent(transform);
                    corner.name = "Wall-Out-Corner";

                    //position/rotate object
                    switch (i)
                    {
                        case 0:
                            corner.transform.localPosition = new Vector3(0.911f, 0.914f, 0);
                            corner.transform.localRotation = Quaternion.Euler(0, 0, 90f);
                            break;
                        case 1:
                            corner.transform.localPosition = new Vector3(0.911f, -0.914f, 0);
                            corner.transform.localRotation = Quaternion.Euler(0, 0, 0f);
                            break;
                        case 2:
                            corner.transform.localPosition = new Vector3(-0.911f, -0.914f, 0);
                            corner.transform.localRotation = Quaternion.Euler(0, 0, 270f);
                            break;
                        default:
                            corner.transform.localPosition = new Vector3(-0.911f, 0.914f, 0);
                            corner.transform.localRotation = Quaternion.Euler(0, 0, -180f);
                            break;
                    }

                    //set up misc color stuff
                    SetUpWall(corner, wallOutCorner, true);
                }
            }

            //check if this wall is an in-corner
            else if (adjacentTiles[next] &&
                    !adjacentTiles[i].adjacentTiles[next] &&
                    !adjacentTiles[next].adjacentTiles[i])
            {
                GameObject corner = new GameObject();
                corner.transform.SetParent(transform);
                corner.name = "Wall-In-Corner";
                switch (i)
                {
                    case 0:
                        corner.transform.localPosition = new Vector3(0.78f, 0.78f, 0);
                        corner.transform.localRotation = Quaternion.Euler(0, 0, 90f);
                        break;
                    case 1:
                        corner.transform.localPosition = new Vector3(0.78f, -0.78f, 0);
                        corner.transform.localRotation = Quaternion.Euler(0, 0, 0f);
                        break;
                    case 2:
                        corner.transform.localPosition = new Vector3(-0.78f, -0.78f, 0);
                        corner.transform.localRotation = Quaternion.Euler(0, 0, 270f);
                        break;
                    default:
                        corner.transform.localPosition = new Vector3(-0.78f, 0.78f, 0);
                        corner.transform.localRotation = Quaternion.Euler(0, 0, -180f);
                        break;
                }
                SetUpWall(corner, wallInCorner, true);
            }
        }
    }

    /// <summary>
    /// Set up extra details for the wals
    /// </summary>
    /// <param name="wallObj">Wall Object</param>
    /// <param name="wallSprite">Sprite for the wall</param>
    /// <param name="isCorner">Is this piece a corner piece?</param>
    private void SetUpWall(GameObject wallObj, Sprite wallSprite, bool isCorner)
    {
        SpriteRenderer rend = wallObj.AddComponent<SpriteRenderer>();
        rend.sprite = wallSprite;
        rend.sortingLayerID = thisSpriteRenderer.sortingLayerID;
        rend.color = thisSpriteRenderer.color;
        rend.sortingOrder = thisSpriteRenderer.sortingOrder + 1;
        if (isCorner)
            rend.sortingOrder += 1;
    }

    /// <summary>
    /// Generate floor objects
    /// </summary>
    private void GenerateFloorObjs()
    {
        if (!createdFloor)
        {
            createdFloor = true;

            //dictionary for min max values for x at that y-position
            Dictionary<int, Vector3> wallMinMax = new Dictionary<int, Vector3>();

            //loops through all the walls
            for (int i = 0; i < walls.Count; i++)
            {
                //instantiate a ykey
                int yKey = (int)walls[i].transform.position.y;

                //adds the key if it doesn't exist
                if (!wallMinMax.ContainsKey(yKey))
                    wallMinMax.Add(yKey, new Vector3(walls[i].transform.position.x, walls[i].transform.position.x, walls[i].transform.position.y)); //initial position

                //replace min
                if (walls[i].transform.position.x < wallMinMax[yKey].x)
                {
                    Vector3 minX = wallMinMax[yKey];
                    minX.x = walls[i].transform.position.x;
                    wallMinMax[yKey] = minX;
                }

                //replace max
                if (walls[i].transform.position.x > wallMinMax[yKey].y)
                {
                    Vector3 minX = wallMinMax[yKey];
                    minX.y = walls[i].transform.position.x;
                    wallMinMax[yKey] = minX;
                }
            }

            //create an empty object for floors
            GameObject floors = new GameObject();
            floors.name = "Floors";

            //loop through all the y-positions
            foreach (int yKey in wallMinMax.Keys)
            {
                //instantiates in the line from min-x to max-x
                for (float x = wallMinMax[yKey].x + 2f; x < wallMinMax[yKey].y - 1f; x += 2f)
                {
                    Instantiate(floorPrefab, new Vector3(x, wallMinMax[yKey].z, 0), Quaternion.identity, floors.transform);
                }
            }
        }
    }
}
