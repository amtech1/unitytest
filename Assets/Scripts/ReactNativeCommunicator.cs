using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class ReactNativeCommunicator : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SendMessageToReactNative(string message);

    [System.Serializable]
    public class GameData
    {
        public string timestamp;
        public string gameState;
        public float playerScore;
        public string randomData;
    }

    private Coroutine periodicSendCoroutine;

    void Start()
    {
        Debug.Log("ReactNativeCommunicator initialized");
        
        // Start sending periodic messages
        periodicSendCoroutine = StartCoroutine(SendPeriodicMessages());
        
        // Send initial message
        SendTestMessage("Unity game started successfully");
    }

    // Send a test message to React Native
    public void SendTestMessage(string message)
    {
        try
        {
            string jsonMessage = "{\"type\":\"test\",\"message\":\"" + message + "\",\"timestamp\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"}";
            
            Debug.Log("Sending message to React Native: " + jsonMessage);
            
#if UNITY_WEBGL && !UNITY_EDITOR
            SendMessageToReactNative(jsonMessage);
#else
            // For testing in editor
            Debug.Log("Would send to React Native: " + jsonMessage);
#endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error sending message: " + e.Message);
        }
    }

    // Send JSON data every 5 seconds
    IEnumerator SendPeriodicMessages()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            
            GameData data = new GameData
            {
                timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                gameState = "running",
                playerScore = UnityEngine.Random.Range(0f, 1000f),
                randomData = "Random value: " + UnityEngine.Random.Range(1, 100)
            };

            string jsonData = JsonUtility.ToJson(data);
            SendPeriodicData(jsonData);
        }
    }

    private void SendPeriodicData(string jsonData)
    {
        try
        {
            string message = "{\"type\":\"periodic\",\"data\":" + jsonData + "}";
            
            Debug.Log("Sending periodic data: " + message);
            
#if UNITY_WEBGL && !UNITY_EDITOR
            SendMessageToReactNative(message);
#else
            // For testing in editor
            Debug.Log("Periodic data would be sent: " + message);
#endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error sending periodic data: " + e.Message);
        }
    }

    // This method can be called by buttons
    public void SendButtonClickMessage()
    {
        string buttonData = "{\"type\":\"button_click\",\"action\":\"button_pressed\",\"timestamp\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"buttonId\":\"test_button\"}";
        
        try
        {
            Debug.Log("Button clicked - sending message: " + buttonData);
            
#if UNITY_WEBGL && !UNITY_EDITOR
            SendMessageToReactNative(buttonData);
#else
            // For testing in editor
            Debug.Log("Button click would send: " + buttonData);
#endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error sending button click message: " + e.Message);
        }
    }

    // Method to receive messages from React Native
    public void ReceiveMessageFromReactNative(string message)
    {
        Debug.Log("Received message from React Native: " + message);
        
        // Process the message here
        try
        {
            // You can parse JSON and handle different message types
            Debug.Log("Processing message: " + message);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error processing message from React Native: " + e.Message);
        }
    }

    void OnDestroy()
    {
        if (periodicSendCoroutine != null)
        {
            StopCoroutine(periodicSendCoroutine);
        }
    }
}
