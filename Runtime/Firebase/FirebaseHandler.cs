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
        
		public void CheckDependencies(){
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available) {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    _app = FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                    Log("Firebase dependencies resolved successfully.");
                } else {
                    LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                    // Firebase Unity SDK is not safe to use here.
                }
            });
		}
        
        public void ReportEvent(string eventName)
        {
            if (_app == null)
            {
                LogError("Firebase app is not initialized.");
                return;
            }

            // Log the event using Firebase Analytics
            FirebaseAnalytics.LogEvent(eventName);
            Log($"Event logged: {eventName}");
        }
        
        public void ReportEvent(string eventName, string paramName, int paramValue)
        {
            if (_app == null)
            {
                LogError("Firebase app is not initialized.");
                return;
            }
            
            // Log the event using Firebase Analytics
            FirebaseAnalytics.LogEvent(eventName, new Parameter(paramName, paramValue));
            Log($"Event logged: {eventName} with parameter {paramName} = {paramValue}");
        }
        
        private void Log(string message)
        {
            UnityEngine.Debug.Log(message);
            DebugLogEvent?.Invoke(message);
        }
        
        public void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
            DebugLogErrorEvent?.Invoke(message);
        }
    }
}