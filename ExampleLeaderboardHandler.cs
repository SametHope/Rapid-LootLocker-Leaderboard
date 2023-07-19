using LootLocker;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SametHope.RapidLeaderboard
{
    /// <summary>
    /// Handles the UI and interaction for displaying and updating a guest leaderboard.
    /// </summary>
    public class ExampleLeaderboardHandler : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameObject _boardContent;
        [SerializeField] private Button _submitButton;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private GuestLeaderboard _leaderboard;
        [Tooltip("Should leaderboard get refreshed automatically when this object gets enabled.")]
        [SerializeField] private bool _refreshOnEnable;

        [Header("Session Entry Texts")]
        [SerializeField] private TextMeshProUGUI _sessionRankText;
        [SerializeField] private TextMeshProUGUI _sessionNameText;
        [SerializeField] private TextMeshProUGUI _sessionScoreText;

        [Header("Other Entry Texts")]
        [SerializeField] private TextMeshProUGUI[] _rankTexts;
        [SerializeField] private TextMeshProUGUI[] _nameTexts;
        [SerializeField] private TextMeshProUGUI[] _scoreTexts;

        [Header("Information Texts")]
        [SerializeField] private TextMeshProUGUI _infoTMP;
        [SerializeField] private string _defaultLoadingText = "Loading...";
        [SerializeField] private string _defaultFailureText = "Something went wrong \n<size=16>{0}</size>";
        [SerializeField] private string _defaultSuccessText = "";

        /// <summary>
        /// This function is used to get the score. Set it from another class when the game ends, etc.
        /// </summary>
        public static Func<int> GetSessionScore = () => { return -1; };

        private void OnEnable()
        {
            if (_refreshOnEnable)
            {
                SetForLoading();
                RefreshAllEntriesInOrder(() => SetForSuccess());
            }
        }

        /// <summary>
        /// Submits the player's score and refreshes the leaderboard UI.
        /// </summary>
        public void SubmitAndRefresh()
        {
            SetForLoading();
            LeaderboardManager.SetName(_nameField.text, (nameResponse) =>
            {
                _leaderboard.SubmitScore(GetSessionScore(), (scoreResponse) =>
                {
                    RefreshAllEntriesInOrder(() => SetForSuccess());
                }, SetForError);
            }, SetForError);
        }

        /// <summary>
        /// Refreshes all entries in the leaderboard in order.
        /// </summary>
        /// <param name="onSuccess">Callback to invoke when the refreshing is successful.</param>
        public void RefreshAllEntriesInOrder(Action onSuccess = null)
        {
            // Refresh other entries first, then refresh session entry, and finally call success
            RefreshOtherEntries(() => RefreshSessionEntry(onSuccess));
        }

        /// <summary>
        /// Called when name input field is modified to update the submit button.
        /// </summary>
        public void UpdateButtonState()
        {
            _submitButton.interactable = _nameField.text != "" && !string.IsNullOrWhiteSpace(_nameField.text);
        }

        private void RefreshOtherEntries(Action onSuccess = null)
        {
            if (TryGetFetchCount(out int count))
            {
                _leaderboard.GetScoreList(count, (listResponse) =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        _rankTexts[i].text = $"{listResponse.items[i].rank}.";
                        _nameTexts[i].text = $"{listResponse.items[i].player.name}";
                        _scoreTexts[i].text = $"{listResponse.items[i].score}";
                    }
                    onSuccess?.Invoke();
                }, SetForError);
            }
        }
        private void RefreshSessionEntry(Action onSuccess = null)
        {
            _leaderboard.GetMemberRank((rankResponse) =>
            {
                // Means player is not on the leaderboard yet, it is not an big problem so we just do this
                if (rankResponse.player == null)
                {
                    _sessionRankText.text = string.Empty;
                    _sessionNameText.text = string.Empty;
                    _sessionScoreText.text = string.Empty;
                }
                else
                {
                    _sessionRankText.text = $"{rankResponse.rank}.";
                    _sessionNameText.text = $"{rankResponse.player.name}";
                    _sessionScoreText.text = $"{rankResponse.score}";
                }

                onSuccess?.Invoke();
            }, SetForError);
        }

        private bool TryGetFetchCount(out int count)
        {
            if (_rankTexts.Length != _nameTexts.Length || _nameTexts.Length != _scoreTexts.Length)
            {
                Debug.LogError($"Leaderboard entry TMP arrays are not the same length, this should never happen.", this);
                count = 0;
                return false;
            }
            else
            {
                count = _rankTexts.Length;
                return true;
            }
        }

        private void SetForLoading()
        {
            _boardContent.SetActive(false);
            _infoTMP.text = _defaultLoadingText;
        }
        private void SetForError(LootLockerResponse response)
        {
            Debug.LogError($"Something went wrong: {response.Error}", this);

            _boardContent.SetActive(false);
            _infoTMP.text = string.Format(_defaultFailureText, response.Error);
        }
        private void SetForSuccess()
        {
            _boardContent.SetActive(true);
            _infoTMP.text = _defaultSuccessText;
        }
    }
}
