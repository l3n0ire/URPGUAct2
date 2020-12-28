using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour
{
    // text that shows up in menu
    [ContextMenu("Convert to regular mesh")]
    void Convert()
    {
        // replace skinnedMeshrenderer with MeshRenderer and MeshFilter
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
        meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;

        // destroy SkinnedMeshRender and this script since we no longer need them
        DestroyImmediate(skinnedMeshRenderer);
        DestroyImmediate(this);
    }
}
