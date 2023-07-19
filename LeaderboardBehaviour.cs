using LootLocker.Requests;
using System.Collections;
using UnityEngine;

namespace SametHope.RapidLeaderboard
{
    /// <summary>
    /// A MonoBehaviour class responsible for handling rapid leaderboard-related operations by utilizing coroutines in runtime.
    /// </summary>
    public class LeaderboardBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Starts a guest session asynchronously and invokes the specified callbacks on success or failure.
        /// </summary>
        /// <param name="onSuccess">Callback to invoke when the guest session is successfully started.</param>
        /// <param name="onFailure">Callback to invoke if starting the guest session fails.</param>
        internal IEnumerator Co_StartSessionAsGuest(System.Action<LootLockerGuestSessionResponse> onSuccess = null, System.Action<LootLockerGuestSessionResponse> onFailure = null)
        {
            bool responseReceived = false;
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    responseReceived = true;
                    onFailure?.Invoke(response);
                    return;
                }
                responseReceived = true;
                onSuccess?.Invoke(response);
            });
            yield return new WaitWhile(() => responseReceived == false);
        }

        /// <summary>
        /// Sets the player name asynchronously and invokes the specified callbacks on success or failure.
        /// </summary>
        /// <param name="nameToSet">The name to set for the player.</param>
        /// <param name="onSuccess">Callback to invoke when the player name is successfully set.</param>
        /// <param name="onFailure">Callback to invoke if setting the player name fails.</param>
        internal IEnumerator Co_SetPlayerName(string nameToSet, System.Action<PlayerNameResponse> onSuccess = null, System.Action<PlayerNameResponse> onFailure = null)
        {
            bool responseReceived = false;
            LootLockerSDKManager.SetPlayerName(nameToSet, (response) =>
            {
                if (!response.success)
                {
                    responseReceived = true;
                    onFailure?.Invoke(response);
                    return;
                }
                responseReceived = true;
                onSuccess?.Invoke(response);
            });
            yield return new WaitWhile(() => responseReceived == false);
        }

        /// <summary>
        /// Submits the player's score asynchronously to the specified leaderboard and invokes the specified callbacks on success or failure.
        /// </summary>
        /// <param name="memberId">The ID of the player whose score is being submitted.</param>
        /// <param name="score">The score to submit.</param>
        /// <param name="leaderboardKey">The key identifying the leaderboard.</param>
        /// <param name="onSuccess">Callback to invoke when the score submission is successful.</param>
        /// <param name="onFailure">Callback to invoke if the score submission fails.</param>
        internal IEnumerator Co_SubmitScore(string memberId, int score, string leaderboardKey, System.Action<LootLockerSubmitScoreResponse> onSuccess = null, System.Action<LootLockerSubmitScoreResponse> onFailure = null)
        {
            bool responseReceived = false;
            LootLockerSDKManager.SubmitScore(memberId, score, leaderboardKey, (response) =>
            {
                if (!response.success)
                {
                    responseReceived = true;
                    onFailure?.Invoke(response);
                    return;
                }
                responseReceived = true;
                onSuccess?.Invoke(response);
            });
            yield return new WaitWhile(() => responseReceived == false);
        }

        /// <summary>
        /// Coroutine that retrieves a list of scores from the specified leaderboard asynchronously.
        /// </summary>
        /// <param name="leaderboardKey">The key identifying the leaderboard.</param>
        /// <param name="count">The number of scores to retrieve.</param>
        /// <param name="onSuccess">Callback to invoke when the scores are successfully retrieved.</param>
        /// <param name="onFailure">Callback to invoke if retrieving the scores fails.</param>
        internal IEnumerator Co_GetScoreList(string leaderboardKey, int count, System.Action<LootLockerGetScoreListResponse> onSuccess = null, System.Action<LootLockerGetScoreListResponse> onFailure = null)
        {
            bool responseReceived = false;
            LootLockerSDKManager.GetScoreList(leaderboardKey, count, (response) =>
            {
                if (!response.success)
                {
                    responseReceived = true;
                    onFailure?.Invoke(response);
                    return;
                }
                responseReceived = true;
                onSuccess?.Invoke(response);
            });
            yield return new WaitWhile(() => responseReceived == false);
        }

        /// <summary>
        /// Coroutine that retrieves the rank of the player from the specified leaderboard asynchronously.
        /// </summary>
        /// <param name="leaderboardKey">The key identifying the leaderboard.</param>
        /// <param name="onSuccess">Callback to invoke when the player's rank is successfully retrieved.</param>
        /// <param name="onFailure">Callback to invoke if retrieving the player's rank fails.</param>
        internal IEnumerator Co_GetMemberRank(string leaderboardKey, System.Action<LootLockerGetMemberRankResponse> onSuccess = null, System.Action<LootLockerGetMemberRankResponse> onFailure = null)
        {
            bool responseReceived = false;
            LootLockerSDKManager.GetMemberRank(leaderboardKey, LeaderboardManager.PlayerID, (response) =>
            {
                if (!response.success)
                {
                    responseReceived = true;
                    onFailure?.Invoke(response);
                    return;
                }
                responseReceived = true;
                onSuccess?.Invoke(response);
            });
            yield return new WaitWhile(() => responseReceived == false);
        }
    }
}
