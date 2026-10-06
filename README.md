# 🌍 Quiz The Globe

A flag-guessing geography quiz built with **Unity** and **C#**, with two game modes, six continents, and saved best scores. Developed as a team project during our game development internship.

## 🎮 Play the Game

### [▶ Play Quiz The Globe Online](https://portal.uetgamestudio.com/games/quizglobe)

No download needed. It runs in your browser on desktop and mobile.

## 📌 About the Game

The game shows a country name, and the player must pick the correct flag from four options. Players choose a continent, pick a game mode, and try to answer as many questions as they can. Flags flip with a rotation animation between questions, and each answer gets instant visual and audio feedback.

## 🎯 Features

- **Six continents:** Europe, Asia, Africa, North America, South America, and Oceania
- **Two game modes:**
  - **Time Trial:** start with 10 seconds, gain 4 seconds for each correct answer, and lose 2 seconds for each wrong one. The run ends when time runs out or every flag has been answered.
  - **Survival:** start with 3 lifelines and lose one for every wrong answer. The run ends when all lifelines are gone or every flag has been answered.
- **Four-flag questions:** the correct flag is placed in a random position each round, with other flags from the same continent as options
- **No repeated questions:** the game tracks which countries have already been asked and picks unasked ones first
- **Progress counter** showing how many flags have been asked out of the continent's total
- **Best scores saved per continent and per mode**, shown on a progress panel (stored with `PlayerPrefs`)
- **Game Over panel** showing correct and incorrect answer counts
- **Animated feedback:** flag flip rotation, an animated correct/wrong checkmark, and sound effects for right and wrong answers
- **Animated UI panels** that scale in and out, such as the game mode selection panel
- **Platform-aware setup:** on WebGL the game detects mobile devices and switches to the mobile canvas and mobile game scene, otherwise it loads the web version

## 🧩 Key Scripts

| Script | What it does |
|---|---|
| `GameData.cs` | Holds the country data (name, flag sprite) for each continent and copies the selected continent's set into the game |
| `CurrentGameData.cs` | Picks the correct flag and the other options, tracks which countries were already asked, and counts flags left |
| `FlagManager.cs` | Assigns flags to the four on-screen objects, animates the flip, shows the country name, and handles game over and best-score saving |
| `Flag.cs` | Handles the player's click on a flag, checks the answer, and notifies the active game mode |
| `TimeTrialMode.cs` | Time Trial logic: countdown timer, time bonus and penalty, and game over |
| `SurvivalModeManager.cs` | Survival logic: lifelines, losing a life, and game over |
| `GameSettings.cs` | Singleton storing the selected continent and game mode |
| `GameModeSelector.cs` / `GameInitializer.cs` | Loads the mobile or web game scene and activates the chosen mode |
| `ProgressManager.cs` / `ProgressPanelManager.cs` | Saves and displays the best score per continent and mode |
| `ScoreManager.cs` | Shows the "questioned / total" flag counter for the selected continent |
| `Checkbox.cs` / `AudioManager.cs` | Correct and wrong animation plus sound effects |
| `ContinentButton.cs` / `ContinentExit.cs` | Continent selection and the animated game mode panel |
| `CanvasManager.cs` / `PickMenuCanvasManger.cs` | Mobile vs. web UI detection |

## 🛠️ Built With

| Technology | Purpose |
|---|---|
| Unity (2D) | Game engine |
| C# | Gameplay, UI, and data logic |
| WebGL | Browser deployment |
| PlayerPrefs | Saving best scores |
| Git & GitHub | Version control |

## 📂 Project Structure

```
Quiz-The-Globe/
├── Assets/           # Scenes, scripts, flag sprites, audio, prefabs
├── Packages/         # Unity package configuration
└── ProjectSettings/  # Unity project configuration
```

## ▶ How to Open the Project

1. Install Unity Hub and the Unity version listed in `ProjectSettings/ProjectVersion.txt`.
2. Clone the repository:
   ```
   git clone https://github.com/Mtaha-az/Quiz-The-Globe.git
   ```
3. Add the folder in Unity Hub and open it.
4. Open the **MainMenu** scene from `Assets` and press **Play**.

## 👥 Team

This game was built as a team project during our game development internship at UET Game Studio.

| Name | Contribution |
|---|---|
| **Salman Sadiq** | Main developer. Built the core game and most of the gameplay systems. |
| **Taha** ([Mtaha-az](https://github.com/Mtaha-az)) | Built the UI and menus, added music, sound effects, and animated UI, wrote the device detection script that switches between the desktop (landscape) and mobile (portrait) layouts, and tested the game and fixed bugs. |

## 📫 Contact

[GitHub: Mtaha-az](https://github.com/Mtaha-az)
