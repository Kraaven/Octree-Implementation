using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float NodeSize;
    public Node TilePrefab;
    public GenerateField Origin;
    public Node[] SubNodes;
    public bool generated;
    
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
            }

            SubNodes[Random.Range(0,7)].SplitNode();
            // 
        }
        else
        {
            Debug.Log("Reached Resolution Limit");
            var pointer = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),transform);
            pointer.name = "Pointer";
            pointer.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
