using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

using Random = System.Random;
//using System.Diagnostics;
//using System.Numerics; // huge credit to https://linuxhint.com/biginteger-csharp/ for the tutorial
//using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private int score;
    public int scorediv5;
    public int rootscore;

    public int selectcount;
    private int playerpos_x;
    public Button pausescreen;
    //public Button WinScreen;

    public TextMeshProUGUI pwrslotText;
    public TextMeshProUGUI pwrslot2Text;
    
    public TextMeshProUGUI rootstartText;
    public TextMeshProUGUI rootendText;
    public TextMeshProUGUI lbrackText;
    public TextMeshProUGUI prestigeWarning;

    public TextMeshProUGUI SlotdispText;
    public TextMeshProUGUI SymboldispText;
    public TextMeshProUGUI targetText;
    
    public TextMeshProUGUI slotpos1Text;

    public TextMeshProUGUI slotpos3Text;
    public TextMeshProUGUI eqSlotText;
    public TextMeshProUGUI solutionIndexText;
    public TextMeshProUGUI solutionText;
    
    public TextMeshProUGUI pauseCountText;
    public TextMeshProUGUI symbolClaimText;
    public TextMeshProUGUI ExtraInfoText;
    
    public Image TutorialList;
    public Image Intro;
    public Image debugMenu;
    public Image alphaGrid;
    public Image betaGrid;

    public Image EquationX1;

    // Tutorial Descriptions
    public TextMeshProUGUI lbrackTutText;
    public TextMeshProUGUI rbrackTutText;
    public TextMeshProUGUI rootTutText;
    public TextMeshProUGUI pwrTutText;
    public TextMeshProUGUI addTutText;
    public TextMeshProUGUI multTutText;
    public TextMeshProUGUI divTutText;
    public TextMeshProUGUI subTutText;
    
    // crafting
    public Transform StorageSlot;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject addition;
    
    //slots
    public double[,] slotpos = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } }; // may have to switch the arrays when eq are switched
    public double[,] slotposStored = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    
    public double[] solutionIndex = {0, 0, 0, 0, 0, 0, 0}; // used ≡ to eqIndex
    
    //equation slots
    public int[] eqIndex = {0, 0, 0, 0, 0, 0, 1}; // val 0 = none, 1 = normal,    in decending order // these are types not values // starts counting at 1 [everything starts at 1]
    
    //crafting slots
    public int[,] craftIndex = { {0, 0, 0}, {0, 0, 0}, { 0, 0, 0 }, { 0, 0, 0 } }; //lets try starting from 0 // [0] = values, [1] = addition, [2] = subtraction, [3] = multiplication
    
    // symbol types in order of operation. used as X in [#, X]
    public int lbrack;
    public int rbrack;
    public int rootstart;
    public int rootend;
    public int pwrslot;
    public int divslot;
    public int multslot;
    public int addslot;
    public int subslot;
    
    // index types in same order. used as X in [X, #]
    public int[,] lbrackIndex       = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] rbrackIndex      = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] rootstartIndex  = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] rootendIndex   = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] pwrIndex      = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] divIndex     = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] multIndex   = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] addIndex   = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    public int[,] subIndex  = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} };
    
    // Recursion indexes for each symbol, switches equivilent position to true if the exact slot is used
    public bool[,] lbrackRecIndex       = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] rbrackRecIndex      = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] rootstartRecIndex  = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] rootendRecIndex   = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] pwrRecIndex      = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] divRecIndex     = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] multRecIndex   = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] addRecIndex   = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
    public bool[,] subRecIndex  = { {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false}, {false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false},{false, false, false, false, false, false, false, false, false, false, false} };
   
     // variables used for computation
    private double pwrvar1 = -1; // the x in (x^y)
    private double pwrvar2 = -1; // the y in (x^y)
    private double divar1 = -1; // the x in (x/y)
    private double divar2 = -1; // the y in (x/y)
    private double multvar1 = -1; // the x in (x*y)
    private double multvar2 = -1; // the y in (x*y)
    private double addvar1 = -1; // the x in (x+y)
    private double addvar2 = -1; // the y in (x+y)
    private double subvar1 = -1; // the x in (x-y)
    private double subvar2 = -1; // the y in (x-y)
    public double solution = 0; // the most recent solution
    
    // temp vaulues for storage
    public double tempdragstorage;
    public int TutorialDesc = 0;
    public bool TempMergeStorage;
    public int TempEqStorage; // type
    public int TempEqPosStorage; // eq slot
    
    // misc
    public int pauseToggle = 0;
    
    // variables used for recursion
    public bool BrackRecDone = false;
    public bool AddRecDone = false;
    public bool MultRecDone = false;
    public bool PowRecDone = false;
    public bool SqrtRecDone = false;
    public bool DivRecDone = false;
    public bool SubRecDone = false;
    
    //variables used for other game mechanics other than calculation
    public double record = 0; // high score
    public double progressIndex = 0;
    public double PrevProgressIndex = 0;
    public double progressStage = 0.6; // the log base that the progress index is calculated at

    
    
    // savedata shit
    [SerializeField]
    private TextMeshProUGUI SourceDataText;
    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private TextMeshProUGUI SaveTimeText;
    [SerializeField]
    private TextMeshProUGUI LoadTimeText;

    private PlayerStats PlayerStats = new PlayerStats();
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled;
    private long SaveTime;
    private long LoadTime;

    public double resetMultiplyer = 1;

     // Start is called before the first frame update :3
    void Start()
    {
        pausescreen.gameObject.SetActive(false);
        TutorialControl(false, false);
        PlayerStats = DataService.LoadData<PlayerStats>("/player-stats.json", EncryptionEnabled);
        resetMultiplyer = PlayerStats.resetMultiplyer;
        
        targetText.text = "" +resetMultiplyer+"\n↓\n"+(1+Math.Sqrt(record/250));
        if (resetMultiplyer > 1)
        {
            Intro.gameObject.SetActive(false);
        }
    }

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }

    public void SerializeJson()
    {
        long startTime = DateTime.Now.Ticks;
        if (DataService.SaveData("/player-stats.json", PlayerStats, EncryptionEnabled))
        {
            SaveTime = DateTime.Now.Ticks - startTime;
            SaveTimeText.SetText($"Save Time: {(SaveTime / TimeSpan.TicksPerMillisecond):N4}ms");

            startTime = DateTime.Now.Ticks;
            try
            {
                PlayerStats data = DataService.LoadData<PlayerStats>("/player-stats.json", EncryptionEnabled);
                LoadTime = DateTime.Now.Ticks - startTime;
                InputField.text = "Loaded from file:\r\n" + JsonConvert.SerializeObject(data, Formatting.Indented);
                LoadTimeText.SetText($"Load Time: {(LoadTime / TimeSpan.TicksPerMillisecond):N4}ms");
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not read file! Show something on the UI here!");
                InputField.text = "<color=#ff0000>Error reading save file!</color>";
            }
        }
        else
        {
            Debug.LogError("Could not save file! Show something on the UI about it!");
            InputField.text = "<color=#ff0000>Error saving data!</color>";
        }
    }

    private void Awake()
    {
        SourceDataText.SetText(JsonConvert.SerializeObject(PlayerStats, Formatting.Indented));
    }

    public void ClearData()
    {
        string path = Application.persistentDataPath + "/player-stats.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            InputField.text = "Loaded data goes` here";
        }
    }

   
    
    public void GetSymbol (int symbolVal)
    {

        switch (symbolVal)
        {case 1:
            Instantiate(one, StorageSlot);
            break;
        case 2:
            Instantiate(two, StorageSlot);
            break;
        case 3:
            Instantiate(three, StorageSlot);
            break;
        }

        Random rnd = new Random(); // credit https://www.tutorialsteacher.com/articles/generate-random-numbers-in-csharp
        if (rnd.Next(10) == 1) // 1/10 chance
            {
                Instantiate(addition, StorageSlot);
            }
        symbolClaimText.text = "[SYMBOL CLAIM / TOTAL: "+StorageSlot.transform.childCount+" ]";
        Debug.Log("FOR LOOP");
    }
    
    public void prestige()
    {
        if(1+Math.Sqrt(record/250) > resetMultiplyer)
        {
            resetMultiplyer = 1 + Math.Sqrt(record/250);
            SourceDataText.text = "{\n\"resetMultiplyer\": "+resetMultiplyer+"\n}";
            PlayerStats.resetMultiplyer = resetMultiplyer;
            Debug.Log(PlayerStats.resetMultiplyer);
            SerializeJson();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            StartCoroutine(ResetWarning());
        }

        
    }

    IEnumerator ResetWarning()
    {
        prestigeWarning.text = "It would be silly to let you do that wouldn't it?\nThink about this: I could have, but made this just so you couldn't";
        yield return new WaitForSecondsRealtime(5);
        prestigeWarning.text = "";
    }
    
    public void gameProgression() // this is where game progression is controlled, ran just before temp values are reset
    { 
        record = (solution > record) ? record = solution : record = record; // shorthand if // 0 for now. fix later               // record is y, output is x
        //m = record / 10000;
        
        if(record >= 970)
        {
            progressIndex = Math.Pow(record*2, 0.6) * resetMultiplyer + 70;
        }
        else if (record >= 20)
        {
            progressIndex = Math.Pow(record, 0.75) * resetMultiplyer + 20;
            EquationX1.gameObject.SetActive(true);
        }
        else if (record >= 8) 
        {
            progressIndex = (Math.Pow(record, 0.75) * resetMultiplyer + 20);//progressIndex = Math.Log((Math.Pow(record, 4.2) - 11), 1.8);
        }                                  //Math.Pow(record, (0.13 * record));//m * (Math.Log(record * progressStage)); //y=m(log(x*c)) m=22 c=0.6  //2.2        //(Math.Log(record, progressIndex + 1)); //progressIndex = (Math.Log((Math.Pow(record, progressIndex)+1), progressStage));
        else
        {
            progressIndex = (Math.Pow(1.7, record - 1)) * resetMultiplyer;
        }
        
        
        progressIndex = Math.Round(progressIndex);
        
        
        if (progressIndex > PrevProgressIndex)
            {
                if (resetMultiplyer > 4)
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {   
                        if (prev % 4 == 0) // gives quarter, but 4x value; saves time with crafting 
                        {
                            GetSymbol(3);
                        }
                    }
                }
                else if (record > 200 || resetMultiplyer > 2)
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {   
                        if (prev % 2 == 0) // gives half, but double value; saves time with crafting 
                        {
                            GetSymbol(2);
                        }
                    }

                }
                else
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {   
                        GetSymbol(1);
                    }
                }  
                PrevProgressIndex = progressIndex;  
            }
        //for (int i = (int) progressIndex)

        
        if (record >= 2)
        {
            Intro.gameObject.SetActive(false);
            if (record == 247)
            {
                QuitToDesktop(); // its a cursed number
            }
        }
        
        
    }

    // Runs the math function
    public void UpdateScore()
    {
        Mathfunc(1, 10, false, 1);
    }

    public void slotposUpd()
    {
        slotpos1Text.text = ""+slotpos[0, 1]+" "+slotpos[0, 2]+" "+slotpos[0, 3]+" "+slotpos[0, 4]+" "+slotpos[0, 5]+" "+slotpos[0, 6]+" "+slotpos[0, 7]+" "+slotpos[0, 8]+" "+slotpos[0, 9]+" "+slotpos[0, 10]+"\n"+slotpos[1, 1]+" "+slotpos[1, 2]+" "+slotpos[1, 3]+" "+slotpos[1, 4]+" "+slotpos[1, 5]+" "+slotpos[1, 6]+" "+slotpos[1, 7]+" "+slotpos[1, 8]+" "+slotpos[1, 9]+" "+slotpos[1, 10]+"\n"+slotpos[2, 1]+" "+slotpos[2, 2]+" "+slotpos[2, 3]+" "+slotpos[2, 4]+" "+slotpos[2, 5]+" "+slotpos[2, 6]+" "+slotpos[2, 7]+" "+slotpos[2, 8]+" "+slotpos[2, 9]+" "+slotpos[2, 10]+"\n"+slotpos[3, 1]+" "+slotpos[3, 2]+" "+slotpos[3, 3]+" "+slotpos[3, 4]+" "+slotpos[3, 5]+" "+slotpos[3, 6]+" "+slotpos[3, 7]+" "+slotpos[3, 8]+" "+slotpos[3, 9]+" "+slotpos[3, 10];
        ;
        eqSlotText.text = "row 0 |"+eqIndex[0]+"|"+solutionIndex[0]+"|\nrow 1 |"+eqIndex[1]+"|"+solutionIndex[1]+"|"+ "\nrow 2 |"+eqIndex[2]+"|"+solutionIndex[2]+"|\nrow 3 |#|#|";//+" "+eqIndex[3];//+" "+eqIndex[3];
        //solutionIndexText.text = "" + solutionIndex[0]+"\n"+solutionIndex[1]+"\n"+solutionIndex[2];//+" "+solutionIndex[3];
        
        
      
        pwrslotText.text = "\t-- internal symbol slot data --\n---------------------------------------\nLbrack 1 = "+lbrackIndex[1, 1]+" "+lbrackIndex[1, 2]+" "+lbrackIndex[1, 3]+" "+lbrackIndex[1, 4]+" "+lbrackIndex[1, 5]+" "+lbrackIndex[1, 6]+" "+lbrackIndex[1, 7]+" "+lbrackIndex[1, 8]+" "+lbrackIndex[1, 9]+" "+lbrackIndex[1, 10]+"\nLbrack 2 = "+lbrackIndex[2, 1]+" "+lbrackIndex[2, 2]+" "+lbrackIndex[2, 3]+" "+lbrackIndex[2, 4]+" "+lbrackIndex[2, 5]+" "+lbrackIndex[2, 6]+" "+lbrackIndex[2, 7]+" "+rbrackIndex[2, 8]+" "+lbrackIndex[2, 9]+" "+lbrackIndex[2, 10]+"\n---------------------------------------\nRbrack 1 = "+rbrackIndex[1, 1]+" "+rbrackIndex[1, 2]+" "+rbrackIndex[1, 3]+" "+rbrackIndex[1, 4]+" "+rbrackIndex[1, 5]+" "+rbrackIndex[1, 6]+" "+rbrackIndex[1, 7]+" "+rbrackIndex[1, 8]+" "+rbrackIndex[1, 9]+" "+rbrackIndex[1, 10]+"\nRbrack 2 = "+rbrackIndex[2, 1]+" "+rbrackIndex[2, 2]+" "+rbrackIndex[2, 3]+" "+rbrackIndex[2, 4]+" "+rbrackIndex[2, 5]+" "+rbrackIndex[2, 6]+" "+rbrackIndex[2, 7]+" "+rbrackIndex[2, 8]+" "+rbrackIndex[2, 9]+" "+rbrackIndex[2, 10]+"\n---------------------------------------\nRootstart 1 = "+rootstartIndex[1, 1]+" "+rootstartIndex[1, 2]+" "+rootstartIndex[1, 3]+" "+rootstartIndex[1, 4]+" "+rootstartIndex[1, 5]+" "+rootstartIndex[1, 6]+" "+rootstartIndex[1, 7]+" "+rootstartIndex[1, 8]+" "+rootstartIndex[1, 9]+" "+rootstartIndex[1, 10]+"\nRootstart 2 = "+rootstartIndex[2, 1]+" "+rootendIndex[2, 2]+" "+rootstartIndex[2, 3]+" "+rootstartIndex[2, 4]+" "+rootstartIndex[2, 5]+" "+rootstartIndex[2, 6]+" "+rootstartIndex[2, 7]+" "+rootstartIndex[2, 8]+" "+rootstartIndex[2, 9]+" "+rootstartIndex[2, 10]+"\n---------------------------------------\nRootend 1 = "+rootendIndex[1, 1]+" "+rootendIndex[1, 2]+" "+rootendIndex[1, 3]+" "+rootendIndex[1, 4]+" "+rootendIndex[1, 5]+" "+rootendIndex[1, 6]+" "+rootendIndex[1, 7]+" "+rootendIndex[1, 8]+" "+rootendIndex[1, 9]+" "+rootendIndex[1, 10]+"\nRootend 2 = "+rootendIndex[2, 1]+" "+rootendIndex[2, 2]+" "+rootendIndex[2, 3]+" "+rootendIndex[2, 4]+" "+rootendIndex[2, 5]+" "+rootendIndex[2, 6]+" "+rootendIndex[2, 7]+" "+rootendIndex[2, 8]+" "+rootendIndex[2, 9]+" "+rootendIndex[2, 10]+"\n---------------------------------------\nPwrslot 1 = "+pwrIndex[1, 1]+" "+pwrIndex[1, 2]+" "+pwrIndex[1, 3]+" "+pwrIndex[1, 4]+" "+pwrIndex[1, 5]+" "+pwrIndex[1, 6]+" "+pwrIndex[1, 7]+" "+pwrIndex[1, 8]+" "+pwrIndex[1, 9]+" "+pwrIndex[1, 10]+"\nPwrslot 2 = "+pwrIndex[2, 1]+" "+pwrIndex[2, 2]+" "+pwrIndex[2, 3]+" "+pwrIndex[2, 4]+" "+pwrIndex[2, 5]+" "+pwrIndex[2, 6]+" "+pwrIndex[2, 7]+" "+pwrIndex[2, 8]+" "+pwrIndex[2, 9]+" "+pwrIndex[2, 10]+"\n---------------------------------------\nDivslot 1 = "+divIndex[1, 1]+" "+divIndex[1, 2]+" "+divIndex[1, 3]+" "+divIndex[1, 4]+" "+divIndex[1, 5]+" "+divIndex[1, 6]+" "+divIndex[1, 7]+" "+divIndex[1, 8]+" "+divIndex[1, 9]+" "+divIndex[1, 10]+"\nDivslot 2 = "+divIndex[2, 1]+" "+divIndex[2, 2]+" "+divIndex[2, 3]+" "+divIndex[2, 4]+" "+divIndex[2, 5]+" "+divIndex[2, 6]+" "+divIndex[2, 7]+" "+divIndex[2, 8]+" "+divIndex[2, 9]+" "+divIndex[2, 10]+"\n---------------------------------------\nMultslot 1 = "+multIndex[1, 1]+" "+multIndex[1, 2]+" "+multIndex[1, 3]+" "+multIndex[1, 4]+" "+multIndex[1, 5]+" "+multIndex[1, 6]+" "+multIndex[1, 7]+" "+multIndex[1, 8]+" "+multIndex[1, 9]+" "+multIndex[1, 10]+"\nMultslot 2 = "+multIndex[2, 1]+" "+multIndex[2, 2]+" "+multIndex[2, 3]+" "+multIndex[2, 4]+" "+multIndex[2, 5]+" "+multIndex[2, 6]+" "+multIndex[2, 7]+" "+multIndex[2, 8]+" "+multIndex[2, 9]+" "+multIndex[2, 10]+"\n---------------------------------------\nAddslot 1 = "+""+addIndex[1, 1]+" "+addIndex[1, 2]+" "+addIndex[1, 3]+" "+addIndex[1, 4]+" "+addIndex[1, 5]+" "+addIndex[1, 6]+" "+addIndex[1, 7]+" "+addIndex[1, 8]+" "+addIndex[1, 9]+" "+addIndex[1, 10]+"\nAddslot 2 = "+addIndex[2, 1]+" "+addIndex[2, 2]+" "+addIndex[2, 3]+" "+addIndex[2, 4]+" "+addIndex[2, 5]+" "+addIndex[2, 6]+" "+addIndex[2, 7]+" "+addIndex[2, 8]+" "+addIndex[2, 9]+" "+addIndex[2, 10]+"\n---------------------------------------\nSubslot 1 = "+subIndex[1, 1]+" "+subIndex[1, 2]+" "+subIndex[1, 3]+" "+subIndex[1, 4]+" "+subIndex[1, 5]+" "+subIndex[1, 6]+" "+subIndex[1, 7]+" "+subIndex[1, 8]+" "+subIndex[1, 9]+" "+subIndex[1, 10]+"\nSubslot 2 = "+subIndex[2, 1]+" "+subIndex[2, 2]+" "+subIndex[2, 3]+" "+subIndex[2, 4]+" "+subIndex[2, 5]+" "+subIndex[2, 6]+" "+subIndex[2, 7]+" "+subIndex[2, 8]+" "+subIndex[2, 9]+" "+subIndex[2, 10]+"\n---------------------------------------";
        
        rootstartText.text = "dev build v1.10";
        
        pauseCountText.text = "-- misc data --\nnumber of pauses: " + pauseToggle+"\nreset multiplyer: "+resetMultiplyer+"\ncurrent Equation slot: ["+TempEqPosStorage+"]\neq type select: "+TempEqStorage+"\n\t-- crafting slot data --\n---------------------------------------\n"+craftIndex[0, 0]+" "+craftIndex[0,1]+"\n"+craftIndex[1, 0]+" "+craftIndex[1,1];
        
        targetText.text = "" +resetMultiplyer+"\n↓\n"+(1+Math.Sqrt(record/250));
    }

    public void EqReset(int CurrentEq)
    {   
        Debug.Log("Resetting Slot Data for ["+CurrentEq+", 1-10]");
        for (int i = 1; i <= 10; i++)
        {
            slotpos[CurrentEq, i] = slotposStored[CurrentEq, i];
        }
        
        for (int k = 0; k <= 3; k++)
        {
            for (int i = 0; i <= 10; i++)
            {
                lbrackRecIndex[k, i] = false;
                rbrackRecIndex[k, i] = false;
                rootstartRecIndex[k, i] = false;
                rootendRecIndex[k, i] = false;
                pwrRecIndex[k, i] = false;
                divRecIndex[k, i] = false;
                multRecIndex[k, i] = false;
                addRecIndex[k, i] = false;
                subRecIndex[k, i] = false;
            }
        }
        solution = 0;
    }

    public void EqPreset()
    {
        for (int i = 0; i <= 3; i++)
            {
                for (int k = 1;k<=10; k++)
                {
                    slotposStored[i, k] = slotpos[i,k];
                }
            }
    }

    public void Mathfunc(int startpos, int endpos, bool IsRecCall, int eqCalled)
    {   
        
        
        if (IsRecCall == false)
        {
            Debug.Log("//---------- Mathfunc Called ----------\\\\");
        }
        else
        {
            Debug.Log("      //--- Called Recursively ---\\\\");
        }
        
        Debug.Log("calling slots: " + startpos + " to " + endpos);
        Debug.Log("called recursively?: " + IsRecCall);
        Debug.Log("Equation SLOT called: " + eqCalled + " // Equation TYPE called: " + eqIndex[eqCalled]);
        
        switch (eqIndex[eqCalled]) //checks for current eq slot
        {

            case 0: //normal type
            {
        
                if(IsRecCall == false)
                {
                    EqPreset();
                }

                // BRACKETS
            
                for (int index=startpos;index<endpos;index++) // check all possible positions within bounds for left brackets 
                {
                    if (lbrackIndex[eqCalled, index] > 0 && lbrackRecIndex[eqCalled, index] == false)
                    {
                        Debug.Log("lbrack found at slot"+index);
                        for (int index2=index+1;index2<=endpos;index2++) // if a vaild left bracket position is found, check all for right brackets, starting at the left bracket position to end
                        {
                            if (rbrackIndex[eqCalled, index2] > 0 && rbrackRecIndex[eqCalled, index2] == false) // if checked position has a right bracket in it and has not been done before
                            {
                                Debug.Log("rbrack found at slot"+index2);
                                lbrackRecIndex[eqCalled, index] = rbrackRecIndex[eqCalled, index2] = true; // updates what has been solved for recursion
                                Mathfunc(lbrackIndex[eqCalled, index] + 1, rbrackIndex[eqCalled, index2] - 1, true, eqCalled); //what stuff is in the brackets and solve that first
                                Debug.Log("post brack TEST =" + solution);
                                //solution = slotpos[eqCalled, rbrackIndex[eqCalled, rbrack]-1];
                                slotpos[eqCalled, rbrackIndex[eqCalled, index2]] = slotpos[eqCalled, lbrackIndex[eqCalled, index]] = solution;
                                Debug.Log("//BRACKETS DONE)");
                                Debug.Log("post brack =" + solution);
                            }
                        }
                    }
                    //&&  <= endpos && rbrackIndex[eqCalled, rbrack] > lbrackIndex[eqCalled, lbrack] && B) // if brackets are used within scope
                }
            

                // EXPONENTS

                // ROOT OR POWER
                for (int expcheck = startpos; expcheck <= endpos; expcheck++)
                {
                    //Debug.Log("expcheck = "+expcheck);
                    if (rootstartIndex[eqCalled, expcheck] > 0 || pwrIndex[eqCalled, expcheck] > 0)
                    {
                        if (rootstartIndex[eqCalled, expcheck] > pwrIndex[eqCalled, expcheck]) // ROOT FOUND FIRST
                        {
                            Debug.Log("Root Found");
                            if (rootstartIndex[eqCalled, expcheck] > 0 && rootstartRecIndex[eqCalled, expcheck] == false) // ROOT FIRST IF TRYE
                            {
                                for (int index2=startpos; index2<=endpos; index2++)
                                {
                                    Debug.Log("index = "+index2);
                                    if (rootendIndex[eqCalled, index2] > 0 && rootendRecIndex[eqCalled, expcheck] == false)
                                    {
                                        rootstartRecIndex[eqCalled, expcheck] = true;
                                        rootendRecIndex[eqCalled, index2] = true;
                                        Mathfunc(rootstartIndex[eqCalled, expcheck] + 1, rootendIndex[eqCalled, index2] - 1, true, eqCalled);
                                        solution = Math.Sqrt(solution);
                                        slotpos[eqCalled, rootendIndex[eqCalled, index2]] = slotpos[eqCalled, rootstartIndex[eqCalled, expcheck]] = solution;
                                        Debug.Log("//SQUARE ROOT DONE");
                                        Debug.Log(solution);
                                    }
                                }
                            }
                        }
                        else if (rootstartIndex[eqCalled, expcheck] < pwrIndex[eqCalled, expcheck])
                        {
                            for (int index = startpos; index < endpos; index++)
                            {
                                if (pwrIndex[eqCalled, index] > 0 && pwrIndex[eqCalled, index] > startpos && pwrIndex[eqCalled, index] < endpos && pwrRecIndex[eqCalled, index] == false)
                                {
                                    Debug.Log("exponentiation: [" + eqCalled + ", " + pwrIndex[eqCalled, index] + "]");
                                    Debug.Log(slotpos[eqCalled, pwrIndex[eqCalled, index] - 1] + " ^ " + slotpos[eqCalled, pwrIndex[eqCalled, index] + 1]);
                                    pwrRecIndex[eqCalled, index] = true;
                                    pwrvar1 = slotpos[eqCalled, pwrIndex[eqCalled, index] - 1];
                                    pwrvar2 = slotpos[eqCalled, pwrIndex[eqCalled, index] + 1];
                                    solution = slotpos[eqCalled, pwrIndex[eqCalled, index] - 1] = slotpos[eqCalled, pwrIndex[eqCalled, index]] = slotpos[eqCalled, pwrIndex[eqCalled, index] + 1] = Math.Pow(pwrvar1, pwrvar2);
                                    Debug.Log("exponentiation: " + solution);
                                    Debug.Log("POWER DONE");
                                }
                            }
                        }
                    }
                }
                /*
                if (rootstartIndex[eqCalled, rootstart] < pwrIndex[eqCalled, pwrslot])
                {
                    // SQUARE ROOT
                    for (int index = startpos; index<=endpos; index++)
                    {
                        if (rootstartIndex[eqCalled, index] > 0 && rootstartRecIndex[eqCalled, index] == false)
                        {
                            for (int index2=index+1;index2<=endpos; index2++)
                            {
                                if (rootendIndex[eqCalled, ] <= endpos && rootendRecIndex[eqCalled, index] == false)
                                {
                                    rootstartRecIndex[eqCalled, index] = true;
                                    rootendRecIndex[eqCalled, index2] = true;
                                    Mathfunc(rootstartIndex[eqCalled, index] + 1, rootendIndex[eqCalled, index2] - 1, true, eqCalled);
                                    solution = slotpos[eqCalled, rootendIndex[eqCalled, index2] - 1]; // get solution value from the last slot inside of the scope
                                    solution = Math.Sqrt(solution); // square root the solution
                                    slotpos[eqCalled, rootendIndex[eqCalled, index2]] = slotpos[eqCalled, rootstartIndex[eqCalled, index]] = solution; // make the root slots equal the new solution
                                    Debug.Log("//SQUARE ROOT DONE");
                                    Debug.Log(solution);

                                }
                            }
                        }
                    }

                    // POWER
                    if (pwrIndex[eqCalled, pwrslot] > startpos && pwrIndex[eqCalled, pwrslot] < endpos && pwrIndex[eqCalled, pwrslot] > 1 && PowRecDone == false) // if only power is used and within param
                    {
                        pwrvar1 = slotpos[eqCalled, pwrIndex[eqCalled, pwrslot]-1];
                        pwrvar2 = slotpos[eqCalled, pwrIndex[eqCalled, pwrslot]+1];
                        solution = slotpos[eqCalled, pwrIndex[eqCalled, pwrslot]-1] = slotpos[eqCalled, pwrIndex[eqCalled, pwrslot]] = slotpos[eqCalled, pwrIndex[eqCalled, pwrslot] + 1] = Math.Pow(pwrvar1, pwrvar2);//BigInteger.Pow(pwrvar1, (int)pwrvar2); // exponent is BigInteger ///https://stackoverflow.com/questions/30224589/biginteger-powbiginteger-biginteger
                        PowRecDone = true;
                        Debug.Log("//POWER DONE SECOND");
                        Debug.Log(solution);
                    }
                }
               
    

                    // SQUARE ROOT
                    if (rootstartIndex[eqCalled, rootstart] >= startpos && rootendIndex[eqCalled, rootend] <= endpos && SqrtRecDone == false)
                    {
                        Mathfunc(rootstartIndex[eqCalled, rootstart] + 1, rootendIndex[eqCalled, rootend] - 1, true, eqCalled);
                        solution = slotpos[eqCalled, rootendIndex[eqCalled, rootend] - 1];
                        solution = Math.Sqrt(solution);
                        slotpos[eqCalled, rootendIndex[eqCalled, rootend]] = slotpos[eqCalled, rootstartIndex[eqCalled, rootstart]] = solution;
                        Debug.Log("//SQUARE ROOT DONE SECOND");
                        SqrtRecDone = true;
                        Debug.Log(solution);
                    }
                }
                */

                // DIVISION
                for (int index = startpos; index < endpos; index++)
                {
                    if (divIndex[eqCalled, index] > 0 && divIndex[eqCalled, index] > startpos && divIndex[eqCalled, index] < endpos && divRecIndex[eqCalled, index] == false) // if checked position has one, is more than startpos and less than endpos and recursion matrix says false, then it returns true
                    {
                        Debug.Log("division: [" + eqCalled + ", " + divIndex[eqCalled, index] + "]");
                        Debug.Log(slotpos[eqCalled, divIndex[eqCalled, index] - 1] + " / " + slotpos[eqCalled, divIndex[eqCalled, index] + 1]);
                        divRecIndex[eqCalled, index] = true;
                        divar1 = slotpos[eqCalled, divIndex[eqCalled, index] - 1];
                        divar2 = slotpos[eqCalled, divIndex[eqCalled, index] + 1];
                        solution = slotpos[eqCalled, divIndex[eqCalled, index] - 1] = slotpos[eqCalled, divIndex[eqCalled, index]] = slotpos[eqCalled, divIndex[eqCalled, index] + 1] = divar1 / divar2;
                        Debug.Log("division: " + solution);
                        Debug.Log("DIVISION DONE");
                    }
                }

           

                // MULTIPLICATION
                for (int index = startpos; index < endpos; index++)
                {
                    if (multIndex[eqCalled, index] > 0 && multIndex[eqCalled, index] > startpos && multIndex[eqCalled, index] < endpos && multRecIndex[eqCalled, index] == false)
                    {

                        Debug.Log("multiplication: [" + eqCalled + ", " + multIndex[eqCalled, index] + "]");
                        Debug.Log(slotpos[eqCalled, multIndex[eqCalled, index] - 1] + " x " + slotpos[eqCalled, multIndex[eqCalled, index] + 1]);
                        multRecIndex[eqCalled, index] = true;
                        multvar1 = slotpos[eqCalled, multIndex[eqCalled, index] - 1];
                        multvar2 = slotpos[eqCalled, multIndex[eqCalled, index] + 1];
                        solution = slotpos[eqCalled, multIndex[eqCalled, index] - 1] = slotpos[eqCalled, multIndex[eqCalled, index]] = slotpos[eqCalled, multIndex[eqCalled, index] + 1] = multvar1 * multvar2;
                        Debug.Log("multiplication: " + solution);
                        Debug.Log("//MULTIPLICATION DONE");
                    }
                }
               
            

                // ADDITION
                for (int index = startpos; index < endpos; index++)
                {
                    if (addIndex[eqCalled, index] > 0 && addRecIndex[eqCalled, index] == false)
                    {
                        if (addIndex[eqCalled, index] > startpos && addIndex[eqCalled, index] < endpos)
                        {
                                Debug.Log(index);
                                Debug.Log("addition: [" + eqCalled + ", " + addIndex[eqCalled, index] + "]");
                                Debug.Log(slotpos[eqCalled, addIndex[eqCalled, index] - 1] + " + " + slotpos[eqCalled, addIndex[eqCalled, index] + 1]);
                                addRecIndex[eqCalled, index] = true;
                                addvar1 = slotpos[eqCalled, addIndex[eqCalled, index] - 1];
                                addvar2 = slotpos[eqCalled, addIndex[eqCalled, index] + 1];
                                solution = slotpos[eqCalled, addIndex[eqCalled, index] - 1] = slotpos[eqCalled, addIndex[eqCalled, index]] = slotpos[eqCalled, addIndex[eqCalled, index] + 1] = (addvar1 + addvar2);
                                Debug.Log("addition: " + solution);
                                Debug.Log("//ADDITION DONE");
                        }
                    }
                }

                // SUBTRACTION
                for (int index = startpos; index < endpos; index++)
                {
                    if (subIndex[eqCalled, index] > 0 && subIndex[eqCalled, index] > startpos && subIndex[eqCalled, index] < endpos && subRecIndex[eqCalled, index] == false)
                    {
                            Debug.Log("subtraction: [" + eqCalled + ", " + subIndex[eqCalled, index] + "]");
                            Debug.Log(slotpos[eqCalled, subIndex[eqCalled, index] - 1] + " - " + slotpos[eqCalled, subIndex[eqCalled, index] + 1]);
                            subRecIndex[eqCalled, index] = true;
                            subvar1 = slotpos[eqCalled, subIndex[eqCalled, index] - 1];
                            subvar2 = slotpos[eqCalled, subIndex[eqCalled, index] + 1];
                            solution = slotpos[eqCalled, subIndex[eqCalled, index]--] = slotpos[eqCalled, subIndex[eqCalled, index]] = slotpos[eqCalled, subIndex[eqCalled, index]++] = subvar1 - subvar2;
                            Debug.Log("subtraction: " + solution);
                            Debug.Log("//SUBTRACTION DONE");
                    }
                }

                if (IsRecCall == true)
                {
                    Debug.Log("      \\\\--- Recursion Done ---//");

                }

                if (IsRecCall == false)
                {
                    solutionIndex[eqCalled] = solution;
                    Debug.Log("\\\\---------- Mathfunc Done ----------//");
                    solution = solutionIndex[1]+solutionIndex[2];
                    Debug.Log("solution: " + solution);
                    solutionText.text = ""+solution;/*+ solutionIndex[0] + " "*/ //+(solutionIndex[1]+solutionIndex[2]);
                    
                    
                    
                    slotposUpd();
                    gameProgression();
                    EqReset(eqCalled); // maybe an issue?
                }
                
                
                    
                break;
            }
            
            case 1: // empty val / index[#] = 0
            {
                Debug.Log("no equation found");
                break;
            }
        }
        
        try
        {
            if (eqIndex[eqCalled] == 0 && eqIndex[eqCalled++] != 1 && IsRecCall == false ) // if next eq is not empty then run again with //&& eqIndex[eqCalled + 1] > 0
                {
                    Debug.Log("//////////////////////////////////////////////////////////////// Next Equation Start");
                    Mathfunc(1, 10, false, eqCalled++);
                }
            /*if (eqIndex[eqCalled] == 0 && eqCalled < 4/*Index[eqCalled++] != 1*/ //&& IsRecCall == false ) // if next eq is not empty then run again with //&& eqIndex[eqCalled + 1] > 0
               // {
                    //Debug.Log("//////////////////////////////////////////////////////////////// Next Equation Start");
                    //Mathfunc(1, 10, false, eqCalled++);
               // }
        }
        
        catch
        {   
            
            Debug.Log("////////////////////////////////////////////////////////////// Equation after Eqslot[" + eqCalled + "] is fucked in some way");
            
           
            /*
            for (int index = eqCalled; index > 0; index--)
                    {
                        solution += solutionIndex[index];
                        Debug.Log("solution: " + solution);
                    }
            */

           
                    
        }

        
                    
    }
    
    
    public void TutorialControl(bool state, bool antiState)
    {
        if (state == true)
        {
         switch (TutorialDesc)
            {
                case 1: // Addition
                    addTutText.gameObject.SetActive(true);
                    break;

                case 2: // Multiplication
                    multTutText.gameObject.SetActive(true);
                    break;

                case 3: // Division
                    divTutText.gameObject.SetActive(true);
                    break;

                case 4: // Subtraction
                    subTutText.gameObject.SetActive(true);
                    break;

                case 5: // Exponentiation
                    pwrTutText.gameObject.SetActive(true);
                    break;

                case 6: // Square Root
                    rootTutText.gameObject.SetActive(true);
                    break;
                
                case 7: // Right Bracket
                    rbrackTutText.gameObject.SetActive(true);
                    break;
                
                case 8: // Left Bracket
                    lbrackTutText.gameObject.SetActive(true);
                    break;
                    
                default:
                    break;
            }
        }
        else
        {
                    
            lbrackTutText.gameObject.SetActive(antiState);
            rbrackTutText.gameObject.SetActive(antiState);
            rootTutText.gameObject.SetActive(antiState);
            pwrTutText.gameObject.SetActive(antiState);
            divTutText.gameObject.SetActive(antiState);
            multTutText.gameObject.SetActive(antiState);
            addTutText.gameObject.SetActive(antiState);
            subTutText.gameObject.SetActive(antiState);

        }
    }
    
    public void QuitToDesktop()
    {
        Debug.Log("!!! QUIT CALLED !!!");
        Application.Quit();
    }

    public void DebugControl(bool TildaDown)
    {
        debugMenu.gameObject.SetActive(TildaDown);
        slotposUpd();
    }
    /*
    public void EqGridToggle(bool TabDown)
    {
        alphaGrid.gameObject.SetActive(TabDown);
        betaGrid.gameObject.SetActive(TabDown);
    }
    */
    public void PauseControl(bool EscToggle)
    {
            pausescreen.gameObject.SetActive(EscToggle);
    }

    // Update is called once per frame ----------------------------------------------------------------- VOID UPDATE --
    void Update()
    {
        
            
        if (Input.GetKey(KeyCode.BackQuote)) 
       
        { 
            DebugControl(true); 
        }
        
        else
        
        {
            DebugControl(false);
        }
        
        /*if(Input.GetKey(KeyCode.Mouse1))
        {
            EqGridToggle(false);
        }
        else
        {
            EqGridToggle(true);
        }
        */
        
        if (Input.GetKeyDown(KeyCode.H)) { TutorialControl(true, false); 
        }
        
        if (Input.GetKeyUp(KeyCode.H)) { TutorialControl(false, false);
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
            {
                Mathfunc(1, 10, false, 1);
            }
            
       // else { TutorialControl(false, false);
     //   }
          
        if (Input.GetKeyDown(KeyCode.Escape)) // if odd amt of esc presses then it pauses
        {
            pauseToggle++;
            
            if(pauseToggle % 2 == 0)
            {
            
              PauseControl(false); // game is now unpaused below this line ---------------------- UNPAUSED \/
              

            }
           
            else
           
            {
              PauseControl(true); // game is now paused below this line
            }
            
        }
    }
}