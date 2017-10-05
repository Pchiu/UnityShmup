using Assets.Scripts.DataManagement;
using Assets.Scripts.Ships;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour {

        public static GameController Instance;
        public LevelController LevelController;
        public PlayerController PlayerController;
        public ShipController ShipController;
    
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }

        // Use this for initialization
        void Start () {

            GameDataManager.Instance.LevelManager.CreateTestLevel();
            GameDataManager.Instance.MovementPatternManager.CreateTestMovementPattern();
            GameDataManager.Instance.MovementPatternManager.CreateTestShotPattern();

            var shipObject = ShipController.AssembleTestShip();
            var ship = shipObject.GetComponent<Ship>();
            ship.Initialize();
            //PrefabUtility.CreatePrefab("./Resources/Prefabs/Ships/TestShip.prefab", shipObject, ReplacePrefabOptions.Default);
            //Destroy(shipObject);

            //ship.Initialize();
            PlayerController.CreateTestShip();

            /*
            Ship enemy = ShipController.SpawnShip("Interceptor1", new Vector2(5, 0), 90);
            enemy.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["TestPattern"];
            enemy.SetTarget(PlayerController.PlayerShip.transform);
            enemy.ToggleRotateTowardsTarget(true);
            enemy.Move();
            /*
        
        Ship enemy = ShipController.SpawnShip("Interceptor1", new Vector2(5, 0), 90);
        enemy.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["TestPattern"];
        enemy.SetTarget(PlayerController.PlayerShip.transform);
        enemy.ToggleRotateTowardsTarget(true);
        enemy.BeginMovement();
        */
            //Enemy enemy2 = ShipController.SpawnEnemy("Interceptor1", new Vector2(-5, 2), 45);
            //enemy2.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["TestPattern"];
        }
    }
}
