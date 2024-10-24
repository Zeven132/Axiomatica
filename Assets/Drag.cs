using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3; // bugfix credit: Ardenian     https://forum.unity.com/threads/ambiguous-reference-between-two-references.885529/
public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public GameManager gameManager;
    public Image Image;
    public Vector3 startPosition;
    public double Value; // assigned value to change internal slot values
    public int SymbolType;
    public int EqType;
    public bool CanMerge;
    public Drop SymbolParent;
    public int TutorialNumber;
    public double tempDragStorageStorage; //dont laugh at me
    public Drop currenteqtype;
    public GameObject InventorySlot;
    [HideInInspector] public Transform parentAfterDrag;

    public void  OnPointerEnter(PointerEventData eventData) // shift hotkey
         {  
            if (Input.GetKey("left shift") && InventorySlot.transform.childCount < 21 && Input.GetMouseButton(0) == false) //if left shift down and inventory not full
            {
                SymbolParent = GetComponentInParent<Drop>(); // get slot and set values to 0
                SymbolParent.IsEmpty();
                parentAfterDrag = transform.parent;
                transform.SetParent(InventorySlot.transform); // change parent of symbol to inventory
                transform.SetAsLastSibling();
            }
         }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag --------------------\\");
        SymbolParent = GetComponentInParent<Drop>();
        SymbolParent.IsNowEmpty = true; // sets previous parents values to 0
       
        parentAfterDrag = transform.parent; // removing parent and setting to layer ontop of everything
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
     
        gameManager.tempdragstorage = Value; //storing values
        gameManager.TutorialDesc = TutorialNumber;
        gameManager.TempMergeStorage = CanMerge;
        gameManager.TempEqStorage = EqType;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag --------------------/");
        if (SymbolParent.transform == parentAfterDrag.transform) // condition met if player drags into nothing
        {
            Debug.Log("!!! drag failed !!!");
            transform.SetParent(parentAfterDrag);
            Image.raycastTarget = true;
            SymbolParent.IsNowEmpty = false;
            Debug.Log("Empty = false");
            gameManager.slotpos[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.tempdragstorage;
            switch (SymbolType) // sets symbol types
            {
                case 1:
                    gameManager.addslot = SymbolParent.SlotObjN;
                    gameManager.addIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.addslot;
                    Debug.Log("addslot: " + gameManager.addslot);
                    break;

                case 2:
                    gameManager.subslot = SymbolParent.SlotObjN;
                    gameManager.subIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.subslot;
                    Debug.Log("subslot: " + gameManager.subslot);
                    break;

                case 3:
                    gameManager.multslot = SymbolParent.SlotObjN;
                    gameManager.multIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.multslot;
                    Debug.Log("multslot: " + gameManager.multslot);
                    break;

                case 4:
                    gameManager.divslot = SymbolParent.SlotObjN;
                    gameManager.divIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.divslot;
                    Debug.Log("divslot: " + gameManager.divslot);
                    break;

                case 5:
                    gameManager.pwrslot = SymbolParent.SlotObjN;
                    gameManager.pwrIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.pwrslot;
                    Debug.Log("pwrslot: " + gameManager.pwrslot);
                    break;

                case 6:
                    gameManager.lbrack = SymbolParent.SlotObjN;
                    gameManager.lbrackIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.lbrack;
                    Debug.Log("lbrack: " + gameManager.lbrack);
                    break;

                case 7:
                    gameManager.rbrack = SymbolParent.SlotObjN;
                    gameManager.rbrackIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.rbrack;
                    Debug.Log("rbrack: " + gameManager.rbrack);
                    break;

                case 8:
                    gameManager.rootstart = SymbolParent.SlotObjN;
                    gameManager.rootstartIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.rootstart;
                    Debug.Log("rootstart: " + gameManager.rootstart);
                    break;

                case 9:
                    gameManager.rootend = SymbolParent.SlotObjN;
                    gameManager.rootendIndex[gameManager.TempEqPosStorage, SymbolParent.SlotObjN] = gameManager.rootend;
                    Debug.Log("rootend: " + gameManager.rootend);
                    break;
                   
                default:
                    break;
            }
        }
        else
        {
            transform.SetParent(parentAfterDrag);
            Image.raycastTarget = true;
            SymbolParent.IsNowEmpty = false;
            Debug.Log("Empty = false");
        }
    }
   
    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
        .GetComponent<GameManager>();
        currenteqtype = GetComponent<Drop>();
        InventorySlot = GameObject.Find("Inventory Slot");
        Image = GetComponent<Image>();
        startPosition = transform.position;
    }
//https://www.youtube.com/watch?v=kWRyZ3hb1Vc
//credit
}