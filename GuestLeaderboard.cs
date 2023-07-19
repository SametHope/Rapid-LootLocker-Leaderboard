using LootLocker.Requests;
using UnityEngine;

namespace SametHope.RapidLeaderboard
{
    /// <summary>
    /// A ScriptableObject representing a guest leaderboard.
    /// </summary>
    [CreateAssetMenu(fileName = "Guest Leaderboard", menuName = "SametHope/Rapid Leaderboard/Guest Leaderboard")]
    public class GuestLeaderboard : ScriptableObject
    {
        [Tooltip("The unique key identifying this leaderboard.")]
        public string LeaderboardKey;

        /// <summary>
        /// Submits the player's score to this guest leaderboard asynchronously.
        /// </summary>
        /// <param name="score">The score to submit.</param>
        /// <param name="onSuccess">Callback to invoke when the score submission is successful.</param>
        /// <param name="onFailure">Callback to invoke if the score submission fails.</param>
        public void SubmitScore(int score, System.Action<LootLockerSubmitScoreResponse> onSuccess = null, System.Action<LootLockerSubmitScoreResponse> onFailure = null)
        {
            LeaderboardManager.SubmitScore(LeaderboardManager.PlayerID, score, LeaderboardKey, onSuccess, onFailure);
        }

        /// <summary>
        /// Retrieves a list of scores from this guest leaderboard asynchronously.
        /// </summary>
        /// <param name="count">The number of scores to retrieve.</param>
        /// <param name="onSuccess">Callback to invoke when the scores are successfully retrieved.</param>
        /// <param name="onFailure">Callback to invoke if retrieving the scores fails.</param>
        public void GetScoreList(int count, System.Action<LootLockerGetScoreListResponse> onSuccess = null, System.Action<LootLockerGetScoreListResponse> onFailure = null)
        {
            LeaderboardManager.GetScoreList(LeaderboardKey, count, onSuccess, onFailure);
        }

        /// <summary>
        /// Retrieves the rank of the player from this guest leaderboard asynchronously.
        /// </summary>
        /// <param name="onSuccess">Callback to invoke when the player's rank is successfully retrieved.</param>
        /// <param name="onFailure">Callback to invoke if retrieving the player's rank fails.</param>
        public void GetMemberRank(System.Action<LootLockerGetMemberRankResponse> onSuccess = null, System.Action<LootLockerGetMemberRankResponse> onFailure = null)
        {
            LeaderboardManager.GetMemberRank(LeaderboardKey, onSuccess, onFailure);
        }
    }
}
