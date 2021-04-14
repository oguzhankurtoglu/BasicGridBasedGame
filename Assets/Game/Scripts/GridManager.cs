using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject Tile;           //  the prefab which will be instantiated
    [SerializeField] private float tileSize;            //  for space amount between tiles.
    [SerializeField] private InputField _inputText;     //  the number for grid size entry by user
    public ActiveX[,] Tiles;                            //  Array containing information for each grid cell
    public int _gridSize { get; private set; }                             
   
    private void Start()
    {
        _gridSize = int.Parse(_inputText.text);         //Convert to integer
        Tiles = new ActiveX[_gridSize, _gridSize];  
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < _gridSize; i++)
        {
            for (int j = 0; j < _gridSize; j++)
            {
                GameObject tile = Instantiate(Tile, transform);     // Create a new Tile
                Tiles[i, j] = tile.GetComponent<ActiveX>();         // Add the information of each tile into the Array.
                tile.name =  i.ToString()+" , "+j.ToString();       // Change tile's name for good naming.

                Tiles[i,j].indisX= i;                               //Enter the index information into the ActiveX script that holds the information of each tile.
                Tiles[i,j].indisY= j;

                float PosX = i * tileSize;                          
                float PosY = j * tileSize;
                tile.transform.position = new Vector2(PosX,PosY);    //set the position each tile Child object            
            }
        }
        float GridW = _gridSize * -tileSize;
        transform.position = new Vector2(GridW/2+tileSize/2, GridW / 2 + tileSize / 2); //set the position each tile parent object
    }


    private void Update()
    {
        CameraFit();//( in Update function for easy testing,Can be put to Start for performance)

    }
    private void CameraFit()//adjusting the camera distance to fit any size array on the screen 
    {
        if (Screen.width >= Screen.height)
        {

            Camera.main.orthographicSize = _gridSize * 2;
        }
        else
        {
            float width = Screen.width;
            float height = Screen.height;
            Camera.main.orthographicSize = _gridSize * (height / width) * 2;  //to keep the vertical ratio
        }
    }
}
