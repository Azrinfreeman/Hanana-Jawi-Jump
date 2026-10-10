# Documentation review and manual checks

Baseline: `4b84a5a54ef1ac5130dad5bae5940f7f76e34c99`.

## Scope

README and these notes only. No scripts, scenes, assets, settings, packages, gameplay, or saved data changed.

Reviewed question collection and image/answer assignment, movement/trigger paths, answer timing, health, score storage, character keys, profile indexing, build metadata, and reporting fields. Checked documentation links, enabled scene paths, whitespace, and documentation-only scope.

No Unity compilation, runtime/device tests, or live services were exercised. This is not a full-history security or asset-rights audit.

## Source observations

- Recorded editor is Unity 2022.3.60f1; manifest includes URP 17.0.3 and uGUI 2.0.0. Verify package/editor compatibility before promising a working import; dependencies remain unchanged.
- QuestionController appends named QuestionsList children during Awake. Check serialized list contents and question-item hierarchy assumptions before indexing/removing answers.
- GameController has separate question flows, removes question/obstacle entries, and changes behavior when fewer than four question items remain. Check terminal conditions, the final choices, and prompt overlap.
- HealthController indexes healthCount minus one without a zero guard. Test repeated collisions and damage after health depletion.
- PathController uses the first obstacle/edge entries and waypoint indices. Verify empty-list and route-end behavior, repeated triggers, and startup order.
- TimeToAnswer uses unscaled time, stores initial maxTime in Start, resets to it on disable, and divides fill by ten. Check initial activation and configured durations under pause/speed changes.
- Profile selection parses only the last character of a key. Test ten or more profiles. Character unlock/selection keys are shared, while stars/rounds use player indices.
- SoundSettings uses Log10(sliderValue); verify zero/minimum slider behavior. Named objects, fixed child indices, audio components, and singleton initialization need scene checks.
- Local score storage and external reporting are separate; server synchronization is not verified.

## Manual checklist

1. Resolve package compatibility in an approved development setup, check compilation/references, and confirm build settings.
2. Navigate SplashScreen, Menu, Store, and scenes 1–4 with synthetic profiles after reviewing networking.
3. Have a Jawi subject reviewer check scene content. Exercise every answer/audio action, timer expiry, correct/wrong feedback, shrinking question banks, and final-item behavior.
4. Test waypoint/obstacle exhaustion, repeated crashes, zero health, pause/speed/restart, and exactly-once star/round awards.
5. Verify reload/profile switching, character state, multi-digit profile indices, sound-slider bounds, intended device controls, and asset/audio rights before distribution.

Source observations above remain unfixed in this documentation-only change.
