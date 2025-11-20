// JavaScript functions for Unity WebGL to React Native communication
mergeInto(LibraryManager.library, {
    SendMessageToReactNative: function(message) {
        var messageStr = UTF8ToString(message);
        console.log("Unity sending message:", messageStr);
        
        // Check if we're in React Native WebView
        if (window.ReactNativeWebView) {
            window.ReactNativeWebView.postMessage(messageStr);
        } else if (window.parent && window.parent.postMessage) {
            // Fallback for regular web environment
            window.parent.postMessage(messageStr, '*');
        } else {
            console.log("No React Native WebView found, message would be:", messageStr);
        }
    },

    // Function to receive messages from React Native
    ReceiveMessageFromReactNative: function(message) {
        var messageStr = UTF8ToString(message);
        console.log("Received message from React Native:", messageStr);
        
        // Send to Unity GameObject
        if (typeof SendMessage !== 'undefined') {
            SendMessage('ReactNativeCommunicator', 'ReceiveMessageFromReactNative', messageStr);
        }
    }
});

// Listen for messages from React Native
if (typeof window !== 'undefined') {
    window.addEventListener('message', function(event) {
        console.log("WebGL received message:", event.data);
        
        // Forward message to Unity
        if (typeof Module !== 'undefined' && Module.SendMessage) {
            Module.SendMessage('ReactNativeCommunicator', 'ReceiveMessageFromReactNative', event.data);
        }
    });
}
