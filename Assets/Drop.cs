using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler
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
    
    public GameObject one; // IM SORRY BUT ITS 12:02 AT NIGHT AND IM TIRED
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject nine;
    
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
    /*
    public GameObject removeChild;
    public Transform removeTransChild; // ok thats too on the nose
    */
    public bool stopInstantly = false;
    
    
    
    public void  OnPointerEnter(PointerEventData eventData) //if mouse enters an equation
         {   
            if (eqSlotDetermine == true)
            {
                //Debug.Log("Assign Equation Slot: " + AssignEqSlot);
                AssignEqSlot = EqSlotNum; // in slot 1's case: 1
                //Debug.Log("Assign Equation Slot NOW: " + AssignEqSlot);

                gameManager.TempEqPosStorage = AssignEqSlot;
                //Debug.Log("temp eq pos is now: " + gameManager.TempEqPosStorage);

                //gameManager.eqIndex[AssignEqSlot] = gameManager.TempEqStorage;
                
            }
         }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eqSlotDetermine == true)
        {
            gameManager.eqIndex[AssignEqSlot] = gameManager.TempEqStorage;
        }
        
        if (transform.childCount == 0 && gameManager.TempMergeStorage == false)
        {
            GameObject dropped = eventData.pointerDrag;
            Drag drag = dropped.GetComponent<Drag>();
            drag.parentAfterDrag = transform;
           

            if (eqSlotDetermine == false)// if symbol dropped
            {   
                if (CraftSlot == false)
                {
                    gameManager.slotpos[gameManager.TempEqPosStorage, SlotObjN] = gameManager.tempdragstorage; //changed slotpos internal val = assigned int to the slot obj
                    //Debug.Log("eq pos is now: " + gameManager.TempEqPosStorage);
                   // Debug.Log("slot pos is now: " + SlotObjN);
                    Debug.Log("assigned slotpos:                     " + gameManager.TempEqPosStorage + ", " + SlotObjN + " with value of " + gameManager.tempdragstorage);
                     // case 1 in eq 1 // slotpos = 1, 1

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
                        case 10:
                            /*
                            gameManager.eqIndex[EqSlotNum] = 1; //normal equation
                            Debug.Log("case 10 line 1: " + gameManager.eqIndex[EqSlotNum]);
                            gameManager.eqIndex[EqSlotNum] = gameManager.TempEqStorage;
                            Debug.Log("case 10 line 2: " + gameManager.eqIndex[EqSlotNum]);
                            */
                            break;


                        default:
                            break;
                    }

                }
                else //crafting possible
                {
                    Debug.Log("Craft slot: true");
                    if (gameManager.tempdragstorage != 0) // if number dropped
                    {
                        gameManager.craftIndex[0, CraftSlotPos] = (int) gameManager.tempdragstorage;
                    }
                    else
                    {
                        gameManager.craftIndex[drag.SymbolType, CraftSlotPos] = drag.SymbolType;
                    }
                    
                    Debug.Log("Craft slot: "+drag.SymbolType+", "+CraftSlotPos + " = " + gameManager.tempdragstorage +"\nval if > [0]:"+drag.SymbolType);
                    
                    
                    //CraftCheck();

                }
            }




        }
    gameManager.slotposUpd();
    }
    //else// if (gameManager.TempMergeStorage == false)
    //{
        /*GameObject dropped = eventData.pointerDrag;
        Drag drag = dropped.GetComponent<Drag>();

        GameObject current = transform.GetChild(0).gameObject;
        Drag currentDraggable = current.GetComponent<Drag>();

        currentDraggable.transform.SetParent(drag.parentAfterDrag);
        drag.parentAfterDrag = transform;

        /*gameManager.slotpos[AssignEqSlot, SlotObjN] = gameManager.tempdragstorage;
        Debug.Log("assigned slotpos: " + AssignEqSlot + "," + gameManager.tempdragstorage);
        */
    //}
    /*else if (transform.childCount == 0 && gameManager.TempMergeStorage == true && )
    {
    switch (gameManger.Temp)
    }*/

    public void CraftCheck()
    {   
        //try
        //{
            
        if(CraftSlot1.transform.childCount == 1 && CraftSlot2.transform.childCount == 1)
        {
            if (gameManager.craftIndex[0, 0] == gameManager.craftIndex[0, 1] && (gameManager.craftIndex[0, 0] > 0 || gameManager.craftIndex[0, 1] > 0))
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
                    case 9:
                        //RemoveSymbol();
                        //Instantiate(nine, OutputSlotPos);
                        break;

                }
                

            }
            else if((gameManager.craftIndex[0, 0] == 4 && gameManager.craftIndex[1, 1] == 1) || (gameManager.craftIndex[0, 1] == 4 && gameManager.craftIndex[1, 0] == 1)) // 4 | + = x
            {
                Instantiate(multiplication, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[0, 0] == 6 && gameManager.craftIndex[3, 1] == 3) || (gameManager.craftIndex[0, 1] == 6 && gameManager.craftIndex[3, 0] == 3)) // 6 | x = ( )
            {
                Instantiate(subtraction, OutputSlotPos);
                RemoveSymbol();
                
            }
            else if ((gameManager.craftIndex[2, 0] == 2 && gameManager.craftIndex[2, 1] == 2)) // - | - = /
            {
                Instantiate(division, OutputSlotPos);
                RemoveSymbol();   
            }
            else if ((gameManager.craftIndex[4, 0] == 4 && gameManager.craftIndex[3, 1] == 3))
            {
                Instantiate(rootStart, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[3, 0] == 3 && gameManager.craftIndex[4, 1] == 4))
            {
                Instantiate(rootEnd, OutputSlotPos);
                RemoveSymbol();
            }
            else if ((gameManager.craftIndex[8, 0] == 8 && gameManager.craftIndex[9, 1] == 9))
            {
                Instantiate(leftBracket, OutputSlotPos);
                RemoveSymbol();
            }
            else if (gameManager.craftIndex[9, 0] == 9 && gameManager.craftIndex[8, 1] == 8)
            {
                Instantiate(rightBracket, OutputSlotPos);
                RemoveSymbol();
            }
        }
      /*  }
        catch
        {
            Debug.Log("Craft Try Failed (?)"); 
            CraftCheck();
        }*/
    }
    
    void RemoveSymbol()
    {  
        try
        {
            if (CraftSlot1.transform.childCount == 1)
            {
                Destroy(CraftSlot1.transform.GetChild(0).gameObject);
            }

            if (CraftSlot2.transform.childCount == 1)
            {
                Destroy(CraftSlot2.transform.GetChild(0).gameObject);
            }

            for (int i = 0; i < 10; i++)
            {
                gameManager.craftIndex[i, 0] = 0;
                gameManager.craftIndex[i, 1] = 0;
                gameManager.craftIndex[0, 0] = 0;
                gameManager.craftIndex[0, 1] = 0;

            }
            Debug.Log("Removed");

        }
        catch
        {
            RemoveSymbol();
        }
    }

    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
        .GetComponent<GameManager>();
        /*
        OutputSlot = GameObject.Find("OutputSlot");
        OutputSlotPos = OutputSlot.transform;
        /*
        CraftSlot1 = GameObject.Find("CraftSlot1");
        //CraftSlot1Trans = CraftSlot1.transform;
        
        CraftSlot2 = GameObject.Find("CraftSlot2");
        //CraftSlot1Trans = CraftSlot1.transform;
        
        CraftingGrid = GameObject.Find("crafting grid");
        CraftingGridTrans = CraftingGrid.transform;
        /*
        Instantiate(CraftSlot1, CraftingGridTrans);
        Instantiate(CraftSlot2, CraftingGridTrans);
        */
    }

    void CraftValReset()
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
    
    void Update()
    {   
        
        //CraftCheck(); //works if uncommented, but i think it would be better to have the player confirm their choice with a button press
        if (IsNowEmpty == true)
        {
            /*try
            {
                gameManager.craftIndex[SymbolType, CraftSlotPos] = 0;
            }
            catch
            {
                Debug.Log("boo");
            }*/
            
            gameManager.slotpos[gameManager.TempEqPosStorage, SlotObjN] = 0; //maybe it will work? YES IT DOES HAHA
            gameManager.eqIndex[AssignEqSlot] = gameManager.addIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.subIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.multIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.divIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.pwrIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rootendIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rootstartIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.rbrackIndex[gameManager.TempEqPosStorage, SlotObjN] = gameManager.lbrackIndex[gameManager.TempEqPosStorage, SlotObjN] = 0;
            
            
        }
        //CraftValReset();
    }


//https://www.youtube.com/watch?v=kWRyZ3hb1Vc
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponentInParent.html                        // do i need to credit unity documtentation??
//credit for all code except for lines 20-54 which were me                                            // this is outdated dummy
//credit for lines 56-65 go to @bertiedev6478, who can be found in the comments of the video above

}