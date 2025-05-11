using System;
using System.IO;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace GameData
{
    public class GameData<T>
    {
        public T Carrot { get; set; }
        public T Experience { get; set; }
        public T Tree { get; set; }
        public T Grass { get; set; }
        public T Coin { get; set; }
        public T Water { get; set; }
        public T Storage { get; set; }

        public GameData(T carrot, T experience,
            T tree, T grass, T coin, T water, T storage)
        {
            Carrot = carrot;
            Experience = experience;
            Tree = tree;
            Grass = grass;
            Coin = coin;
            Water = water;
            Storage = storage;
        }
    }

    public class GameDataManager : MonoBehaviour
    {
        public static Action<int, int> OnCarrotChange;
        public static Action<int, int> OnExperienceChange;
        public static Action<int, int> OnTreeChange;
        public static Action<int, int> OnGrassChange;
        public static Action<int, int> OnCoinChange;
        public static Action<int, int> OnWaterChange;
        public static Action<int, int> OnStorageChange;

        private static GameData<int> _gameData;
        private static string _dataPath;
        private const string FileName = "GameData.json";

        private void Awake()
        {
            _dataPath = GetGameDataPath();
            var loadedGameData = Load<GameData<int>>(_dataPath);
            _gameData = loadedGameData != null
                ? new GameData<int>(loadedGameData.Carrot, loadedGameData.Experience,
                loadedGameData.Tree, loadedGameData.Grass, loadedGameData.Coin, loadedGameData.Water, loadedGameData.Storage)
                : new GameData<int>(0, 0, 0, 0, 0, 0, 0);
        }

        private void Start()
        {
            FireEvents();
        }

        private static void FireEvents()
        {
            OnCarrotChange?.Invoke(0, _gameData.Carrot);
            OnExperienceChange?.Invoke(0, _gameData.Experience);
            OnTreeChange?.Invoke(0, _gameData.Tree);
            OnGrassChange?.Invoke(0, _gameData.Grass);
            OnCoinChange?.Invoke(0, _gameData.Coin);
            OnWaterChange?.Invoke(0, _gameData.Water);
            OnStorageChange?.Invoke(0, _gameData.Storage);
        }

        public static void AddCoin(int value)
        {
            var prevValue = _gameData.Coin;
            _gameData.Coin += value;
            OnCoinChange?.Invoke(prevValue, _gameData.Coin);
        }
        public static bool ReduceCoin(int value)
        {
            if (_gameData.Water == 12)
            {
                return false;
            }

            var prevValue = _gameData.Coin;

            if (value == 999)
            {
                var currWater = _gameData.Water;
                var reqWater = 12 - currWater; 
                var reducingCost = reqWater * 2;
                value = reducingCost;
            }

            if (_gameData.Coin - value < 0)
            {
                return false;
            }
            else
            {
                _gameData.Coin -= value;
            }

            OnCoinChange?.Invoke(prevValue, _gameData.Coin);
            return true;
        }
        public static void AddWater(int value)
        {
            var prevValue = _gameData.Water;
            _gameData.Water += value;
            if (_gameData.Water > 12)
            {
                _gameData.Water = 12;
            }
            OnWaterChange?.Invoke(prevValue, _gameData.Water);
        }
        public static void ReduceWater(int value)
        {
            var prevValue = _gameData.Water;
            if (_gameData.Water - value < 0)
                _gameData.Water = 0;
            else
                _gameData.Water -= value;
            OnWaterChange?.Invoke(prevValue, _gameData.Water);
        }

        public static void AddCarrot()
        {
            var prevValue = _gameData.Carrot;
            _gameData.Carrot++;
            SetStorage();
            OnCarrotChange?.Invoke(prevValue, _gameData.Carrot);
        }

        public static void AddExperience(int value)
        {
            var prevValue = _gameData.Experience;
            _gameData.Experience += value;
            OnExperienceChange?.Invoke(prevValue, _gameData.Experience);
        }

        public static void AddTree()
        {
            var prevValue = _gameData.Tree;
            _gameData.Tree++;
            SetStorage();
            OnTreeChange?.Invoke(prevValue, _gameData.Tree);
        }

        public static void AddGrass()
        {
            var prevValue = _gameData.Grass;
            _gameData.Grass++;
            SetStorage();
            OnGrassChange?.Invoke(prevValue, _gameData.Grass);
        }

        private static void Save<T>(GameData<T> gameData, string dataPath)
        {
            var dataJson = JsonConvert.SerializeObject(gameData);
            File.WriteAllText(dataPath, dataJson);
        }

        private static T Load<T>(string dataPath)
        {
            T gameData = default;
            if (File.Exists(dataPath))
            {
                var dataJson = File.ReadAllText(dataPath);
                gameData = JsonConvert.DeserializeObject<T>(dataJson);
            }

            return gameData;
        }

        public static bool IsStorageFull(){
            if(_gameData.Storage < 12)
                return false;
            else
                return true;
        }
        private static void SetStorage(int value = 1)
        {
            var prevValue = _gameData.Storage;
            _gameData.Storage += value;
            OnStorageChange?.Invoke(prevValue, _gameData.Storage);
        }

        private string GetGameDataPath()
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, "Data", FileName);
#elif UNITY_STANDALONE
    return Path.Combine(Application.persistentDataPath, FileName);
#else
            return Path.Combine(Application.persistentDataPath, FileName);
#endif
        }

#if UNITY_EDITOR
        [MenuItem("Project Tools/Reset Game Data")]
        private static void ResetGameData()
        {
            _gameData = new GameData<int>(0, 0, 0, 0, 0, 0, 0);
            Save(_gameData, _dataPath);
            FireEvents();

            Debug.LogWarning("Game Data is Reset");
        }
#endif

        private void OnApplicationQuit()
        {
            Save(_gameData, _dataPath);
        }
    }
}