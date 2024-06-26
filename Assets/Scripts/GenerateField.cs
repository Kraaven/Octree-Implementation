using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateField : MonoBehaviour
{
    public Node TileNodePrefab;
    public float fieldsize;

    public float MinNodeSize;

    // Start is called before the first frame update
    void Start()
    {
        if (fieldsize < MinNodeSize && MinNodeSize != 0)
        {
            this.enabled = false;
        }
        
        RegenrateField();
        // StartCoroutine(Generate());

    }

    // private void FixedUpdate()
    // {
    //     RegenrateField();
    // }

    // IEnumerator Generate()
    // {
    //     while (true)
    //     {
    //         RegenrateField();
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }

    public void RegenrateField()
    {
        Debug.Log("Cycled");
        DeleteAllChildren(transform);
        var Root = Instantiate(TileNodePrefab);
        Root.InitiateNode(fieldsize);
    }
    
    void DeleteAllChildren(Transform parent)
    {
        
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
