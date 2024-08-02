using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject tile_prefab,
        protagonist_prefab;

    [Header("Objects")]
    public GameObject parent_object,
        prot_obj;

    private GameObject[,] board;

    [Header("Size")]
    public int rows,
        cols;

    // Start is called before the first frame update
    void Start()
    {
        if (parent_object.transform.childCount == 0)
        {
            CreateBoard();
        }
        InitialPosCharacther();

    }

    public void CreateBoard()
    {
        RectTransform prefab_rect = tile_prefab.GetComponent<RectTransform>();
        float tile_width = prefab_rect.rect.width;
        float tile_height = prefab_rect.rect.height;

        float board_width = cols * tile_width;
        float board_height = rows * tile_height;

        board = new GameObject[rows, cols]; // Initialize the board array

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // Instantiate the tile prefab
                GameObject tile = Instantiate(tile_prefab);
                tile.name = $"X: {i}, Y: {j}";
                tile.transform.SetParent(parent_object.transform, false);

                // Set the tile's position
                RectTransform tile_rect = tile.GetComponent<RectTransform>();
                float xPos = j * tile_width - board_width / 2 + tile_width / 2;
                float yPos = -i * tile_height + board_height / 2 - tile_height / 2;
                tile_rect.localPosition = new Vector2(xPos, yPos);

                // Store the tile in the board array
                TileInteraction script = tile.GetComponent<TileInteraction>();
                script.coordinates = (i,j);
                board[i, j] = tile;

                // Add Event Triggers
                AddEventTrigger(
                    tile,
                    EventTriggerType.PointerClick,
                    (eventData) => OnGameObjectClicked(tile)
                );
                AddEventTrigger(
                    tile,
                    EventTriggerType.PointerEnter,
                    (eventData) => OnPointerEnter(tile)
                );
                AddEventTrigger(
                    tile,
                    EventTriggerType.PointerExit,
                    (eventData) => OnPointerExit(tile)
                );
            }
        }
    }



    public void InitialPosCharacther()
    {
        //x 8 y 4
        if (prot_obj == null)
        {
            prot_obj = Instantiate(protagonist_prefab);
        }

        prot_obj.transform.SetParent(board[8, 4].transform, false);
        prot_obj.transform.localPosition = new Vector2(0, 0);


    }

    private void AddEventTrigger(
        GameObject obj,
        EventTriggerType eventType,
        UnityAction<BaseEventData> action
    )
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    void OnGameObjectClicked(GameObject obj)
    {
        Debug.Log("Clicked " + obj.name);
        (int, int) target_coordinates =  obj.GetComponent<TileInteraction>().coordinates;
        (int, int) current_coordinates =  obj.GetComponent<TileInteraction>().coordinates;

        
    }


    void OnPointerEnter(GameObject obj)
    {
        var script = obj.GetComponent<TileInteraction>();
        (int, int) coordinates = (script.x, script.y);

        if (script != null)
        {
            Color new_color = script.hoverColor;
            script.SetColor(new_color);
        }
        else
        {
            Debug.LogError($"TileInteraction script missing on {obj.name}");
        }
    }

    void OnPointerExit(GameObject obj)
    {
        var script = obj.GetComponent<TileInteraction>();
        if (script != null)
        {
            Color new_color = script.originalColor;
            script.SetColor(new_color);
        }
        else
        {
            Debug.LogError($"TileInteraction script missing on {obj.name}");
        }
    }
}
