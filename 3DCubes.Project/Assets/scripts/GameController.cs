using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    private CubePos nowCube = new CubePos(0, 1, 0);
       public float CubeChangePlaceSpeed = 0.5f;
    private float camMoveToYPosition,camMoveSpeed=2f;
    public Text scoreTxt;
    public Transform cubeToPlace;
    public Material[] Materials;
    public GameObject  allCubes,vfx;
    public GameObject[] cubesToCreate; 
    public List<Material> posibleMaterials=new List<Material>();
    public List<GameObject> posibleCubesToCreate=new List<GameObject>();

    public GameObject[] canvasStartPage;
    private Rigidbody allCubesRb;
    private bool firstCube;
    private int ID;
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
        if(PlayerPrefs.GetInt("score")<2)
        {}
        else if (PlayerPrefs.GetInt("score")<3)
        AddPossibleCubes(1);
         else if (PlayerPrefs.GetInt("score")<5)
        AddPossibleCubes(2);
         else if (PlayerPrefs.GetInt("score")<7)
        AddPossibleCubes(3);
         else if (PlayerPrefs.GetInt("score")<11)
        AddPossibleCubes(4);
         else if (PlayerPrefs.GetInt("score")<13)
        AddPossibleCubes(5);
         else if (PlayerPrefs.GetInt("score")<17)
        AddPossibleCubes(6);
         else if (PlayerPrefs.GetInt("score")<19)
        AddPossibleCubes(7);

        scoreTxt.text="<size=15><color=#C31414>Best score:</color></size>"+ PlayerPrefs.GetInt("score")+"<size=15><color=#1BB70F>Now:</color></size>";
      
        toCameraColor = Camera.main.backgroundColor;
     mainCam = Camera.main.transform;
        camMoveToYPosition = 4.9f + nowCube.y;
        
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
                cubesToCreate[ID],
                cubeToPlace.position,
                Quaternion.identity) as GameObject;
                
            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            AllCubesPositions.Add(nowCube.getVector());
camMoveToYPosition = 4.9f + nowCube.y;

            GameObject newVfx =  Instantiate(vfx,cubeToPlace.position,Quaternion.identity )as GameObject;
            Destroy(newVfx,1.5f);
            allCubesRb.isKinematic = true;
            allCubesRb.isKinematic = false;
            if(PlayerPrefs.GetString("music")=="Yes")
        GetComponent<AudioSource>().Play();
            SpawnPositions();
           MoveCameraChangeBg();
           StopCoroutine(showCubePlace);
           showCubePlace=StartCoroutine(ShowCubePlace());
        }

        if(!isLose && allCubesRb.velocity.magnitude>0.15f)
        {
            Destroy(cubeToPlace.gameObject);
            isLose = true;
            StopCoroutine(showCubePlace);
        }
       
       mainCam.localPosition =  Vector3.MoveTowards(mainCam.localPosition,
          new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z),
         camMoveSpeed * Time.deltaTime);
       if (Camera.main.backgroundColor != toCameraColor) 
       Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, toCameraColor, Time.deltaTime / 1.5f);
    }

    private void MoveCameraChangeBg()
    {
        Transform mainCam = Camera.main.transform;
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

      if(PlayerPrefs.GetInt("score")<maxY)
      PlayerPrefs.SetInt("score",maxY);

      scoreTxt.text="<size=15><color=#C31414>Best score:</color></size>"+ PlayerPrefs.GetInt("score")+"<size=15><color=#1BB70F>Now:</color></size>"+Convert.ToString(maxY);
      
        maxHor = maxX > maxZ ? maxX : maxZ;
       if(maxHor%3==0 && prevCountMaxHorizontal != maxHor ){
            mainCam.localPosition += new Vector3(2f, 0, 2.5f);
            prevCountMaxHorizontal = maxHor;
            }

       if (maxY >= 5)
       {
           
toCameraColor = bgColors[1];
       }
            if (maxY >= 3){
                toCameraColor = bgColors[0];
            }
            
            if (maxY >= 7){
                toCameraColor = bgColors[2];
            }
        
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
            ID=UnityEngine.Random.Range(0,posibleCubesToCreate.Count);
            cubeToPlace.GetComponent<MeshRenderer>().material=posibleMaterials[ID];

          //  Debug.Log(ID+" and "+posibleCubesToCreate.Count);
            
            
        }
    }
    private void AddPossibleCubes(int Num)
    {
    for(int i=1;i<=Num;i++){
    posibleMaterials.Add(Materials[i-1]);
    posibleCubesToCreate.Add(cubesToCreate[i-1]);
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
         //GetComponent<MeshRenderer>().material=posibleCubesToCreate[1].GetComponent<MeshRenderer>.material;


    }

}
