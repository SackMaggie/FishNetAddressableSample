using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Transporting;
using UnityEngine.ResourceManagement.ResourceProviders;
using FishNet.Addressable.Runtime;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SampleFishnetNetworkStateHook : MonoBehaviour
{
    public FishNet.Managing.Server.ServerManager serverManager;

    public GameServerAssetRef gameServerAssetRef;
    private AsyncOperationHandle<SampleGameServer> gameServerHandle;


    // Start is called before the first frame update
    public void Start()
    {
        serverManager.OnRemoteConnectionState += ServerManager_OnRemoteConnectionState;
        serverManager.OnServerConnectionState += ServerManager_OnServerConnectionState;
    }

    public void OnDestroy()
    {
        if (serverManager != null)
        {
            serverManager.OnRemoteConnectionState -= ServerManager_OnRemoteConnectionState;
            serverManager.OnServerConnectionState -= ServerManager_OnServerConnectionState;
        }
    }

    private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs serverConnnectionState)
    {
        Debug.Log("Server Connection State: " + serverConnnectionState.ConnectionState);
        switch (serverConnnectionState.ConnectionState)
        {
            case LocalConnectionState.Stopped:
                if (gameServerHandle.IsValid())
                {
                    gameServerHandle.Release();
                }
                break;
            case LocalConnectionState.Stopping:
                break;
            case LocalConnectionState.Starting:
                break;
            case LocalConnectionState.Started:
                if (!gameServerHandle.IsValid())
                    gameServerHandle = serverManager.SpawnAddressableAsync(gameServerAssetRef);
                break;
            case LocalConnectionState.StoppedError:
                break;
            case LocalConnectionState.StoppedClosed:
                break;
            default:
                break;
        }
    }

    private void ServerManager_OnRemoteConnectionState(FishNet.Connection.NetworkConnection networkConnection, FishNet.Transporting.RemoteConnectionStateArgs remoteConnectionState)
    {
        Debug.Log($"ServerManager_OnRemoteConnectionState {networkConnection} {remoteConnectionState.ConnectionState}");
    }
}
