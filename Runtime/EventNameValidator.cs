public static class EventNameValidator
{
    /// <summary>
    /// Validates a Firebase Analytics event name according to Firebase's requirements.
    /// </summary>
    /// <param name="eventName">The event name to validate.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool IsValid(string eventName)
    {
        if (string.IsNullOrEmpty(eventName))
            return false;
        if (eventName.Length >= 40)
            return false;
        if (!char.IsLetter(eventName[0]))
            return false;
        foreach (char c in eventName)
        {
            if (!char.IsLetterOrDigit(c) && c != '_')
                return false;
        }
        return true;
    }

    /// <summary>
    /// Validates event name, parameter name, and parameter value for Firebase Analytics.
    /// </summary>
    /// <param name="eventName">Event name to validate.</param>
    /// <param name="paramName">Parameter name to validate.</param>
    /// <param name="paramValue">Parameter value to validate.</param>
    /// <returns>True if all are valid, false otherwise.</returns>
    public static bool IsValidEventWithParam(string eventName, string paramName, int paramValue)
    {
        if (!IsValid(eventName))
            return false;
        if (string.IsNullOrEmpty(paramName))
            return false;
        if (paramName.Length >= 40)
            return false;
        if (!char.IsLetter(paramName[0]))
            return false;
        foreach (char c in paramName)
        {
            if (!char.IsLetterOrDigit(c) && c != '_')
                return false;
        }
        // paramValue is int, so always valid for Firebase
        return true;
    }
}
