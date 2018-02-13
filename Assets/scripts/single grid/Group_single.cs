using UnityEngine;

public class Group_single : MonoBehaviour {

    // Time since last gravity tick
    float lastFall = 0;
    public bool hitFloor=false;
    public bool block1, block2;
    public bool isPreviewBlock1, isPreviewBlock2;
    public bool block1Floor, block2Floor;
    public float handLeftX, handRightX, initHandLeftX, initHandRightX;
    public float handLeftY, handRightY, initHandLeftY, initHandRightY;
    private float nextUpdate = .3f;
    private float previousUpdate = 2f;
    //KinectManager km;
    bool init = false;
    int counter = 0;
   

    //public PreviewSpawner preview; // Reference to the preview spawner.


    // Use this for initialization
    void Start () {

        initHandLeftX = (FindObjectOfType<KinectManager>().leftHandPosX * 10) - 3;
        initHandRightX = (FindObjectOfType<KinectManager>().rightHandPosX * 10) + 3;
        initHandLeftY = (FindObjectOfType<KinectManager>().leftHandPosY * 10) + 2;
        initHandRightY = (FindObjectOfType<KinectManager>().rightHandPosY * 10) + 2;


        // Default position not valid? Then it's game over
        if (!isValidGridPos())
            {
                Debug.Log("GAME OVER");
                Destroy(gameObject);
            }
            
        }

    // Update is called once per frame
    void Update()
    {
        

        


            Debug.Log("***************if (Time.time >= nextUpdate)");

            // Change the next update (current second+1)
            //********************************************

            //nextUpdate = Mathf.FloorToInt(Time.time) + .5f;
            nextUpdate = Time.time + .3f;
            //^^^^^^^^^^^^^^^^^^^^^^^^^commented out

            block1Floor = FindObjectOfType<Spawner_single>().block1HitFloor;
            block2Floor = FindObjectOfType<Spawner_single>().block2HitFloor;

            handLeftX = (FindObjectOfType<KinectManager>().leftHandPosX * 10);
            handRightX = (FindObjectOfType<KinectManager>().rightHandPosX * 10);

            handLeftY = (FindObjectOfType<KinectManager>().leftHandPosY * 10);
            handRightY = (FindObjectOfType<KinectManager>().rightHandPosY * 10);

            



            //BLOCK 2 CONTROLS
            /////////////////////////////////////////////
            if (block2 == true)
            {
                // Move Left
                if (Input.GetKeyDown(KeyCode.A))
                {
                    // Modify position
                    transform.position += new Vector3(-1, 0, 0);

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        updateGrid();
                    else
                        // It's not valid. revert.
                        transform.position += new Vector3(1, 0, 0);
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    // Modify position
                    transform.position += new Vector3(1, 0, 0);

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        updateGrid();
                    else
                        // It's not valid. revert.
                        transform.position += new Vector3(-1, 0, 0);
                }

                else if (Input.GetKeyDown(KeyCode.W))
                {
                    //if it is square blo
                    bool dontRotate = FindObjectOfType<Spawner_single>().isSquare;
                    //Debug.Log("bool dontRotate = FindObjectOfType<Spawner>().isSquare; " + dontRotate + "\n");

                    if (dontRotate == false) { transform.Rotate(0, 0, -90); }

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        updateGrid();
                    else
                        // It's not valid. revert.
                        transform.Rotate(0, 0, 90);
                }

                // Move Downwards and Fall
                else if (Input.GetKeyDown(KeyCode.S) ||
                         Time.time - lastFall >= 1)
                {

                    // Modify position
                    transform.position += new Vector3(0, -1, 0);

                    // See if valid
                    if (isValidGridPos())
                    {
                        // It's valid. Update grid.
                        updateGrid();
                    }
                    else
                    {
                        // It's not valid. revert.
                        transform.position += new Vector3(0, 1, 0);

                        // Clear filled horizontal lines

                        Grid.deleteFullRows();

                        //Debug.Log("updateScore() + updateUI()\n");
                        //Debug.Log("score : " + FindObjectOfType<updateScore>().currentScore + "\n");

                        // Spawn next Group
                        if (!hitFloor)
                        {
                            int next1 = FindObjectOfType<nextBlock_single>().nextBlock1;
                            int next2 = FindObjectOfType<nextBlock_single>().nextBlock2;

                            FindObjectOfType<Spawner_single>().isSquare = false;
                            isPreviewBlock1 = false;

                            // GameObject.Find("nextBlock").BroadcastMessage("PreviewNextSpawn");

                            FindObjectOfType<Spawner_single>().setBlock1(true);
                            block1Floor = true;
                            hitFloor = true;

                            Debug.Log("block1HitFloor  = " + block1Floor + "\n");
                            Debug.Log("block2HitFloor  = " + block2Floor + "\n");


                            Debug.Log("if (block1Floor && block2Floor)\n");
                            FindObjectOfType<Spawner_single>().spawnBlock1(next1);

                            FindObjectOfType<Spawner_single>().setBlock1(false);

                            FindObjectOfType<updateScore_single>().updateCurrentScore();
                            FindObjectOfType<updateScore_single>().UpdateUI();

                        }

                        // Disable script
                        enabled = false;
                        Debug.Log("enabled = " + enabled + "\n");
                    }

                    lastFall = Time.time;
                }
            }

            //BLOCK1 CONTROLS
            ///////////////////////////////////////////////////////////
            else if (block1 == true)
            {
                // Move Left
                if (Input.GetKeyDown(KeyCode.LeftArrow) )
                {
                    // Modify position
                    transform.position += new Vector3(-1, 0, 0);

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        updateGrid();
                    else
                        // It's not valid. revert.
                        transform.position += new Vector3(1, 0, 0);
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow) )
                {
                    // Modify position
                    transform.position += new Vector3(1, 0, 0);

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        updateGrid();
                    else
                        // It's not valid. revert.
                        transform.position += new Vector3(-1, 0, 0);
                }



                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    //if it is square blo
                    bool dontRotate = FindObjectOfType<Spawner_single>().isSquare;
                    //Debug.Log("bool dontRotate = FindObjectOfType<Spawner>().isSquare; " + dontRotate + "\n");

                    if (dontRotate == false) { transform.Rotate(0, 0, -90); }

                    // See if valid
                    if (isValidGridPos())
                        // It's valid. Update grid.
                        updateGrid();
                    else
                        // It's not valid. revert.
                        transform.Rotate(0, 0, 90);
                }
            }


        //always falling
        // Move Downwards and Fall
        if (block1 || block2)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) ||
                     Time.time - lastFall >= 1)
            {

                // Modify position
                transform.position += new Vector3(0, -1, 0);


                // See if valid
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Grid_single.deleteFullRows();

                    //Debug.Log("updateScore() + updateUI()\n");
                    //Debug.Log("score : " + FindObjectOfType<updateScore>().currentScore + "\n");
                    // Spawn next Group


                    if (!hitFloor)
                    {
                        //reset disable rotation
                        FindObjectOfType<Spawner_single>().isSquare = false;

                        //get next block to spawn
                        int next1 = FindObjectOfType<nextBlock_single>().nextBlock1;
                        int next2 = FindObjectOfType<nextBlock_single>().nextBlock2;
                        //reset to bool
                        isPreviewBlock2 = false;

                        // GameObject.Find("nextBlock").BroadcastMessage("PreviewNextSpawn");
                        FindObjectOfType<Spawner_single>().setBlock2(true);
                        block2Floor = true;
                        hitFloor = true;

                        Debug.Log("block1HitFloor  = " + block2Floor + "\n");
                        Debug.Log("block2HitFloor  = " + block2Floor + "\n");
                        //spawn next block

                        Debug.Log("if (block1Floor && block2Floor)\n");
                        FindObjectOfType<Spawner_single>().spawnBlock1(next1);
                        FindObjectOfType<Spawner_single>().setBlock1(false);


                        //update score
                        FindObjectOfType<updateScore_single>().updateCurrentScore();
                        FindObjectOfType<updateScore_single>().UpdateUI();
                    }

                    // Disable script
                    enabled = false;
                    Debug.Log("enabled = " + enabled + "\n");
                }

                lastFall = Time.time;
            }
        }
    }



    public bool isValidGridPos()
    {

        //Debug.Log("isPreviewBlock1 : " + isPreviewBlock1 + "\n");
        //Debug.Log("isPreviewBlock2 : " + isPreviewBlock2 + "\n");


        if (isPreviewBlock1 || isPreviewBlock2)
        {
            Debug.Log("public bool isValidGridPos()\n");
            Debug.Log("     if (isPreviewBlock1 || isPreviewBlock2)\n");
            enabled = false;
            return true;
        }
        else { 

        foreach (Transform child in transform)
        {

            Vector2 v = Grid.roundVec2(child.position);
            // Not inside Border?
            if (!Grid_single.insideBorder(v)) { return false; }

            if (Grid_single.grid1[(int)v.x, (int)v.y] != null &&
                Grid_single.grid1[(int)v.x, (int)v.y].parent != transform) { return false; }

        }
        return true;
    }

    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid_single.g1h; ++y)
            for (int x = 0; x < Grid_single.g1w; ++x)
                if (Grid_single.grid1[x, y] != null)
                    if (Grid_single.grid1[x, y].parent == transform)
                        Grid_single.grid1[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid_single.roundVec2(child.position);
            Grid_single.grid1[(int)v.x, (int)v.y] = child;
        }
    }
}
