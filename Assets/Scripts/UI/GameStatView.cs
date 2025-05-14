using System;
using System.Collections;
using GameData;
using TMPro;
using UnityEngine;


namespace Farm.UI
{
    public class GameStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _carrotLabel;
        [SerializeField] private TextMeshProUGUI _carrotLabelSell;
        [SerializeField] private TextMeshProUGUI _expLabel;
        [SerializeField] private TextMeshProUGUI _treeLabel;
        [SerializeField] private TextMeshProUGUI _treeLabelSell;
        [SerializeField] private TextMeshProUGUI _grassLabel;
        [SerializeField] private TextMeshProUGUI _grassLabelSell;
        [SerializeField] private TextMeshProUGUI _coinLabel;
        [SerializeField] private TextMeshProUGUI _waterLabel;
        private TextMeshProUGUI _storage = new TextMeshProUGUI();
        [SerializeField] private LevelUpControler _levelUpControler;


        bool flag = true;

        private float _minDuration = 0.33f;
        private float _maxDuration = 0.66f;

        private void OnEnable()
        {
            GameDataManager.OnCarrotChange += OnCarrotChange;
            GameDataManager.OnExperienceChange += OnExperienceChange;
            GameDataManager.OnTomatoChange += OnTomatoChange;
            GameDataManager.OnCabbageChange += OnCabbageChange;
            GameDataManager.OnCoinChange += OnCoinChange;
            GameDataManager.OnWaterChange += OnWaterChange;
            GameDataManager.OnStorageChange += OnStorageChange;
        }

        private void OnDisable()
        {
            GameDataManager.OnCarrotChange -= OnCarrotChange;
            GameDataManager.OnExperienceChange -= OnExperienceChange;
            GameDataManager.OnTomatoChange -= OnTomatoChange;
            GameDataManager.OnCabbageChange -= OnCabbageChange;
            GameDataManager.OnCoinChange -= OnCoinChange;
            GameDataManager.OnWaterChange -= OnWaterChange;
            GameDataManager.OnStorageChange -= OnStorageChange;
        }

        private void OnCarrotChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateCarrotLabel));
        }

        private void UpdateCarrotLabel(float value)
        {
            _carrotLabel.SetText($"Carrot: {value:0}");
            _carrotLabelSell.SetText($"Carrot: {value:0}");
        }

        private void OnTomatoChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateTomatoLabel));
        }

        private void UpdateTomatoLabel(float value)
        {
            _treeLabel.SetText($"Tomato: {value:0}");
            _treeLabelSell.SetText($"Tomato: {value:0}");
        }

        private void OnCabbageChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateCabbageLabel));
        }

        private void UpdateCabbageLabel(float value)
        {
            _grassLabel.SetText($"Cabbage: {value:0}");
            _grassLabelSell.SetText($"Cabbage: {value:0}");
        }

        private void OnExperienceChange(int prevValue, int newValue)
        {

            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateExperienceLabel));
            string value = "";
            int expValue = 0;

            if (flag)
            {
                if (_expLabel.text.Contains(":"))
                {
                    int colonIndex = _expLabel.text.IndexOf(":");
                    value = _expLabel.text.Substring(colonIndex + 1).Trim();

                    int.TryParse(value, out expValue);
                }
                if (expValue >= 1000)
                {
                    _levelUpControler.levelUpPanel.SetActive(true);
                    flag = false;
                }
            }
        }

        private void UpdateExperienceLabel(float value)
        {
            _expLabel.SetText($"Exp: {value:0}");
        }
        private void OnCoinChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateCoinLabel));
        }
        private void UpdateCoinLabel(float value)
        {
            _coinLabel.SetText($"Coin: {value:0}");
        }
        private void OnWaterChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateWaterLabel));
        }
        private void UpdateWaterLabel(float value)
        {
            _waterLabel.SetText($"Water: {value:0}/12");
        }
        private void OnStorageChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateStorageLabel));
        }
        private void UpdateStorageLabel(float value)
        {
            _storage.SetText($"Storage: {value:0}/12");
        }

        private IEnumerator GradualChangeValue(float startValue, float endValue, Action<float> callback)
        {
            var diff = Mathf.Abs(endValue - startValue);
            var elapsedTime = 0f;
            var dynamicDuration = Mathf.Lerp(_minDuration, _maxDuration, 1f / Mathf.Max(diff, 1f));

            while (elapsedTime < dynamicDuration)
            {
                var t = elapsedTime / dynamicDuration;
                elapsedTime += Time.deltaTime;
                var result = Mathf.SmoothStep(startValue, endValue, t);
                callback?.Invoke(result);

                yield return null;
            }

            callback?.Invoke(endValue);
        }
    }
}