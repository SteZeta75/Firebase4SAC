using System;
using Firebase.Extensions;
using System.Collections.Generic;
using Firebase.Analytics;

namespace Firebase
{
    public class FirebaseHandler
    {
        private FirebaseApp _app;
        private event Action<string> DebugLogEvent, DebugLogErrorEvent;
        
        public FirebaseHandler(Action<string> logAction, Action<string> errorAction)
        {
            DebugLogEvent = logAction;
            DebugLogErrorEvent = errorAction;
        }
        
        /// <summary>
        /// Checks and fixes Firebase dependencies asynchronously, and initializes the Firebase app if available.
        /// </summary>
		public void CheckDependencies(){
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available) {
                    _app = FirebaseApp.DefaultInstance;
                    
                    Log("Firebase dependencies resolved successfully.");
                } else {
                    LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
		}
        
        /// <summary>
        /// Logs an event to Firebase Analytics without parameters.
        /// </summary>
        /// <param name="eventName">The name of the event to log.</param>
        public void ReportEvent(string eventName)
        {
            if (_app == null)
            {
                LogError("Firebase app is not initialized.");
                return;
            }

            try
            {
                FirebaseAnalytics.LogEvent(eventName);
                Log($"Event logged: {eventName}");
            }
            catch (System.Exception ex)
            {
                LogError($"Failed to log event '{eventName}': {ex.Message}");
            }
        }
        
        /// <summary>
        /// Logs an event to Firebase Analytics with a single integer parameter.
        /// </summary>
        /// <param name="eventName">The name of the event to log.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="paramValue">The integer value of the parameter.</param>
        public void ReportEvent(string eventName, string paramName, int paramValue)
        {
            if (_app == null)
            {
                LogError("Firebase app is not initialized.");
                return;
            }
            
            try
            {
                FirebaseAnalytics.LogEvent(eventName, new Parameter(paramName, paramValue));
                Log($"Event logged: {eventName} with parameter {paramName} = {paramValue}");
            }
            catch (System.Exception ex)
            {
                LogError($"Failed to log event '{eventName}' with parameter '{paramName}': {ex.Message}");
            }
        }
        
        /// <summary>
        /// Logs a message to the Unity console and invokes the debug log event.
        /// </summary>
        /// <param name="message">The message to log.</param>
        private void Log(string message)
        {
            UnityEngine.Debug.Log(message);
            DebugLogEvent?.Invoke(message);
        }
        
        /// <summary>
        /// Logs an error message to the Unity console and invokes the debug log error event.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        private void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
            DebugLogErrorEvent?.Invoke(message);
        }
    }
}