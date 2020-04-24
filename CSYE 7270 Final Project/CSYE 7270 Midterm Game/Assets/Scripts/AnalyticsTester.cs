using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Reference the Unity Analytics namespace
using UnityEngine.Analytics;


/// <summary>
// This Script is used to test custom event for Unity Analytics
/// </summary>
public class AnalyticsTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReportSecretFound(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReportSecretFound(int secretID)
    {
        AnalyticsEvent.Custom("secret_found", new Dictionary<string, object>
        {
            { "secret_id", secretID },
            { "time_elapsed", Time.timeSinceLevelLoad }
        });
    }

}
