using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [Header("UI References")]
    public Button testButton;
    public Text statusText;
    public Text logText;

    private ReactNativeCommunicator communicator;
    private string logHistory = "";

    void Start()
    {
        // Find the communicator
        communicator = FindObjectOfType<ReactNativeCommunicator>();
        
        if (communicator == null)
        {
            Debug.LogError("ReactNativeCommunicator not found in scene!");
            return;
        }

        // Setup button
        if (testButton != null)
        {
            testButton.onClick.AddListener(OnTestButtonClicked);
        }

        // Initialize UI
        UpdateStatus("Unity game initialized and ready");
        AddLog("UI initialized successfully");
    }

    public void OnTestButtonClicked()
    {
        if (communicator != null)
        {
            communicator.SendButtonClickMessage();
            AddLog("Test button clicked - message sent");
            UpdateStatus("Button message sent at " + System.DateTime.Now.ToString("HH:mm:ss"));
        }
    }

    private void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = "Status: " + message;
        }
        Debug.Log("Status: " + message);
    }

    private void AddLog(string message)
    {
        string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
        string logEntry = "[" + timestamp + "] " + message;
        
        logHistory = logEntry + "\n" + logHistory;
        
        // Keep only last 10 entries
        string[] lines = logHistory.Split('\n');
        if (lines.Length > 10)
        {
            System.Array.Resize(ref lines, 10);
            logHistory = string.Join("\n", lines);
        }

        if (logText != null)
        {
            logText.text = logHistory;
        }
        
        Debug.Log(logEntry);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any real-time UI updates here
    }
}
