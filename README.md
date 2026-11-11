# Throw a Ball — Mobile Prototype

A physics-based mobile prototype in Unity: drag to aim, see the trajectory, and throw every ball into the hole.

> Built in April 2020 (commit history: 1 day). Archived as a finished prototype.

## Gameplay

- Drag on the screen to aim. A dotted line shows the path the ball will take.
- Release to throw. Get the required number of balls (shown at the top) into the hole before you run out.
- Successful throws in a row build a **streak**: each shot's score is multiplied by the streak count.

## Technical highlights

- **Trajectory preview:** `TrajectorySimulation` calculates the projectile path from the start position and velocity and draws it before the throw.
- **Data-driven levels:** ball count, required ball count and scene elements live in a `ScriptableObject` (`Assets/Levels/Level_0.asset`).
- **Score, streak and UI:** `GameManager` runs the score and streak system, and `UserInterfaceManager` handles the UI states.
- **Screen fit:** the camera adapts to any screen ratio (`CameraAspectRatioFix`).

## Project layout

```
Assets/Scripts/
  BallController.cs         aiming and throwing
  TrajectorySimulation.cs   trajectory preview
  Ball.cs                   ball behaviour
  GameManager.cs            levels, score, streaks, win/lose
  UserInterfaceManager.cs
  SoLevel.cs                level data
```

## Run it

Open the project in Unity **2019.3.4f1** and play the scene in `Assets/Scenes`. Portrait orientation, 1080×2160 recommended.

## Tech

Unity 2019.3 · C# · TextMesh Pro
