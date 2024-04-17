using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Node : MonoBehaviour
{
    public float NodeSize;
    public Node TilePrefab;
    public GenerateField Origin;
    public Node[] SubNodes;
    public bool generated;
    public bool Detected;
    
    public void InitiateNode(float size)
    {
        Origin = FindObjectOfType<GenerateField>();
        TilePrefab = Origin.TileNodePrefab;
        NodeSize = size;
        transform.localScale = Vector3.one * size;
        transform.parent = Origin.transform;
        
    }


    public void SplitNode()
    {
        Debug.Log(NodeSize/2);
        
        if (NodeSize/2 >= FindObjectOfType<GenerateField>().MinNodeSize)
        {
            SubNodes = new Node[8];

            float OffsetNumber = NodeSize / 4;
            Vector3 InitOffset = transform.position;

            SubNodes[0] = Instantiate(TilePrefab, new Vector3(OffsetNumber, OffsetNumber, OffsetNumber) + InitOffset, Quaternion.identity);
            SubNodes[1] = Instantiate(TilePrefab, new Vector3(OffsetNumber, OffsetNumber, -OffsetNumber)+ InitOffset, Quaternion.identity);
            SubNodes[2] = Instantiate(TilePrefab, new Vector3(OffsetNumber, -OffsetNumber, OffsetNumber)+ InitOffset, Quaternion.identity);
            SubNodes[3] = Instantiate(TilePrefab, new Vector3(OffsetNumber, -OffsetNumber, -OffsetNumber)+ InitOffset, Quaternion.identity);
            
            SubNodes[4] = Instantiate(TilePrefab, new Vector3(-OffsetNumber, OffsetNumber, OffsetNumber)+ InitOffset, Quaternion.identity);
            SubNodes[5] = Instantiate(TilePrefab, new Vector3(-OffsetNumber, OffsetNumber, -OffsetNumber)+ InitOffset, Quaternion.identity);
            SubNodes[6] = Instantiate(TilePrefab, new Vector3(-OffsetNumber, -OffsetNumber, OffsetNumber)+ InitOffset, Quaternion.identity);
            SubNodes[7] = Instantiate(TilePrefab, new Vector3(-OffsetNumber, -OffsetNumber, -OffsetNumber)+ InitOffset, Quaternion.identity);

            foreach (var Subnode in SubNodes)
            {
                //Subnode.transform.parent = transform;
                Subnode.InitiateNode(OffsetNumber*2);

                // if (!Subnode.Detected)
                // {
                //     Destroy(Subnode);
                // }
            }
            
            // 
        }
        else
        {
            Debug.Log("Reached Resolution Limit");
        }
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Detected = true;
        SplitNode();
    }
}
