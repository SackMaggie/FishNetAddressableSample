using FishNet;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SampleUI : MonoBehaviour
{
    public Button button;

    public void Start()
    {
        button.onClick.AddListener(OnClickCrateCube);
    }

    private void OnClickCrateCube()
    {
        if (!InstanceFinder.IsClientStarted)
            throw new Exception("Make sure client is start and connected");
        if (InstanceFinder.TryGetInstance<SampleGameServer>(out SampleGameServer gameServer))
            gameServer.ServerRpcCreateCube();
        else
            throw new Exception($"{nameof(SampleGameServer)} not found, is it loading ?");
    }
}