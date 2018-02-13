using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreviewEvent : UnityEvent
{
}

public class Spawner : MonoBehaviour {

    //groups
    public GameObject[] groups;
    public bool isSquare = false; // used to diasble rotation of square block
    public Transform pos1, pos2, pos1Next, pos2Next;
    int counter=0;
    public GameObject b1, b2, p1, p2;
    public bool block1HitFloor = false, block2HitFloor=false;
    

    //Transform NextBlock;
    PreviewEvent nextSpawn;

    public void setBlock1(bool state)
    {
        block1HitFloor = state;
    }
    public void setBlock2(bool state)
    {
        block2HitFloor = state;
    }

    public void spawnBlock1(int blockNumber)
    {
        int random;
        random = Random.Range(0, groups.Length);
        //random = 0;
        if (blockNumber == 6)
            { isSquare = true; } //square block

        b1 = Instantiate(groups[blockNumber], pos1.position, Quaternion.identity);
        Debug.Log("Instantiate(groups[blockNumber], pos1.position, Quaternion.identity);\n");

        FindObjectOfType<block>().isBlock1 = true; 
        FindObjectOfType<nextBlock>().nextBlock1 = random;
        FindObjectOfType<Group>().block1 = true;
        spawnPreviewBlock1(random);

    }

    public void spawnPreviewBlock1(int blockNumber)
    {
        Destroy(p1);
        p1 = Instantiate(groups[blockNumber], pos1Next.position, Quaternion.identity);
        //FindObjectOfType<nextBlock>().isNextBlock1 = true;
        FindObjectOfType<block>().isNext1 = true;
        FindObjectOfType<block>().isBlock1 = false;
        FindObjectOfType<Group>().block1 = false;
        FindObjectOfType<Group>().isPreviewBlock1 = true;
        this.tag = "next";
        Debug.Log("public void spawnPreviewBlock1(int blockNumber)\n");
    }

    public void spawnPreviewBlock2(int blockNumber)
    {
        Destroy(p2);
        p2 = Instantiate(groups[blockNumber], pos2Next.position, Quaternion.identity);
        FindObjectOfType<block>().isNext2 = true;
        FindObjectOfType<block>().isBlock2 = false;
        FindObjectOfType<Group>().block2 = false;
        FindObjectOfType<Group>().isPreviewBlock2 = true;


        //FindObjectOfType<nextBlock>().isNextBlock2 = false;
        Debug.Log("public void spawnPreviewBlock2(int blockNumber)\n");
    }

    public void spawnBlock2(int blockNumber)
    {
        int random;
        random = Random.Range(0, groups.Length);
        //random = 0;

        if (blockNumber == 6)
        { isSquare = true; } //square block

        b2 = Instantiate(groups[blockNumber], pos2.position, Quaternion.identity);
        Debug.Log("Instantiate(groups[blockNumber], pos2.position, Quaternion.identity);\n");

        FindObjectOfType<block>().isBlock2 = true;
        FindObjectOfType<nextBlock>().nextBlock2 = random;
        FindObjectOfType<Group>().block2 = true;

        spawnPreviewBlock2(random);

    }


    void Start()
    {
        
        
            FindObjectOfType<updateScore>().updateCurrentScore();
            FindObjectOfType<updateScore>().UpdateUI();

            counter = 0;
            int b1 = Random.Range(0, groups.Length);
            int b2 = Random.Range(0, groups.Length);
            //int b1 = 0;
            //int b2 = 0;



            // Spawn initial Group
            if (counter < 1 )
            {
                spawnBlock1(b1);
                spawnBlock2(b2);
                counter++;
            }
        }
    
}
