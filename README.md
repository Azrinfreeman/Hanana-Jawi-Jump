# Hanana Jawi Jump

A Unity Jawi-learning prototype combining timed question choices with a 3D running and obstacle-jumping course. The character moves automatically along waypoints, while answers influence obstacle handling and learning rewards.

## Project highlights

- Scene-authored question items with image-based choices and an answer-audio action.
- Automatic waypoint movement, jump/crash triggers, and health/end-screen handling.
- An unscaled answer countdown, stars, and completed-round totals.
- Local profiles and saved totals using PlayerPrefs.
- Character-selection/unlock state, badges, music, and sound settings.

These are source-based descriptions. Lesson accuracy, serialized content, scene wiring, gameplay, and device compatibility have not been verified by this documentation review.

## Open the project

1. Clone the repository with its required assets. [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) records **Unity 2022.3.60f1**.
2. Add the repository root through Unity Hub. Inspect package resolution before proceeding: the [manifest](Packages/manifest.json) lists URP 17.0.3 and Unity UI 2.0.0 alongside that editor version, so compatibility needs an Editor check. This update does not change versions.
3. Open [SplashScreen](Assets/Scenes/SplashScreen.unity), review backend requests, and use synthetic profiles in a development setup before entering Play mode.
4. Exercise the Menu, Store, and numbered learning scenes.

[Build settings](ProjectSettings/EditorBuildSettings.asset) list SplashScreen, Menu, Store, and scenes 1–4. The manifest also includes Cinemachine 2.10.3, ProBuilder 5.2.3, and TextMesh Pro 3.0.9. No compilation or build is claimed here.

## Code guide

| Area | Source |
| --- | --- |
| Scene question-item collection | [QuestionController](Assets/QuestionController.cs) |
| Question choices, obstacle handling, and rewards | [GameController](Assets/GameController.cs) |
| Automatic movement and collision triggers | [PathController](Assets/PathController.cs) |
| Answer countdown | [TimeToAnswer](Assets/TimeToAnswer.cs) |
| Saved stars and rounds | [CollectionController](Assets/CollectionController.cs) |
| Character selection | [CharacterSelectionController](Assets/CharacterSelectionController.cs) |
| Profile setup and reporting | [GameStartupController](Assets/GameStartupController.cs) |

## Data and networking

PlayerPrefs stores per-player stars and rounds. Character startup/selection uses shared keys such as `Character_No_` and `CurrentCharacterNo_`, so not every item of state is isolated by profile.

GameStartupController contains score reporting to `app-hanana.com/jawimodul2/insertmark.php`, posting player name, player ID, star/round totals, and device name. Server source is not included. No live endpoint was contacted during this review; availability, authorization, and synchronization remain unverified. Review networking before demonstrations with real learner data.

## Status and reuse

See [validation notes](docs/VALIDATION.md) for source observations and manual checks. Documentation only: scripts, scenes, assets, packages, settings, and saved data remain unchanged. No new license is assigned; review bundled asset/audio terms before redistribution.
