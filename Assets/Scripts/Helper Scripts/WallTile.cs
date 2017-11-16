using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : MonoBehaviour
{
    public Sprite wallSide;
    public Sprite wallOutCorner;
    public Sprite wallInCorner;

    [SerializeField]
    private static List<WallTile> walls;

    private WallTile[] adjacentTiles = new WallTile[4];

    private SpriteRenderer thisSpriteRenderer;

    // Use this for initialization
    void Awake()
    {
        if (walls == null)
        {
            walls = new List<WallTile>();
        }

        walls.Add(this);

        for (int i = 0; i < adjacentTiles.Length; i++)
        {
            adjacentTiles[i] = null;
        }

        for (int i = 0; i < walls.Count; i++)
        {
            //check if there's already a tile to the up of this
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

            //check for left
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

            //check for up
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

    private void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < adjacentTiles.Length; i++)
        {
            int next = i + 1;
            if (i + 1 >= adjacentTiles.Length)
                next = 0;

            if (!adjacentTiles[i])
            {
                GameObject side = new GameObject();
                side.transform.SetParent(transform);
                side.name = "Wall-Side";
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

                SpriteRenderer r = side.AddComponent<SpriteRenderer>();
                r.sprite = wallSide;

                r.sortingLayerID = thisSpriteRenderer.sortingLayerID;
                r.sortingOrder = thisSpriteRenderer.sortingOrder + 1;
                r.color = thisSpriteRenderer.color;



                if (!adjacentTiles[next])
                {
                    GameObject corner = new GameObject();
                    corner.transform.SetParent(transform);
                    corner.name = "Wall-Out-Corner";
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

                    SpriteRenderer rend = corner.AddComponent<SpriteRenderer>();
                    rend.sprite = wallOutCorner;
                    rend.sortingLayerID = thisSpriteRenderer.sortingLayerID;
                    rend.sortingOrder = thisSpriteRenderer.sortingOrder + 2;
                    rend.color = thisSpriteRenderer.color;
                }
            }

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

                SpriteRenderer rend = corner.AddComponent<SpriteRenderer>();
                rend.sprite = wallInCorner;
                rend.sortingLayerID = thisSpriteRenderer.sortingLayerID;
                rend.sortingOrder = thisSpriteRenderer.sortingOrder + 2;
                rend.color = thisSpriteRenderer.color;
            }
        }
    }

    private void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.up * 2);
    }
}
