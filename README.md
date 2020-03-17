# ARPass

## Quick Start
Currently only supporting android build.
1. Get the setting file of Mapbox which is ignored in git.
2. Place it under `Assets/Resources/Mapbox`
3. Build the app.

## Features
- Utilizing `Zenject` library for Dependency Injection.
    - The `Core` scene has this project's core class instances when in play mode, while loading & unloading each scenes by `ARPassSceneManager`.
    - Interfaces are for replacing the real implementation with mocks.
- Using UniRx for Observer pattern.

## Used Libraries
- UniRx
- Zenject
- DoTween
- Mapbox Unity SDK
- Firebase
- JsonDotNet
- GoogleARCore