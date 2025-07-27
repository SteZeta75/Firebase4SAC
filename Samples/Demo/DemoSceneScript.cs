using System;
using TMPro;
using UnityEngine;

namespace Demo
{
    public class DemoSceneScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI logText; 
        
        private void Awake()
        {
            AnalyticsManager.DebugLogEvent += Log;
            AnalyticsManager.DebugLogErrorEvent += LogError;
        }
        
        private void Log(string message)
        {
            logText.text += "\n- " + message;
        }
        
        private void LogError(string message)
        {
            logText.text += $"<color=red>\n- {message}</color>";
        }

        public void OnInitializeClick()
        {
            // Initialize the analytics system
            AnalyticsManager.Initialize();
        }

        #region Demo Events

        public void OnStartGameClick()
        {
            // Log a start game event
            AnalyticsManager.LogEvent("game_start");
        }
        
        public void OnLevelStartClick(int levelNumber)
        {
            // Log a level start event with the level number
            AnalyticsManager.LogEvent("level_start", "level_number", "Level " + levelNumber);
        }
        
        public void OnLevelCompleteClick(int levelNumber)
        {
            // Log a level complete event with the level number
            AnalyticsManager.LogEvent("level_complete", "level_number", "Level " + levelNumber);
        }

        #endregion
    }
}
