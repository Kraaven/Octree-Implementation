using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateField : MonoBehaviour
{
    public Node TileNodePrefab;
    [SerializeField] private float fieldsize;

    public float MinNodeSize;

    // Start is called before the first frame update
    void Start()
    {
        if (fieldsize < MinNodeSize && MinNodeSize != 0)
        {
            this.enabled = false;
        }
        
        var Root = Instantiate(TileNodePrefab);
        Root.InitiateNode(fieldsize);
        Root.SplitNode();
        
    }

    IEnumerator Cycle()
    {
        Debug.Log("Cycled");
        DeleteAllChildren(transform);
        var Root = Instantiate(TileNodePrefab);
        Root.InitiateNode(fieldsize);
        Root.SplitNode();

        yield return new WaitForSeconds(3);
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
