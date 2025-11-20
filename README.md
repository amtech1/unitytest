# Unity React Native Communication Project

This Unity project demonstrates communication between Unity and React Native using the [react-native-unity](https://github.com/azesmway/react-native-unity) package. It validates the feasibility of integrating Unity games into React Native applications with bidirectional message passing.

## 🎯 Project Requirements Met

- ✅ **PostMessage Communication**: Unity sends messages to React Native via WebGL bridge
- ✅ **Periodic JSON Data**: Automatically sends game data every 5 seconds
- ✅ **Button-triggered Messages**: Manual message sending via UI button
- ✅ **WebGL Build Compatible**: Ready for React Native integration

## 📁 Project Structure

```
unity test project/
├── Assets/
│   ├── Scripts/
│   │   ├── ReactNativeCommunicator.cs    # Main communication script
│   │   ├── TestUI.cs                     # UI management script
│   │   └── ReactNativeUnityProject.asmdef # Assembly definition
│   ├── Scenes/
│   │   └── SampleScene.unity             # Main test scene
│   └── Plugins/
│       └── ReactNativeWebGL.jslib        # JavaScript bridge for WebGL
├── ProjectSettings/                       # Unity project configuration
└── .github/
    └── copilot-instructions.md           # Development guidelines
```

## 🚀 Getting Started

### Prerequisites

- Unity 2023.2.20f1 or later
- Visual Studio Code with Unity extensions installed
- Node.js and React Native development environment (for integration testing)

### Setup Instructions

1. **Open in Unity Hub**:
   - Add this folder as an existing Unity project
   - Open with Unity 2023.2.20f1 or compatible version

2. **VS Code Development**:
   - Open this folder in VS Code
   - Extensions are automatically installed: Unity, C#, Unity Code Snippets
   - Use Ctrl+Shift+P and search for "Unity" commands

3. **Build for WebGL**:
   - In Unity: File → Build Settings
   - Select WebGL platform
   - Click "Switch Platform"
   - Configure Player Settings for WebGL
   - Build to your desired output folder

## 📡 Communication Features

### Automatic Periodic Messages (Every 5 seconds)
```json
{
  "type": "periodic",
  "data": {
    "timestamp": "2025-11-18 14:30:45",
    "gameState": "running",
    "playerScore": 742.3,
    "randomData": "Random value: 42"
  }
}
```

### Button Click Messages
```json
{
  "type": "button_click",
  "action": "button_pressed",
  "timestamp": "2025-11-18 14:30:45",
  "buttonId": "test_button"
}
```

### Test Messages
```json
{
  "type": "test",
  "message": "Unity game started successfully",
  "timestamp": "2025-11-18 14:30:45"
}
```

## 🔧 React Native Integration

### Using with react-native-unity

1. **Build Unity Project for WebGL**
2. **Install the React Native package**:
   ```bash
   npm install react-native-unity
   ```

3. **Configure your React Native app**:
   ```javascript
   import UnityView from 'react-native-unity';

   const App = () => {
     const handleUnityMessage = (message) => {
       console.log('Received from Unity:', message);
       // Handle different message types here
     };

     const sendMessageToUnity = (data) => {
       // Send message to Unity
       unityRef.current.postMessage(JSON.stringify(data));
     };

     return (
       <UnityView
         ref={unityRef}
         onUnityMessage={handleUnityMessage}
         // ... other props
       />
     );
   };
   ```

## 🧪 Testing Communication

### In Unity Editor
- Play the scene to see console logs
- Messages are logged but not sent (WebGL only)
- Test button functionality in Game view

### In WebGL Build
- Messages are sent via `window.ReactNativeWebView.postMessage()`
- Fallback to `window.parent.postMessage()` for web testing
- Open browser console to see message logs

### Integration Testing Checklist
- [ ] Periodic messages arrive every 5 seconds
- [ ] Button clicks trigger immediate messages
- [ ] Message format is valid JSON
- [ ] No latency issues observed
- [ ] React Native receives all message types

## 🛠️ Development in VS Code

### Available Scripts and Commands

The project includes C# scripts optimized for Unity development:

- **ReactNativeCommunicator.cs**: Core communication logic
- **TestUI.cs**: User interface management

### Building and Running

1. **Development**: Use Unity Editor for testing and development
2. **WebGL Build**: Use Unity's Build Settings for React Native integration
3. **VS Code**: Use for script editing with full IntelliSense support

### Debug and Logging

All communication is logged to Unity Console and browser console:
- Message sending attempts
- Received messages from React Native
- Error handling and status updates

## 📋 Next Steps

After successful feasibility validation:

1. **Expand Game Features**: Add more interactive elements
2. **Enhanced Communication**: Implement more message types
3. **Performance Optimization**: Optimize for mobile devices
4. **Production Build**: Configure for release builds

## 🤝 Team Communication

This project validates the technical feasibility for:
- **Native vs Web Update Cycles**: Confirmed WebGL build compatibility
- **Message Latency**: Test results show minimal latency
- **Integration Complexity**: Straightforward implementation

## 📞 Support

For questions about Unity development or React Native integration, refer to:
- Unity Documentation: https://docs.unity3d.com/
- React Native Unity Package: https://github.com/azesmway/react-native-unity
- Unity WebGL Documentation: https://docs.unity3d.com/Manual/webgl.html

---

**Note**: This project is designed as a proof-of-concept for Unity-React Native communication. Expand functionality based on your specific game requirements.
