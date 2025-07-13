using System;
using Firebase;
using UnityEngine;

public static class AnalyticsManager 
{
    public static event Action<string> DebugLogEvent, DebugLogErrorEvent;
    private static FirebaseHandler firebaseHandler;
    
    /// <summary>
    /// Initializes the analytics system.
    /// </summary>
    public static void Initialize()
    {
        firebaseHandler = new FirebaseHandler(
            DebugLogEvent, DebugLogErrorEvent
        );
        // Initialization logic for the analytics system
        Log("Analytics system initialized.");
        firebaseHandler.CheckDependencies();
    }

    /// <summary>
    /// Logs an event to the analytics system.
    /// </summary>
    /// <param name="eventName">The name of the event to log.</param>
    public static void LogEvent(string eventName)
    {
        if (firebaseHandler == null)
        {
            LogError("Analytics system not initialized. Call Initialize() first.");
            return;
        }
        if (string.IsNullOrEmpty(eventName))
        {
            LogError("Event name cannot be null or empty.");
            return;
        }
        
        // Logic to log an event
        firebaseHandler.ReportEvent(eventName);
    }
    
    
    /// <summary>
    /// Logs an event with a parameter to the analytics system.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <param name="paramValue">Value of the parameter.</param>
    public static void LogEvent(string eventName, string paramName, int paramValue)
    {
        if (firebaseHandler == null)
        {
            LogError("Analytics system not initialized. Call Initialize() first.");
            return;
        }
        if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(paramName))
        {
            LogError("Event name and parameter name cannot be null or empty.");
            return;
        }
        
        // Logic to log an event with parameters
        firebaseHandler.ReportEvent(eventName, paramName, paramValue);
    }
    
    private static void Log(string message)
    {
        Debug.Log(message);
        DebugLogEvent?.Invoke(message);
    }
        
    private static void LogError(string message)
    {
        Debug.LogError(message);
        DebugLogErrorEvent?.Invoke(message);
    }
}
