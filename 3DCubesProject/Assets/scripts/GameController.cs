using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class GameController : MonoBehaviour
{
    private CubePos nowCube = new CubePos(0, 1, 0);
   // public float pi= 3.14159265358979323846f, Count =1f,blue=0,red=0,green=0;
    public float CubeChangePlaceSpeed = 0.5f;
    private float camMoveToYPosition=0,camMoveSpeed=2f;
    public Transform cubeToPlace;
    public GameObject cubeToCreate, allCubes;
    public GameObject[] canvasStartPage;
    private Rigidbody allCubesRb;
    private bool firstCube;
    private bool isLose=false;
    public Color[] bgColors;
    private Color toCameraColor;
    private int prevCountMaxHorizontal;
    private List<Vector3> AllCubesPositions = new List<Vector3>
        {
        new Vector3( 0, 0, 0 ),
        new Vector3( 1, 0, 0 ),
        new Vector3( 0, 0, 1 ),
        new Vector3( 1, 0, 1 ),
        new Vector3( 1,0,-1 ),
        new Vector3(-1, 0,1 ),
        new Vector3( -1, 0,0 ),
        new Vector3( 0, 0,-1 ),
        new Vector3( -1, 0,-1 ),
        new Vector3( 0, 1, 0 ),
        };
    private Transform mainCam;
    private Coroutine showCubePlace;
    private void Start()
                
    {
        toCameraColor = Camera.main.backgroundColor;
      mainCam = Camera.main.transform;
        camMoveToYPosition = 4.9f + nowCube.y;
        //  mainCam = Camera.main.transform;
        //camMoveToYPosition = 5.9f + nowCube.y - 1f;
        allCubesRb = allCubes.GetComponent<Rigidbody>();
        showCubePlace = StartCoroutine(ShowCubePlace());
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            && (cubeToPlace != null)
            &&  (!EventSystem.current.IsPointerOverGameObject()) )
        {
#if !UNITY_EDITOR
            if (Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            
            if (!firstCube) { 
            foreach (GameObject obj in canvasStartPage)
                Destroy(obj);
                }
           GameObject newCube = Instantiate(
                cubeToCreate,
                cubeToPlace.position,
                Quaternion.identity) as GameObject;
            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            AllCubesPositions.Add(nowCube.getVector());
            allCubesRb.isKinematic = true;
            allCubesRb.isKinematic = false;
            SpawnPositions();
           MoveCameraChangeBg()    ;
          
        }

        if(!isLose && allCubesRb.velocity.magnitude>0.1f)
        {
            Destroy(cubeToPlace.gameObject);
            isLose = true;
            StopCoroutine(showCubePlace);
        }
       
       mainCam.localPosition =  Vector3.MoveTowards(mainCam.localPosition,
          new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z),
         camMoveSpeed * Time.deltaTime);
   //     if (Camera.main.backgroundColor != toCameraColor) ;
   //     Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, toCameraColor, Time.deltaTime / 1.5f);
    }

    private void MoveCameraChangeBg()
    {
        int maxX = 0, maxY = 0, maxZ = 0, maxHor;
    foreach (Vector3 pos in AllCubesPositions)
        { 
      if(Mathf.Abs(Convert.ToInt32(pos.x))> maxX)
        maxX=Convert.ToInt32(pos.x);

        if(Mathf.Abs(Convert.ToInt32(pos.y))> maxY)
        maxY=Convert.ToInt32(pos.y); 

        if(Mathf.Abs(Convert.ToInt32(pos.z))> maxZ)
        maxZ=Convert.ToInt32(pos.z);
        }
      //  Transform mainCam = Camera.main.transform;
        camMoveToYPosition = 4.9f + nowCube.y;
        maxHor = maxX > maxZ ? maxX : maxZ;
        if (maxHor%3==0)
        {
            mainCam.localPosition -= new Vector3(0, 0, 2.5f);
            prevCountMaxHorizontal = maxHor;
        }
        // mainCam.localPosition = new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z);


        if (maxY >= 5)
            toCameraColor = bgColors[1];
       

       


    }


    struct CubePos
    {
        public int x, y, z;
        public CubePos(int x, int y, int z)
        { 
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3 getVector()
        {
            return new Vector3(x, y, z);
        }
        public void setVector(Vector3 pos)
        {
           
            x = Convert.ToInt32(pos.x);
            y = Convert.ToInt32(pos.y);
            z = Convert.ToInt32(pos.z);
        }

    }
    IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(CubeChangePlaceSpeed);
        }
    }

   



    


    private bool isPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
            return false;
        foreach (Vector3 pos in AllCubesPositions)
        
            if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
                return false;
        
        return true;
    }

  
    
       
    
    private void SpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        if (isPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z))
            && nowCube.x + 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
        if (isPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z))
            && nowCube.x - 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
        if (isPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1))
            && nowCube.z + 1 != cubeToPlace.position.z)
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
        if (isPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z))
            && nowCube.y + 1 != cubeToPlace.position.y)
            positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
        if (isPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z))
            && nowCube.y - 1 != cubeToPlace.position.y)
            positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
        if (isPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1))
            && nowCube.z - 1 != cubeToPlace.position.z)
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
        if (positions.Count > 1)    
            cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
        else if (positions.Count == 0)
            isLose = true;
        else
            cubeToPlace.position = positions[0];


    }

}
