using System;
using Firebase;
using UnityEngine;

public static class AnalyticsManager 
{
    public static event Action<string> DebugLogEvent, DebugLogErrorEvent;
    private static FirebaseHandler firebaseHandler;
    
    /// <summary>
    /// Initializes the analytics system and sets up the Firebase handler.
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
    /// Logs an event to the analytics system without parameters.
    /// </summary>
    /// <param name="eventName">The name of the event to log.</param>
    public static void LogEvent(string eventName)
    {
        if (firebaseHandler == null)
        {
            LogError("Analytics system not initialized. Call Initialize() first.");
            return;
        }
        if (!EventNameValidator.IsValid(eventName))
        {
            LogError("Invalid event name. Event names must be alphanumeric, start with a letter, be less than 40 characters, and may only contain letters, numbers, and underscores.");
            return;
        }
        // Logic to log an event
        firebaseHandler.ReportEvent(eventName);
    }
    
    /// <summary>
    /// Logs an event to the analytics system with a single integer parameter.
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
        if (!EventNameValidator.IsValidEventWithParam(eventName, paramName, paramValue))
        {
            LogError("Invalid event or parameter name. Event and parameter names must be alphanumeric, start with a letter, be less than 40 characters, and may only contain letters, numbers, and underscores. Parameter name cannot be null or empty.");
            return;
        }
        // Logic to log an event with parameters
        firebaseHandler.ReportEvent(eventName, paramName, paramValue);
    }
    
    /// <summary>
    /// Logs an event to the analytics system with a string parameter.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <param name="paramValue">Value of the parameter.</param>
    public static void LogEvent(string eventName, string paramName, string paramValue)
    {
        if (firebaseHandler == null)
        {
            LogError("Analytics system not initialized. Call Initialize() first.");
            return;
        }
        if (!EventNameValidator.IsValidEventWithParam(eventName, paramName, paramValue))
        {
            LogError("Invalid event or parameter name. Event and parameter names must be alphanumeric, start with a letter, be less than 40 characters, and may only contain letters, numbers, and underscores. Parameter name cannot be null or empty.");
            return;
        }
        // Logic to log an event with parameters
        firebaseHandler.ReportEvent(eventName, paramName, paramValue);
    }
    
    /// <summary>
    /// Logs a message to the Unity console and invokes the debug log event.
    /// </summary>
    /// <param name="message">The message to log.</param>
    private static void Log(string message)
    {
        Debug.Log(message);
        DebugLogEvent?.Invoke(message);
    }
    
    /// <summary>
    /// Logs an error message to the Unity console and invokes the debug log error event.
    /// </summary>
    /// <param name="message">The error message to log.</param>
    private static void LogError(string message)
    {
        Debug.LogError(message);
        DebugLogErrorEvent?.Invoke(message);
    }
}
