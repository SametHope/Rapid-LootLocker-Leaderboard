# Rapid LootLocker Leaderboard

**Rapid LootLocker Leaderboard** is a Unity plugin that allows you to quickly and rather effortlessly integrate LootLockers guest leaderboards into your applications. It is basically a wrapper for LootLocker that is tailored to make implementing guest leaderboards as fast and easy as possible.

## How to Use

Forenote: It is not shown below but every API call below also offers optional callbacks for both successful and failed operations. **You are expected to use success callbacks to implement your custom behaviours** just like you would using the normal LootLockers API. Only difference is Rapid LootLocker Leaderboard provides a callback for failed operations too and both callbacks are optional.

### LeaderboardManager

The `LeaderboardManager` class provides a static interface to manage leaderboards for players. It handles player sessions, score submissions, and retrieval of player rankings.

```csharp
// Set the player name for the current session.
LeaderboardManager.SetName("PlayerName");

// Submit the player's score to a specific leaderboard.
LeaderboardManager.SubmitScore(memberId, score, leaderboardKey);

// Retrieve a list of scores from the specified leaderboard asynchronously.
LeaderboardManager.GetScoreList(leaderboardKey, count);

// Retrieve the rank of the player from the specified leaderboard asynchronously.
LeaderboardManager.GetMemberRank(leaderboardKey);
```

### GuestLeaderboard

The `GuestLeaderboard` class is a ScriptableObject representing a guest leaderboard. It allows you to submit scores, get a list of scores, and retrieve the player's rank for this specific leaderboard.

```csharp
// Create a new GuestLeaderboard asset and assign the unique leaderboard key.
// This asset will be used to interact with the leaderboard.
GuestLeaderboard leaderboard = CreateInstance<GuestLeaderboard>();
leaderboard.LeaderboardKey = "your_leaderboard_key";

// Submit the player's score to this guest leaderboard asynchronously.
leaderboard.SubmitScore(score);

// Retrieve a list of scores from this guest leaderboard asynchronously.
leaderboard.GetScoreList(count);

// Retrieve the rank of the player from this guest leaderboard asynchronously.
leaderboard.GetMemberRank();
```

### ExampleLeaderboardHandler

The `ExampleLeaderboardHandler` class demonstrates how to handle the UI and interaction for displaying and updating a guest leaderboard in your game.

```csharp
// Set the parameters in the Inspector for the ExampleLeaderboardHandler script to work with your UI.

// Submit the player's score and refresh the leaderboard UI.
ExampleLeaderboardHandler.SubmitAndRefresh();

// Refresh all entries in the leaderboard UI in order.
ExampleLeaderboardHandler.RefreshAllEntriesInOrder();
```

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

Special thanks to the LootLocker team for providing the backend service and making this leaderboard integration possible.

---

**Note**: Please ensure you have the required dependencies and configurations for the project to work correctly. Refer to the [LootLocker documentation](https://docs.lootlocker.com) for more information on integrating their service with your game.
