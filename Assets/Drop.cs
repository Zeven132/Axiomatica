using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager gameManager;
    public int SlotObjN; //object Slot number
    public int EqSlotNum; //for equations
    public string Number;
    public bool eqSlotDetermine; //false if symbol slot; true if eq slot
    private int AssignEqSlot; //used to assign eq slots to symbols
    public bool IsNowEmpty = false;
    public bool CraftSlot;
    public int CraftSlotPos;
    public bool isInvSlot;
   
    // Symbol Gameobjects
    // 1-10
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject nine;
   
    // Operators/Exponents
    public GameObject multiplication;
    public GameObject leftBracket;
    public GameObject rightBracket;
    public GameObject subtraction;
    public GameObject division;
    public GameObject rootStart;
    public GameObject rootEnd;
    public GameObject exponentiation;
   
    public List<GameObject> OutputNumbers;
   
    public GameObject OutputSlot;
    public Transform OutputSlotPos;
   
    public GameObject CraftSlot1;
    public GameObject CraftSlot2;
   
    public GameObject CraftingGrid;
    public Transform CraftingGridTrans;
   
    public Transform CraftSlot1Trans; // ha
    public Transform CraftSlot2Trans;
    public bool stopInstantly = false;
   
    public void  OnPointerEnter(PointerEventData eventData) //if mouse enters an equation
    {  
        if (eqSlotDetermine == true && (Input.GetMouseButton(0) || Input.GetKey("left shift")))
        {
            AssignEqSlot = EqSlotNum;
            gameManager.TempEqPosStorage = AssignEqSlot;
            Debug.Log(gameManager.TempEqPosStorage);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eqSlotDetermine == true && Input.GetMouseButton(0) == false )
        {
            gameManager.TempEqPosStorage = 0;
            Debug.Log(gameManager.TempEqPosStorage);
        }
    }
         
    public void OnDrop(PointerEventData eventData) //on symbol dropped
    {
        if (eqSlotDetermine == true) {gameManager.eqIndex2[AssignEqSlot] = gameManager.TempEqStorage;}
        if (transform.childCount == 0 && gameManager.TempMergeStorage == false) //if slot empty
        {
            GameObject dropped = eventData.pointerDrag;
            Drag drag = dropped.GetComponent<Drag>();
            drag.parentAfterDrag = transform; // sets dragged symbol to be the child of slot gameobject
            if (eqSlotDetermine == false)// if symbol dropped
            {  
                if (CraftSlot == false)
                {
                    gameManager.slotpos[gameManager.TempEqPosStorage, SlotObjN] = gameManager.tempdragstorage; //changed slotpos internal val = assigned int to the slot obj
                    Debug.Log("assigned slotpos:                     " + gameManager.TempEqPosStorage + ", " + SlotObjN + " with value of " + gameManager.tempdragstorage);
                    switch (drag.SymbolType)
                    {
                        case 1:
                            gameManager.addslot = SlotObjN;
                            gameManager.addIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.addslot;
                            Debug.Log("addslot: " + gameManager.addslot);
                            break;

                        case 2:
                            gameManager.subslot = SlotObjN;
                            gameManager.subIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.subslot;
                            Debug.Log("subslot: " + gameManager.subslot);
                            break;

                        case 3:
                            gameManager.multslot = SlotObjN;
                            gameManager.multIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.multslot;
                            Debug.Log("multslot: " + gameManager.multslot);
                            break;

                        case 4:
                            gameManager.divslot = SlotObjN;
                            gameManager.divIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.divslot;
                            Debug.Log("divslot: " + gameManager.divslot);
                            break;

                        case 5:
                            gameManager.pwrslot = SlotObjN;
                            gameManager.pwrIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.pwrslot;
                            Debug.Log("pwrslot: " + gameManager.pwrslot);
                            break;

                        case 6:
                            gameManager.lbrack = SlotObjN;
                            gameManager.lbrackIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.lbrack;
                            Debug.Log("lbrack: " + gameManager.lbrack);
                            break;

                        case 7:
                            gameManager.rbrack = SlotObjN;
                            gameManager.rbrackIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rbrack;
                            Debug.Log("rbrack: " + gameManager.rbrack);
                            break;

                        case 8:
                            gameManager.rootstart = SlotObjN;
                            gameManager.rootstartIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rootstart;
                            Debug.Log("rootstart: " + gameManager.rootstart);
                            break;

                        case 9:
                            gameManager.rootend = SlotObjN;
                            gameManager.rootendIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rootend;
                            Debug.Log("rootend: " + gameManager.rootend);
                            break;
                        default:
                            break;
                    }
                }
                else //crafting possible
                {
                    Debug.Log("Craft slot: true");
                    if (gameManager.tempdragstorage != 0) // if number dropped
                    {gameManager.craftIndex[0, CraftSlotPos] = (int) gameManager.tempdragstorage;
                    }
                    else {gameManager.craftIndex[drag.SymbolType, CraftSlotPos] = drag.SymbolType;
                    }
                    Debug.Log("Craft slot: "+drag.SymbolType+", "+CraftSlotPos + " = " + gameManager.tempdragstorage +"\nval if > [0]:"+drag.SymbolType);
                }
            }
        }
        else if (isInvSlot == true)
        {
            GameObject dropped = eventData.pointerDrag;
            Drag drag = dropped.GetComponent<Drag>();
            drag.parentAfterDrag = transform;
        }
    gameManager.slotposUpd();
    }

    public void CraftCheck() // checks for valid crafting output once called
    {  
        if(CraftSlot1.transform.childCount == 1 && CraftSlot2.transform.childCount == 1) // if both slots have symbols in them
        {
            if (gameManager.craftIndex[0, 0] == gameManager.craftIndex[0, 1] && (gameManager.craftIndex[0, 0] > 0 && gameManager.craftIndex[0, 1] > 0)) // if both slots equal in numerical value and above 0
            {      
                switch(gameManager.craftIndex[0, 0]) // merge type recipies
                {  
                    case 1:
                        Instantiate(two, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 2:
                        Instantiate(three, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 3:
                        Instantiate(four, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 4:
                        Instantiate(five, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 5:
                        Instantiate(six, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 6:
                        Instantiate(seven, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 7:
                        Instantiate(eight, OutputSlotPos);
                        RemoveSymbol();
                        break;
                    case 8:
                        Instantiate(nine, OutputSlotPos);
                        RemoveSymbol();
                        break;
                }
            }
            else if((gameManager.craftIndex[0, 0] == 4 && gameManager.craftIndex[1, 1] == 1) || (gameManager.craftIndex[0, 1] == 4 && gameManager.craftIndex[1, 0] == 1)) // 4 | + = x (both orders)
            {
                Instantiate(multiplication, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[0, 0] == 9 && gameManager.craftIndex[3, 1] == 3) || (gameManager.craftIndex[0, 1] == 9 && gameManager.craftIndex[3, 0] == 3)) // 9 | x = - (both orders)
            {
                Instantiate(subtraction, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[2, 0] == 2 && gameManager.craftIndex[2, 1] == 2)) // - | - = /
            {
                Instantiate(division, OutputSlotPos);
                RemoveSymbol();  
            }
            else if ((gameManager.craftIndex[4, 0] == 4 && gameManager.craftIndex[3, 1] == 3)) // / | x = √
            {
                Instantiate(rootStart, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[3, 0] == 3 && gameManager.craftIndex[4, 1] == 4)) // x | / = ¬
            {
                Instantiate(rootEnd, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[8, 0] == 8 && gameManager.craftIndex[9, 1] == 9)) // √ | ¬ = (
            {
                Instantiate(leftBracket, OutputSlotPos);
                RemoveSymbol();
            }
            else if (gameManager.craftIndex[9, 0] == 9 && gameManager.craftIndex[8, 1] == 8) // ¬ | √ = )
            {
                Instantiate(rightBracket, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[6, 0] == 6 && gameManager.craftIndex[7, 1] == 7) || gameManager.craftIndex[6, 1] == 6 && gameManager.craftIndex[7, 0] == 7) // ( | ) = ^ (both orders)
            {
                Instantiate(exponentiation, OutputSlotPos);
                RemoveSymbol();
            }
        }
    }

    void RemoveSymbol() //deletes symbols in crafting slots once used to craft something. also sets crafting
    {
        try
        {
            if (CraftSlot1.transform.childCount == 1) {Destroy(CraftSlot1.transform.GetChild(0).gameObject);}
            if (CraftSlot2.transform.childCount == 1) {Destroy(CraftSlot2.transform.GetChild(0).gameObject);}
            for (int i = 0; i < 10; i++)
            {
                gameManager.craftIndex[i, 0] = 0;
                gameManager.craftIndex[i, 1] = 0;
                gameManager.craftIndex[0, 0] = 0;
                gameManager.craftIndex[0, 1] = 0;
            }
            Debug.Log("Removed");
        }
        catch {RemoveSymbol();}
    }

    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
        .GetComponent<GameManager>();
    }

    void CraftValReset() // is this function used fuckin anywhere???`
    {
        if (CraftSlot1.transform.childCount == 0 && CraftSlot2.transform.childCount == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                gameManager.craftIndex[i, 0] = 0;
                gameManager.craftIndex[i, 1] = 0;
            }
        }
    }

    public void IsEmpty() // removes all values from slot
    {
        gameManager.slotpos[gameManager.TempEqPosStorage, SlotObjN] = 0; //maybe it will work? YES IT DOES HAHA
        gameManager.eqIndex2[AssignEqSlot] = gameManager.addIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.subIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.multIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.divIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.pwrIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rootendIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rootstartIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rbrackIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.lbrackIndex[gameManager.TempEqPosStorage, SlotObjN] = 0;
    }

    void Update()
    {  
        //CraftCheck(); //works if uncommented, but i think it would be better to have the player confirm their choice with a button press
        if (IsNowEmpty == true) //removes values if symbol removed from slot
        {
            IsEmpty();
        }
    }
//credit to https://www.youtube.com/watch?v=kWRyZ3hb1Vc
}