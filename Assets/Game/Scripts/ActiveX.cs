using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
public class ActiveX : MonoBehaviour
{
    public enum State
    {
        Unclicked,
        Clicked
    }
    public State state;

    [SerializeField] private Sprite TickSprite;                     // get sprite-Texture through Inspector
    [SerializeField] private Sprite UnTickSprite;                   // get sprite-Texture through Inspector

    public List<GameObject> clickedList;                            // currently clicked tile list. 
    public int indisX, indisY;                                      // my two dimensional array's index.

    private int count;
    private GridManager gridManager;
    private SpriteRenderer myTileSprite;
    private void OnEnable() => GameManager.OnButtonClicked += ControlNeighbours;
    private void OnDisable() => GameManager.OnButtonClicked -= ControlNeighbours;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        myTileSprite = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        ChangeSprite();
        state = State.Clicked;                          // if clicked on tile, state change
        GameManager.ClickButton();                      // after each click, control all forced neighbours
    }
   
    private void ChangeSprite()                         //if tile is unclicked, change the sprite
    {
        if (state==State.Unclicked)                     
            myTileSprite.sprite = TickSprite;
    }

    public void ControlNeighbours()             // check the concerned neighbors
    {
        if (state==State.Clicked)
        {
            if (indisX > 0 && gridManager.Tiles[indisX - 1, indisY].state == State.Clicked)
                clickedList.Add(gridManager.Tiles[indisX - 1, indisY].gameObject);                                      // if neighbour has clicked, add it to the clicked list

            if (indisY > 0 && gridManager.Tiles[indisX, indisY - 1].state == State.Clicked)
                clickedList.Add(gridManager.Tiles[indisX , indisY - 1].gameObject);                                     // if neighbour has clicked, add it to the clicked list

            if (indisY < gridManager._gridSize - 1 && gridManager.Tiles[indisX, indisY + 1].state == State.Clicked)
                clickedList.Add(gridManager.Tiles[indisX, indisY + 1].gameObject);                                      // if neighbour has clicked, add it to the clicked list

            if (indisX < (gridManager._gridSize-1) && gridManager.Tiles[indisX+1, indisY].state == State.Clicked)
                clickedList.Add(gridManager.Tiles[indisX+1, indisY ].gameObject);                                       // if neighbour has clicked, add it to the clicked list


            if (clickedList.Count >= 2)
                StartCoroutine(WaitRemoveX());                                                                          
            else
                clickedList.Clear();                                                                                    //if my clicked list count lower two, Clear the list.
        } 
    }

    WaitForSeconds duration = new WaitForSeconds(.05f);
    private IEnumerator WaitRemoveX()                                                       // if my clicked list count bigger two, wait a moment for it to appear on the screen
    {
        yield return duration;                                                                      
        myTileSprite.sprite = UnTickSprite;                                                 // replace with clean sprite
        state = State.Unclicked;                                                            // replace state Unclicked

        for (int i = 0; i < clickedList.Count; i++)                                         // all neighbors on the list,replace with clean sprite
        {
            clickedList[i].GetComponent<SpriteRenderer>().sprite = UnTickSprite;
            clickedList[i].GetComponent<ActiveX>().state = State.Unclicked;

        }
        clickedList.Clear();    
    }

}
