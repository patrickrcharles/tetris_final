using UnityEngine;

public class Group : MonoBehaviour {

    // Time since last gravity tick
    float lastFall = 0;
    public bool hitFloor=false;
    public bool block1, block2;
    public bool isPreviewBlock1, isPreviewBlock2;
    [SerializeField]
    public bool block1Floor, block2Floor;

    public float handLeftX, handRightX, initHandLeftX, initHandRightX;

    public float handLeftY, handRightY, initHandLeftY, initHandRightY;

    //private float previousUpdate = 3f;

    private float nextUpdate = .3f;
    int counter = 0;
    bool init = false;

    //public PreviewSpawner preview; // Reference to the preview spawner.


    // Use this for initialization
    void Start () {
        /*
        initHandLeftX = (FindObjectOfType<KinectManager>().leftHandPosX * 10) - 3;
        initHandRightX = (FindObjectOfType<KinectManager>().rightHandPosX * 10) + 3;
        initHandLeftY = (FindObjectOfType<KinectManager>().leftHandPosY * 10) + 3;
        initHandRightY = (FindObjectOfType<KinectManager>().rightHandPosY * 10) + 3;
        
        initHandLeftX = (FindObjectOfType<KinectManager>().leftHandInitialPosX * 10) - 3;
        initHandRightX = (FindObjectOfType<KinectManager>().rightHandInitialPosX * 10) + 3;
        initHandLeftY = (FindObjectOfType<KinectManager>().leftHandInitialPosY * 10) + 2;
        initHandRightY = (FindObjectOfType<KinectManager>().rightHandInitialPosY * 10) + 2;
        */

        // Default position not valid? Then it's game over
        if (!isValidGridPos())
            {
                Debug.Log("GAME OVER");
                Destroy(gameObject);
            }

        // Preview spawner get component call.
        //preview = GameObject.Find("nextBlock").GetComponent<PreviewSpawner>();
            
        }

    // Update is called once per frame
    void Update()
    {
        

            // Change the next update (current second+1)
            nextUpdate = Time.time + .3f;
            /*
            handLeftX = (FindObjectOfType<KinectManager>().leftHandPosX * 10);
            handRightX = (FindObjectOfType<KinectManager>().rightHandPosX * 10);

            handLeftY = (FindObjectOfType<KinectManager>().leftHandPosY * 10);
            handRightY = (FindObjectOfType<KinectManager>().rightHandPosY * 10);
*/
            block1Floor = FindObjectOfType<Spawner>().block1HitFloor;
            block2Floor = FindObjectOfType<Spawner>().block2HitFloor;
            

            if (block1 == true)
            {
                // Move Left
                if (Input.GetKeyDown(KeyCode.A) )
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
                else if (Input.GetKeyDown(KeyCode.D) )
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

                else if (Input.GetKeyDown(KeyCode.W) )
                {
                    //if it is square blo
                    bool dontRotate = FindObjectOfType<Spawner>().isSquare;
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


            //BLOCK2 CONTROLS
            else if (block2 == true)
            {
                // Move Left
                if (Input.GetKeyDown(KeyCode.LeftArrow))
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



                else if (Input.GetKeyDown(KeyCode.UpArrow) )
                {
                    //if it is square blo
                    bool dontRotate = FindObjectOfType<Spawner>().isSquare;
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

        if (block1 || block2)
        { 

        // Move Downwards and Fall
         if (Input.GetKeyDown(KeyCode.S) ||
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



                    //Debug.Log("updateScore() + updateUI()\n");
                    //Debug.Log("score : " + FindObjectOfType<updateScore>().currentScore + "\n");

                    // Spawn next Group
                    if (!hitFloor)
                    {
                        int next1 = FindObjectOfType<nextBlock>().nextBlock1;
                        int next2 = FindObjectOfType<nextBlock>().nextBlock2;

                        FindObjectOfType<Spawner>().isSquare = false;
                        isPreviewBlock1 = false;
                        isPreviewBlock2 = false;

                        // GameObject.Find("nextBlock").BroadcastMessage("PreviewNextSpawn");

                        if (block1)
                        {
                            FindObjectOfType<Spawner>().setBlock1(true);
                            block1Floor = true;
                            hitFloor = true;
                        }
                        if (block2)
                        {
                            FindObjectOfType<Spawner>().setBlock2(true);
                            block2Floor = true;
                            hitFloor = true;
                        }

                        Debug.Log("block1HitFloor  = " + block1Floor + "\n");
                        Debug.Log("block2HitFloor  = " + block2Floor + "\n");

                        if (block1Floor && block2Floor)
                        {
                            Grid.deleteFullRows();
                            Debug.Log("if (block1Floor && block2Floor)\n");
                            FindObjectOfType<Spawner>().spawnBlock1(next1);
                            FindObjectOfType<Spawner>().spawnBlock2(next2);

                            FindObjectOfType<Spawner>().setBlock1(false);
                            FindObjectOfType<Spawner>().setBlock2(false);

                            Debug.Log("***********block1HitFloor  = " + block1Floor + "\n");
                            Debug.Log("***********block2HitFloor  = " + block2Floor + "\n");
                        }

                        FindObjectOfType<updateScore>().updateCurrentScore();
                        FindObjectOfType<updateScore>().UpdateUI();

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
            if (!Grid.insideBorder(v)) { return false; }

            if (Grid.grid1[(int)v.x, (int)v.y] != null &&
                Grid.grid1[(int)v.x, (int)v.y].parent != transform) { return false; }

        }
        return true;
    }

    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid.g1h; ++y)
            for (int x = 0; x < Grid.g1w; ++x)
                if (Grid.grid1[x, y] != null)
                    if (Grid.grid1[x, y].parent == transform)
                        Grid.grid1[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid1[(int)v.x, (int)v.y] = child;
        }
    }
}
