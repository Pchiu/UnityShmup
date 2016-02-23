using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;

public class LevelDataManager {

    public Dictionary<string, Level> Levels;
    public Dictionary<string, Area> Areas;
    // Use this for initialization
    void Start()
    {
        Levels = new Dictionary<string, Level>();
    }

    public void AddLevel(string levelName)
    {

    }

    public void CreateTestLevel()
    {
        Levels = new Dictionary<string, Level>();
        var level = new Level();
        level.LevelName = "Level1";
        level.Areas = new List<Area>();

        var area1 = new Area();
        area1.AreaBackgroundImage = "Background1";
        area1.AreaType = AreaTypes.Scroll;
        area1.Time = 5;
        area1.Speed = 5;
        area1.Doodads = new List<Doodad>();

        var area2 = new Area();
        area2.AreaBackgroundImage = "Background2";
        area2.AreaType = AreaTypes.Scroll;
        area2.Time = 4;
        area2.Speed = 8;
        area2.Doodads = new List<Doodad>();

        level.Areas.Add(area1);
        level.Areas.Add(area2);

        Levels.Add("Level1", level);
    }
}
