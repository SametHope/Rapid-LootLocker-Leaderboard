using LootLocker.Requests;
using UnityEngine;

namespace SametHope.RapidLeaderboard
{
    /// <summary>
    /// Provides a static interface to manage leaderboards for players.
    /// </summary>
    public static class LeaderboardManager
    {
        /// <summary>
        /// The PlayerPrefs key for storing the LootLocker player ID.
        /// </summary>
        public const string PREFKEY_LootLockerPlayerID = "RapidLeaderboardPlayerID";

        /// <summary>
        /// Gets the player ID from PlayerPrefs. Also known as member ID.
        /// </summary>
        public static string PlayerID => PlayerPrefs.GetString(PREFKEY_LootLockerPlayerID);

        private static LeaderboardBehaviour _behaviour;

        /// <summary>
        /// Initializes the RapidLeaderboardManager and establishes a guest session on runtime.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Debug.Log("Initializing Rapid LootLocker Leaderboard.");
            _behaviour = new GameObject("RapidLeaderboardManager").AddComponent<LeaderboardBehaviour>();
            Object.DontDestroyOnLoad(_behaviour);

            StartSessionAsGuest((response) =>
            {
                PlayerPrefs.SetString(PREFKEY_LootLockerPlayerID, response.player_id.ToString());
            }, (failResponse) =>
            {
                Debug.LogWarning($"Could not establish guest session.");
                Debug.LogWarning(failResponse.Error);
            });
        }

        /// <summary>
        /// Starts a guest session with the rapid leaderboard server.
        /// </summary>
        /// <param name="onSuccess">Callback for successful session establishment.</param>
        /// <param name="onFailure">Callback for failed session establishment.</param>
        private static void StartSessionAsGuest(System.Action<LootLockerGuestSessionResponse> onSuccess = null, System.Action<LootLockerGuestSessionResponse> onFailure = null)
        {
            _behaviour.StartCoroutine(_behaviour.Co_StartSessionAsGuest(onSuccess, onFailure));
        }

        /// <summary>
        /// Sets the player name for the current session.
        /// </summary>
        /// <param name="newName">The new name to be set for the player.</param>
        /// <param name="onSuccess">Callback for successful name update.</param>
        /// <param name="onFailure">Callback for failed name update.</param>
        public static void SetName(string newName, System.Action<PlayerNameResponse> onSuccess = null, System.Action<PlayerNameResponse> onFailure = null)
        {
            _behaviour.StartCoroutine(_behaviour.Co_SetPlayerName(newName, onSuccess, onFailure));
        }

        /// <summary>
        /// Submits the player's score to a specific leaderboard.
        /// </summary>
        /// <param name="memberId">The ID of the player whose score is being submitted.</param>
        /// <param name="score">The score value to be submitted.</param>
        /// <param name="leaderBoardKey">The key of the leaderboard to submit the score.</param>
        /// <param name="onSuccess">Callback for successful score submission.</param>
        /// <param name="onFailure">Callback for failed score submission.</param>
        public static void SubmitScore(string memberId, int score, string leaderBoardKey, System.Action<LootLockerSubmitScoreResponse> onSuccess = null, System.Action<LootLockerSubmitScoreResponse> onFailure = null)
        {
            _behaviour.StartCoroutine(_behaviour.Co_SubmitScore(memberId, score, leaderBoardKey, onSuccess, onFailure));
        }

        /// <summary>
        /// Retrieves a list of scores from the specified leaderboard asynchronously.
        /// </summary>
        /// <param name="leaderboardKey">The key identifying the leaderboard.</param>
        /// <param name="count">The number of scores to retrieve.</param>
        /// <param name="onSuccess">Callback to invoke when the scores are successfully retrieved.</param>
        /// <param name="onFailure">Callback to invoke if retrieving the scores fails.</param>
        public static void GetScoreList(string leaderboardKey, int count, System.Action<LootLockerGetScoreListResponse> onSuccess = null, System.Action<LootLockerGetScoreListResponse> onFailure = null)
        {
            _behaviour.StartCoroutine(_behaviour.Co_GetScoreList(leaderboardKey, count, onSuccess, onFailure));
        }

        /// <summary>
        /// Retrieves the rank of the player from the specified leaderboard asynchronously.
        /// </summary>
        /// <param name="leaderboardKey">The key identifying the leaderboard.</param>
        /// <param name="onSuccess">Callback to invoke when the player's rank is successfully retrieved.</param>
        /// <param name="onFailure">Callback to invoke if retrieving the player's rank fails.</param>
        public static void GetMemberRank(string leaderboardKey, System.Action<LootLockerGetMemberRankResponse> onSuccess = null, System.Action<LootLockerGetMemberRankResponse> onFailure = null)
        {
            _behaviour.StartCoroutine(_behaviour.Co_GetMemberRank(leaderboardKey, onSuccess, onFailure));
        }
    }
}
