using FishNet;
using FishNet.Addressable.Runtime;
using FishNet.Connection;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SampleGameServer : NetworkBehaviour
{
    [FormerlySerializedAs("networkedCubeAssetRef")]public SampleNetworkObjectAssetRef sampleNetworkObject;

    public void Start()
    {
        InstanceFinder.RegisterInstance(this);
    }

    [ServerRpc(RequireOwnership = false)]
    [ContextMenu("ServerRpcCreateCube")]
    public void ServerRpcCreateCube(NetworkConnection networkConnection = null)
    {
        Debug.Log($"ServerRpcCreateCube {networkConnection}");
        sampleNetworkObject.SpawnAddressableAsync(networkConnection, networkConnection.Scenes.LastOrDefault(), null);
    }
}

[Serializable]
public class GameServerAssetRef : NetworkedComponentReference<SampleGameServer>
{
    public GameServerAssetRef(string guid) : base(guid)
    {
    }
}
