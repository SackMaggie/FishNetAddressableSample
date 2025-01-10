using FishNet.Addressable.Runtime;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SampleNetworkObject : NetworkBehaviour
{
    public new Renderer renderer;
    public readonly SyncVar<Color> synccolor = new SyncVar<Color>();

    protected override void OnValidate()
    {
        base.OnValidate();
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        synccolor.OnChange += Synccolor_OnChange;
    }

    private void Synccolor_OnChange(Color prev, Color next, bool asServer)
    {
        if (asServer)
            return;
        foreach (Material item in renderer.materials)
        {
            item.color = next;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        synccolor.Value = CreateRandomColor();
    }

    private Color CreateRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, 1);
    }

    public void Update()
    {
        if (IsServerStarted)
        {
            if (this.transform.position.y < -1000)
                Despawn();
        }
    }
}

[Serializable]
public class SampleNetworkObjectAssetRef : NetworkedComponentReference<SampleNetworkObject>
{
    public SampleNetworkObjectAssetRef(string guid) : base(guid)
    {
    }
}