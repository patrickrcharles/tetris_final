using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class Spawner_single : MonoBehaviour {

    //groups
    public GameObject[] groups;
    public bool isSquare = false; // used to diasble rotation of square block
    public Transform pos1,  pos1Next;
    int counter=0;
    public GameObject b1,b2, p1,p2;
    public bool block1HitFloor = false, block2HitFloor=false;

    public float start;
    


    //Transform NextBlock;
    //PreviewEvent nextSpawn;

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

        FindObjectOfType<block_single>().isBlock1 = true; 
        FindObjectOfType<nextBlock_single>().nextBlock1 = random;
        FindObjectOfType<Group_single>().block1 = true;
        spawnPreviewBlock1(random);

    }

    public void spawnPreviewBlock1(int blockNumber)
    {
        Destroy(p1);
        p1 = Instantiate(groups[blockNumber], pos1Next.position, Quaternion.identity);
        //FindObjectOfType<nextBlock>().isNextBlock1 = true;
        FindObjectOfType<block_single>().isNext1 = true;
        FindObjectOfType<block_single>().isBlock1 = false;
        FindObjectOfType<Group_single>().block1 = false;
        FindObjectOfType<Group_single>().isPreviewBlock1 = true;
        this.tag = "next";
        Debug.Log("public void spawnPreviewBlock1(int blockNumber)\n");
    }
    

    void PreviewNextSpawn()
    {
        // Send current preview to real spawner.

        // Get random int for next possible spawn.

    }

    void Start()
    {
        start = Time.time + 3f;

       
        
            FindObjectOfType<updateScore_single>().updateCurrentScore();
            FindObjectOfType<updateScore_single>().UpdateUI();

            //get initial left and right hand positions


            counter = 0;
            int b1 = Random.Range(0, groups.Length);
            //int b2 = Random.Range(0, groups.Length);
            //int b1 = 0;
            //int b2 = 0;

            /*
            if (nextSpawn == null)
            {
                nextSpawn = new PreviewEvent();
            }
            nextSpawn.AddListener(PreviewNextSpawn);
            */

            // Spawn initial Group
            if (counter < 1)
            {
                spawnBlock1(b1);
                counter++;
            }
        
    }
}
