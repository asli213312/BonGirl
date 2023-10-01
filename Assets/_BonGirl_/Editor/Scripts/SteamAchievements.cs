using Steamworks;
using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class SteamAchievements : MonoBehaviour
    {
        [SerializeField] private string[] achievements;
        
        private bool _isStatsReceived;
        private bool _isAchievementsCleared;
        private bool _isAchievementsStatusUpdated;
        private bool _isStatsStored;

        private string _achievementName = "Girl_1";

        private void Start()
        {
            RequestStats();
            ClearAchievement(_achievementName);
            SetAchievement(_achievementName);
        }

        private void RequestStats()
        {
            _isStatsReceived = Steamworks.SteamUserStats.RequestCurrentStats();
            
            Debug.Log("Is stat received: " + _isStatsReceived);
        }

        private void ClearAchievement(string achievementName)
        {
            _isAchievementsCleared = Steamworks.SteamUserStats.ClearAchievement(achievementName);

            Debug.Log("Is achievement cleared: " + _isAchievementsCleared);
            
            StoreStats();
        }

        private void SetAchievement(string achievementName)
        {
            _isAchievementsStatusUpdated = Steamworks.SteamUserStats.SetAchievement(achievementName);
                
            Debug.Log($"Is achievement {achievementName} status updated: {_isAchievementsStatusUpdated}");
            
            StoreStats();
        }

        private void StoreStats()
        {
            _isStatsStored = Steamworks.SteamUserStats.StoreStats();

            Debug.Log("Is stats stored: " + _isStatsStored);
        }
    }
}