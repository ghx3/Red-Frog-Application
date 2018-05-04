// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RedFrogs.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

        // Is Team Leader
        private const string isTeamLeaderKey = "is_team_leader";
        private static readonly bool isTeamLeaderDefault = false;

        // user name
        private const string VolunteerNameKey = "vol_name_key";
        private static readonly string VolunteerNameDafault = "default";

		#endregion


		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        public static bool isTeamLeader
        {
            get
            {
                return AppSettings.GetValueOrDefault(isTeamLeaderKey, isTeamLeaderDefault);
            } set
            {
                AppSettings.AddOrUpdateValue(isTeamLeaderKey, value);
            }
        }

        public static string VolunteerName
        {
            get
            {
                return AppSettings.GetValueOrDefault(VolunteerNameKey, VolunteerNameDafault);
            } set
            {
                AppSettings.AddOrUpdateValue(VolunteerNameKey, value);
            }
        }

	}
}