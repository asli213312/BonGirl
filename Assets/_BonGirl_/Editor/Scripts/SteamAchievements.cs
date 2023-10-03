using Steamworks;
using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class SteamAchievements : MonoBehaviour
    {
        private bool _isStatsReceived;
        private bool _isAchievementsCleared;
        private bool _isAchievementsStatusUpdated;
        private bool _isStatsStored;

        public void GainAchievement(string achievementName)
        {
            RequestStats();

            ClearAchievement(achievementName);
            SetAchievement(achievementName);
        }

        public void RequestStats()
        {
            _isStatsReceived = Steamworks.SteamUserStats.RequestCurrentStats();
            
            Debug.Log("Is stat received: " + _isStatsReceived);
        }

        public void ClearAchievement(string achievementName)
        {
            _isAchievementsCleared = Steamworks.SteamUserStats.ClearAchievement(achievementName);

            Debug.Log("Is achievement cleared: " + _isAchievementsCleared);
            
            StoreStats();
        }

        public void SetAchievement(string achievementName)
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