using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3; // bugfix credit: Ardenian     https://forum.unity.com/threads/ambiguous-reference-between-two-references.885529/
public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
    
    //public GameObject OutputSlot;
    
    [HideInInspector] public Transform parentAfterDrag;
    
    
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        /*
        if (OutputSlot.childCount == 0)
        {
            Debug.Log("good");
        
        }
        */
        SymbolParent = GetComponentInParent<Drop>();
        SymbolParent.IsNowEmpty = true;
        Debug.Log("Empty = true");
        
        //gameManager.tempdragstorage = SymbolParent.GetComponentInParent(typeof(Drop)) as Drop;
        //gameManager.eqIndex[drop.EqSlotNum] = 0;
        //Drop.EqSlotNum = 0;
        
        
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
        
        gameManager.tempdragstorage = Value;
        gameManager.TutorialDesc = TutorialNumber;
        gameManager.TempMergeStorage = CanMerge;
        
                //if (currenteqtype.eqSlotDetermine == true)
        //    {
                gameManager.TempEqStorage = EqType;
        //    }
        
        //Debug.Log(TutorialNumber);
        
        
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        
 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag");
        transform.SetParent(parentAfterDrag);
        Image.raycastTarget = true;
        
        SymbolParent.IsNowEmpty = false;
        Debug.Log("Empty = false");
        
        //Drop.CraftCheck();
       
        
        

        
    }
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
        .GetComponent<GameManager>();
        /*
        Drop drop = GameObject.Find("Slot1");
        .GetComponent<Drop>();
        */
        //OutputSlot = GameObject.Find("OutputSlot");
        currenteqtype = GetComponent<Drop>();
          
        Image = GetComponent<Image>();
        startPosition = transform.position;
    }
//https://www.youtube.com/watch?v=kWRyZ3hb1Vc
//credit
}