﻿using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

using XamList.Mobile.Shared;

namespace XamList
{
    public static class ConnectivityService
    {
        public static void SubscribeEventHandlers() =>
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;

        public static void UnsubscribeEventHandlers() =>
            CrossConnectivity.Current.ConnectivityChanged -= HandleConnectivityChanged;

		static async void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
            var isRemoteDatabaseReachable = e.IsConnected 
                                             && await CrossConnectivity.Current.IsRemoteReachable(BackendConstants.AzureAPIUrl).ConfigureAwait(false);

            if (isRemoteDatabaseReachable)
                await DatabaseSyncService.SyncRemoteAndLocalDatabases().ConfigureAwait(false);
		}
    }
}
