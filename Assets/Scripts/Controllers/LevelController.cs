using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;

public class LevelController : MonoBehaviour {

    private Level ActiveLevel;

    public int AreaIndex;
    public float TotalElapsedTime;
    public float AreaElapsedTime;
    public List<AreaTile> ActiveTiles;
    public List<Doodad> ActiveDoodads;
    public int DoodadIndex;
    public float CurrentSpeed;
    public AreaTile LatestTile;
    public float CameraHeight;

    // Use this for initialization
    void Start () {
        AreaIndex = 0;
        DoodadIndex = 0;
        TotalElapsedTime = 0f;
        AreaElapsedTime = 0f;
        CurrentSpeed = 0f;
        ActiveTiles = new List<AreaTile>();
        ActiveDoodads = new List<Doodad>();
        LatestTile = null;
        CameraHeight = Camera.main.orthographicSize * 2f;
        SetActiveLevel("Level1");
    }
	
	// Update is called once per frame
	void Update () {
        if (ActiveLevel)
        {
            TotalElapsedTime += Time.deltaTime;
            AreaElapsedTime += Time.deltaTime;

            if (AreaIndex >= ActiveLevel.Areas.Count && ActiveTiles.Count == 0)
            {
                ActiveLevel = null;
                return;
            }

            foreach (AreaTile tile in ActiveTiles.Reverse<AreaTile>())
            {
                float distanceTraveled = Time.deltaTime * CurrentSpeed;
                tile.transform.position -= new Vector3(0, distanceTraveled);
                tile.DistanceTraveled += distanceTraveled;
                if (tile.transform.position.y < -((CameraHeight / 2) + tile.Height / 2))
                {
                    ActiveTiles.Remove(tile);
                    Destroy(tile.gameObject);
                }
            }

            foreach (Doodad doodad in ActiveDoodads.Reverse<Doodad>())
            {
                doodad.transform.position -= new Vector3(0, Time.deltaTime * CurrentSpeed);
                if (doodad.transform.position.y < -((CameraHeight / 2) + doodad.Height / 2))
                {
                    ActiveDoodads.Remove(doodad);
                    Destroy(doodad.gameObject);
                }
            }

            if (AreaIndex < ActiveLevel.Areas.Count)
            {
                while (DoodadIndex < ActiveLevel.Areas[AreaIndex].Doodads.Count && AreaElapsedTime >= ActiveLevel.Areas[AreaIndex].Doodads[DoodadIndex].Time)
                {
                    GameObject DoodadObject = Instantiate(Resources.Load("Prefabs/" + ActiveLevel.Areas[AreaIndex].Doodads[DoodadIndex].id), new Vector3(0, 10, 10), this.transform.rotation) as GameObject;
                    Doodad doodad = DoodadObject.GetComponent<Doodad>();
                    Sprite sprite = doodad.GetComponent<SpriteRenderer>().sprite;
                    doodad.transform.position = new Vector3(0, CameraHeight / 2 + doodad.Height / 2, 5);
                    doodad.Height = sprite.bounds.size.y;
                    ActiveDoodads.Add(doodad);
                    DoodadIndex++;
                }

                if (ActiveLevel.Areas[AreaIndex].AreaType == AreaTypes.Area && LatestTile.DistanceTraveled >= LatestTile.Height)
                {
                    CurrentSpeed = 0;
                }

                if (AreaElapsedTime >= ActiveLevel.Areas[AreaIndex].Time)
                {
                    AreaIndex++;
                    if (AreaIndex < ActiveLevel.Areas.Count)
                    {
                        AreaElapsedTime = 0f;
                        SetNextTile(true);
                        CurrentSpeed = ActiveLevel.Areas[AreaIndex].Speed;
                    }
                }
                else
                {
                    if (LatestTile.DistanceTraveled >= LatestTile.Height && ActiveLevel.Areas[AreaIndex].AreaType != AreaTypes.Area)
                    {
                        SetNextTile(false);
                    }
                }
            }
        }
    }

    public void SetActiveLevel(string levelName)
    {
        if (LevelManager.instance == null)
        {
            Debug.Log("LevelManager has not been initialized yet.  Unable to load a level.");
            return;
        }
        ActiveLevel = LevelManager.instance.Levels.FirstOrDefault(l => l.LevelName == levelName);        
        if (!ActiveLevel)
        {
            Debug.Log("Failed to load level: " + levelName);
            return;
        }

        TotalElapsedTime = 0;
        AreaIndex = 0;
        CurrentSpeed = ActiveLevel.Areas[0].Speed;

        GameObject tileObject = Instantiate(Resources.Load("Prefabs/" + ActiveLevel.Areas[AreaIndex].AreaBackgroundImage), new Vector3(0, 10, 10), this.transform.rotation) as GameObject;
        AreaTile tile = tileObject.GetComponent<AreaTile>();
        Sprite sprite = tileObject.GetComponent<SpriteRenderer>().sprite;

        tile.Height = sprite.bounds.size.y;
        tile.DistanceTraveled = tile.Height;
        tile.transform.position = new Vector3(0, -(CameraHeight / 2) + tile.Height / 2, 10);
        ActiveTiles.Add(tile);
        LatestTile = tile;
    }

    public void SetNextTile(bool changedArea)
    {
        GameObject tileObject = Instantiate(Resources.Load("Prefabs/" + ActiveLevel.Areas[AreaIndex].AreaBackgroundImage), new Vector3(0, 10, 10), this.transform.rotation) as GameObject;
        AreaTile tile = tileObject.GetComponent<AreaTile>();
        Sprite sprite = tileObject.GetComponent<SpriteRenderer>().sprite;
        tile.DistanceTraveled = 0;
        tile.Height = sprite.bounds.size.y;
        ActiveTiles.Add(tile);
        if (LatestTile)
        {
            if (changedArea)
            {
                float diff = 0;
                if (LatestTile.DistanceTraveled > LatestTile.Height)
                {
                    diff = LatestTile.DistanceTraveled - LatestTile.Height;
                }
                tile.transform.position = new Vector3(0, ((CameraHeight / 2) + (tile.Height / 2)) - diff, 10);
                tile.DistanceTraveled += diff;
            }
            else
            {
                Vector3 position = LatestTile.transform.position + new Vector3(0, (LatestTile.Height / 2) + (tile.Height / 2), 0);
                tile.transform.position = position;
                tile.DistanceTraveled = LatestTile.DistanceTraveled - LatestTile.Height;
            }
        }
        LatestTile = tile;
    }
}
