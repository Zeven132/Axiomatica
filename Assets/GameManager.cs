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


public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private int score;
    public int scorediv5;
    public int rootscore;

    public int selectcount;
    private int playerpos_x;
    public Button pausescreen;
   
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
    public TextMeshProUGUI endTime;
   
    // Misc Images
    public Image TutorialList;
    public Image Intro;
    public Image debugMenu;
    public Image alphaGrid;
    public Image betaGrid;
   
    // Equations
    public Image EquationX1;
    public Image EquationX2;
    public Image EquationX3;
    public Image EquationX4;
    public Image EquationX5;
    public Image EquationX6;
    public Image EquationX7;
    public Image EquationX8;

    // Tutorial text
    public TextMeshProUGUI tutText;
   
    // Crafting
    public Transform StorageSlot; // output for instantantiated symbols (not output slot for crafting)
    public GameObject one; // one symbol
    public GameObject two; // two symbol
    public GameObject three; // three symbol
    public GameObject four; // four symbol
    public GameObject five; // five symbol
    public GameObject addition; // addition symbol
    public GameObject multiplication; // multiplication symbol

    // misc game objects
    public GameObject BVSymbol; // symbol with 999999 val, used for cheats
    public GameObject WinScreen; // win screen
    public GameObject InventorySlot; // inventory gameobject
    public GameObject TrashSlot; // trash slot
   
    // Slot numeric values
    public double[,] slotpos = new double [12, 11]; // used during calculation
    public double[,] slotposStored = new double [12, 11]; // stored to change values back after calculation
   
    // Solutions
    public double[] solutionIndex = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; // only works for [1], [2] // the reason for this error is unknown
    public double[] solutionIndex2 = new double[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //same here; works until [6]
    public double[] solutionIndex3 = new double[13] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; // until [9]
   
    // Squation slots
    public int[] eqIndex = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; // used for different type of equations
    public int[] eqIndex2 = new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; // if 0 then normal, if 1 then ignored

   
    // Crafting slots
    public int[,] craftIndex = new int[11, 4];
    //[0] = values, [1] = addition, [2] = subtraction, [3] = multiplication, [4] = division, [5] = exponentiation, [6] = left bracket, [7] = right bracket, [8] = rootstart, [9] = rootend
    // [x, n] = x if there is a symbol there
    // Symbol types in order of operation. used as X in [#, X]
    public int lbrack;
    public int rbrack;
    public int rootstart;
    public int rootend;
    public int pwrslot;
    public int divslot;
    public int multslot;
    public int addslot;
    public int subslot;
    
    // Index types in order of operation. 0 is ignored
    public int[,] lbrackIndex       = new int [12,11];
    public int[,] rbrackIndex      = new int [12,11];
    public int[,] rootstartIndex  = new int [12,11];
    public int[,] rootendIndex   = new int [12,11];
    public int[,] pwrIndex      = new int [12,11];
    public int[,] divIndex     = new int [12,11];
    public int[,] multIndex   = new int [12,11];
    public int[,] addIndex   = new int [12,11];
    public int[,] subIndex  = new int [12,11];
   
    // Recursion indexes for each symbol. Switches equivilent position to true if the exact slot is used
    public bool[,] lbrackRecIndex       = new bool [12,11];
    public bool[,] rbrackRecIndex      = new bool [12,11];
    public bool[,] rootstartRecIndex  = new bool [12,11];
    public bool[,] rootendRecIndex   = new bool [12,11];
    public bool[,] pwrRecIndex      = new bool [12,11];
    public bool[,] divRecIndex     = new bool [12,11];
    public bool[,] multRecIndex   = new bool [12,11];
    public bool[,] addRecIndex   = new bool [12,11];
    public bool[,] subRecIndex  = new bool [12,11];
   
     // variables used for computation
    private double pwrvar1 = -1;  // the x in (x^y)
    private double pwrvar2 = -1;  // the y in (x^y)
    private double divar1 = -1;   // the x in (x/y)
    private double divar2 = -1;   // the y in (x/y)
    private double multvar1 = -1; // the x in (x*y)
    private double multvar2 = -1; // the y in (x*y)
    private double addvar1 = -1;  // the x in (x+y)
    private double addvar2 = -1;  // the y in (x+y)
    private double subvar1 = -1;  // the x in (x-y)
    private double subvar2 = -1;  // the y in (x-y)
    public double solution = 0;   // the most recent solution
   
    // temp vaulues for storage
    public double tempdragstorage; //temporary drag storage, stores numeric value of currently dragged symbol
    public int TutorialDesc = 0;
    public bool TempMergeStorage; //unused
    public int TempEqStorage; // stores currently dragged eq type
    public int TempEqPosStorage; // current assigned equation row
   
    // misc
    public int pauseToggle = 0;
    public bool gameWon = false;

    //variables used for other game mechanics other than calculation
    public double record = 0; // high score in current game
    public double progressIndex = 0;
    public double PrevProgressIndex = 0;
    public double progressStage = 0.6; // the log base that the progress index is calculated at

    // sound effect
    [SerializeField] public AudioClip[] chalkSounds;

    // savedata serialization
    [SerializeField]
    private TextMeshProUGUI SourceDataText;
    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private TextMeshProUGUI SaveTimeText;
   // [SerializeField]
    //private TextMeshProUGUI LoadTimeText;

    private PlayerStats PlayerStats = new PlayerStats();
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled;
    
    //private long LoadTime;
    public float playTime; // play time
    public double resetMultiplyer = 1; // prestige multiplyer

    // Start is called before the first frame update :3
    void Start()
    {
        pausescreen.gameObject.SetActive(false);
        TutorialControl(false);
        debugMenu.gameObject.SetActive(false);
        PlayerStats = DataService.LoadData<PlayerStats>("/player-stats.json", EncryptionEnabled);
         // sets value based off of save file
        playTime = PlayerStats.playTime;
        resetMultiplyer = PlayerStats.resetMultiplyer;
        targetText.text = "" + resetMultiplyer + "\n↓\n" + (1 + Math.Sqrt(record / 250));
        if (resetMultiplyer > 1) // if player has prestiged
        {
            Intro.gameObject.SetActive(false);
        }
    }

    // credit for save/load file system: https://www.youtube.com/watch?v=mntS45g8OK4
    // load file
    public void SerializeJson()
    {
        if (DataService.SaveData("/player-stats.json", PlayerStats, EncryptionEnabled))
        {
            //SaveTimeText.SetText($"Save Time: {(SaveTime / TimeSpan.TicksPerMillisecond):N4}ms");
            //startTime = DateTime.Now.Ticks;
            try // ran if file found
            {
                PlayerStats data = DataService.LoadData<PlayerStats>("/player-stats.json", EncryptionEnabled);
                InputField.text = "Loaded from file:\r\n" + JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            catch
            {
                Debug.LogError($"Could not read file!");
                InputField.text = "<color=#ff0000>Error reading save file!</color>";
            }
        }
        else
        {
            Debug.LogError("Could not save file!");
            InputField.text = "<color=#ff0000>Error saving data!</color>";
        }
    }

    void Awake()
    {
        SourceDataText.SetText(JsonConvert.SerializeObject(PlayerStats, Formatting.Indented));
    }

    // clears save data and resets scene
    public void ClearData()
    {
        string path = Application.persistentDataPath + "/player-stats.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            InputField.text = "";
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
    // used to spawn symbols for the player to use. called each iteration of a for-loop
    public void GetSymbol (int symbolVal)
    {
        switch (symbolVal)
        {
            case 1:
                Instantiate(one, StorageSlot);
                break;
               
            case 2:
                Instantiate(two, StorageSlot);
                break;
               
            case 3:
                Instantiate(three, StorageSlot);
                break;
               
            case 4:
                Instantiate(four, StorageSlot);
                break;
               
            case 5:
                Instantiate(five, StorageSlot);
                break;
        }
        if (symbolVal < 5)
        {
            Random rnd = new Random();
            // 1/10 chance
            if (rnd.Next(10) == 1)
            {
                Instantiate(addition, StorageSlot);
            }
        }
        else
        {
            Random rnd = new Random();
            // 1/25 chance
            if (rnd.Next(25) == 1)
            {
                Instantiate(multiplication, StorageSlot);
            }
        }
    }

    // Called when player clicks the prestige button.
    // If the given reset multiplyer is higher than the current one it writes the new value to the file and restarts the scene.
    // On start() the value is determined by the file.
    public void prestige()
    {
        if (1 + Math.Sqrt(record / 250) > resetMultiplyer)
        {
             resetMultiplyer = 1 + Math.Sqrt(record / 250);
            SourceDataText.text = "{\n\"resetMultiplyer\": "+resetMultiplyer+"\n}";
            PlayerStats.resetMultiplyer = resetMultiplyer;
            PlayerStats.playTime = playTime;
            SerializeJson();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            StartCoroutine(ResetWarning());
        }
    }

    // Called if player attempts to prestige to get a lower resetmult than they already have
    IEnumerator ResetWarning()
    {
        prestigeWarning.text = "It would be silly to do that, you would just waste time by decreasing the multiplyer.\nIf you're trying to get yourself out of a softlock then go to settings in the pause screen.";
        yield return new WaitForSecondsRealtime(12);
        prestigeWarning.text = "";
    }
   
    //debug menu option that forces prestige
    public void ForcePrestige()
    {
        resetMultiplyer = 1 + Math.Sqrt(record / 250);
        SourceDataText.text = "{\n\"resetMultiplyer\": "+resetMultiplyer+"\n}";
        PlayerStats.resetMultiplyer = resetMultiplyer;
        PlayerStats.playTime = playTime;
        SerializeJson();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // reloads scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
    // this is where game progression is controlled, ran just before temp values are reset
    public void gameProgression()
    {
        if (solution > record) // update record
        {
            record = solution;
            SoundFXManager.instance.PlayRandomSoundFXClip(chalkSounds, transform, 1f);
        }
        if (record >= 100000)
        {
            EquationUnlock(5);
            if (gameWon == false)
            {
                WinScreen.SetActive(true);
                gameWon = true;
                endTime.SetText($"Play Time: {(System.TimeSpan.FromSeconds((int)playTime).ToString())}");
            }
        }
        else if (record >= 25000){EquationUnlock(4);}
        else if (record >= 10000){EquationUnlock(3);}
        else if (record >= 3000)
        {
            progressIndex = Math.Pow(record, 0.5) * resetMultiplyer + 70;
            EquationUnlock(2);
        }
        if(record >= 970)
        {
            progressIndex = Math.Pow(record, 0.5) * resetMultiplyer + 70;
            EquationX1.gameObject.SetActive(true);
        }
        else if (record >= 500)
        {
            EquationX1.gameObject.SetActive(true);
            progressIndex = Math.Pow(record, 0.75) * resetMultiplyer + 20;
        }
        else if (record >= 20){progressIndex = Math.Pow(record, 0.75) * resetMultiplyer + 20;}
        else if (record >= 8)
        {
            progressIndex = (Math.Pow(record, 0.75) * resetMultiplyer + 20);
        }
        else {progressIndex = (Math.Pow(1.7, record - 1)) * resetMultiplyer;}
        progressIndex = Math.Round(progressIndex);
        if (progressIndex > PrevProgressIndex)
            {
                if (resetMultiplyer > 16)
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {  
                        if (prev % 16 == 0) // gives sixteenth, but 16x value; saves time with crafting while providing same value
                        {
                            GetSymbol(5);
                        }
                    }
                }
                else if (resetMultiplyer > 10)
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {  
                        if (prev % 8 == 0) // gives eighth, but 8x value
                        {
                            GetSymbol(4);
                        }
                    }
                }
                else if (resetMultiplyer > 4)
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {  
                        if (prev % 4 == 0) // gives quarter, but 4x value
                        {
                            GetSymbol(3);
                        }
                    }
                }
                else if (record > 200 || resetMultiplyer > 2)
                {
                    for (int prev = (int) PrevProgressIndex; prev < (int) progressIndex; prev++)
                    {  
                        if (prev % 2 == 0) // gives half, but double value
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
                symbolClaimText.text = "[SYMBOL CLAIM / AMOUNT: "+StorageSlot.transform.childCount+" ]";
            }
        if (record >= 2)
        {
            Intro.gameObject.SetActive(false);
        }
    }
   
    // makes unlocked equations active
    public void EquationUnlock(int upToX)
    {
        switch(upToX)
        {
            case 1:
                EquationX1.gameObject.SetActive(true);
                break;
               
            case 2:
                EquationX1.gameObject.SetActive(true);
                EquationX2.gameObject.SetActive(true);
                break;
               
            case 3:
                EquationUnlock(2);
                EquationX3.gameObject.SetActive(true);
                break;
               
            case 4:
                EquationUnlock(3);;
                EquationX4.gameObject.SetActive(true);
                break;
               
            case 5:
                EquationUnlock(4);
                EquationX5.gameObject.SetActive(true);
                break;
               
            case 6:
                EquationUnlock(5);
                EquationX6.gameObject.SetActive(true);
                break;
        }
    }

    // Runs the math function
    public void UpdateScore()
    {
        Mathfunc(1, 10, false, 1);
    }

    // updates debug menu along with some other display text
    public void slotposUpd()
    {
        slotpos1Text.text = "";
        for (int i = 0; i < 8; i++) //for each row
        {
            for(int k = 1; k < 11; k++) // for each element
            {
                slotpos1Text.text += slotpos[i,k] + " ";
            }
            slotpos1Text.text += "\n";
        }
       
        eqSlotText.text = "row 0 |" + eqIndex[0] + "|" + solutionIndex[0]
        + "|\nrow 1 |" + eqIndex[1] + "|" + solutionIndex[1]
        + "|\nrow 2 |" + eqIndex[2] + "|" + solutionIndex[2]
        + "|\nrow 3 |" + eqIndex2[3] + "|" + solutionIndex2[3]
        + "|\nrow 4 |" + eqIndex2[4] + "|" + solutionIndex2[4]
        + "|\nrow 5 |" + eqIndex2[5] + "|" + solutionIndex2[5];

        pwrslotText.text = "     -- internal symbol slot data --\n---------------------------------------\n lbrack\n";
        for (int i = 1; i < 5; i++) // Left Bracket
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += lbrackIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n rbrack\n";
        for (int i = 1; i < 5; i++) // Right Bracket
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += rbrackIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n rootstart\n";
        for (int i = 1; i < 5; i++) // Root Start
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += rootstartIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n rootend\n";
        for (int i = 1; i < 5; i++) // Root end
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += rootendIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n exponentiation\n";
        for (int i = 1; i < 5; i++) // Power
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += pwrIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n division\n";
        for (int i = 1; i < 5; i++) // Division
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += divIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n multiplication\n";
        for (int i = 1; i < 5; i++) // Multiplication
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += multIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n addition\n";
        for (int i = 1; i < 5; i++) // Addition
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += addIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------\n subtraction\n";
        for (int i = 1; i < 5; i++) // Subtraction
        {
            pwrslotText.text += $"row {i}: ";
            for(int k = 1; k < 11; k++)
            {
                pwrslotText.text += subIndex[i,k] + " ";
            }
            pwrslotText.text += "\n";
        }
        pwrslotText.text += "---------------------------------------";
        rootstartText.text = "-- misc data --\nnumber of pauses: " + pauseToggle + "\nreset multiplyer: " + resetMultiplyer + "\ncurrent Equation slot: [" + TempEqPosStorage + "]\neq type select: " + TempEqStorage + "\nrecord: " + record;
        pauseCountText.text = "\t-- crafting slot data --\n---------------------------------------\n"
        + "\nValue:\t\t" + craftIndex[0, 0] + "\t" + craftIndex[0,1]
        + "\nAddition:\t\t" + craftIndex[1, 0] + "\t" + craftIndex[1,1]
        + "\nSubtraction:\t" + craftIndex[2, 0] + "\t" +craftIndex[2,1]
        + "\nMultiplication\t" + craftIndex[3, 0]+"\t" + craftIndex[3,1]
        + "\nDivision:\t\t" + craftIndex[4, 0] + "\t" + craftIndex[4,1]
        + "\nExponentiation:\t" + craftIndex[5, 0] + "\t" + craftIndex[5,1]
        + "\nLeft Bracket:\t" + craftIndex[6, 0] + "\t" + craftIndex[6,1]
        + "\nRight Bracket:\t" + craftIndex[7, 0] + "\t" + craftIndex[7,1]
        + "\nRoot Start:\t" + craftIndex[8, 0] + "\t" + craftIndex[8,1]
        + "\nRoot End:\t\t" + craftIndex[9, 0] + "\t" + craftIndex[9,1]
        + "\n---------------------------------------";
        targetText.text = "" + resetMultiplyer + "\n↓\n" + (1 + Math.Sqrt(record / 250));
        symbolClaimText.text = "[SYMBOL CLAIM / AMOUNT: " + StorageSlot.transform.childCount + " ]";
    }

 public void EqReset(int CurrentEq)
    {  
        Debug.Log("Resetting Slot Data for [" + CurrentEq + ", 1-10]");
        for (int i = 1; i <= 10; i++)
        {
            slotpos[CurrentEq, i] = slotposStored[CurrentEq, i];
        }
        for (int k = 0; k <= 5; k++)
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
        for (int i = 0; i <= 5; i++)
            {
                for (int k = 1; k <= 10; k++)
                {
                    slotposStored[i, k] = slotpos[i,k];
                }
            }
    }

     // Calculator. param describes the slot where it starts, the slot where it ends, if it is being called recursively, the row that it calls
    public void Mathfunc(int startpos, int endpos, bool IsRecCall, int eqCalled)
    {  
        if (IsRecCall == false)
        {
            Debug.Log("                         //---------- Mathfunc Called ----------\\\\");
        }
        else
        {
            Debug.Log("      //--- Called Recursively ---\\\\");
        }
        Debug.Log("calling slots: " + startpos + " to " + endpos);
        Debug.Log("called recursively?: " + IsRecCall);
        //Debug.Log("Equation SLOT called: " + eqCalled + " // Equation TYPE called: " + eqIndex[eqCalled]);
        switch (eqIndex2[eqCalled]) //checks for current eq slot
        {
            case 0: //normal type
            {
                if(IsRecCall == false) // if ran non-recursively
                {
                    EqPreset();
                }

                // BRACKETS
                for (int index = startpos; index < endpos; index++) // check all possible positions within bounds for left brackets
                {
                    if (lbrackIndex[eqCalled, index] > 0 && lbrackRecIndex[eqCalled, index] == false) //if checked slot has a left bracket and has not been ran recursively
                    {
                        Debug.Log("lbrack found at slot"+index);
                        for (int index2 = index + 1; index2 <= endpos; index2++) // if a vaild left bracket position is found, check all for right brackets, starting at the left bracket position to end
                        {
                            if (rbrackIndex[eqCalled, index2] > 0 && rbrackRecIndex[eqCalled, index2] == false) // if checked position has a right bracket in it and has not been done before
                            {
                                Debug.Log("rbrack found at slot"+index2);
                                lbrackRecIndex[eqCalled, index] = rbrackRecIndex[eqCalled, index2] = true; // updates what has been solved for recursion
                                Mathfunc(lbrackIndex[eqCalled, index] + 1, rbrackIndex[eqCalled, index2] - 1, true, eqCalled); // call MathFunction recursively, only solving within brackets
                                Debug.Log("post brack TEST =" + solution);
                                slotpos[eqCalled, rbrackIndex[eqCalled, index2]] = slotpos[eqCalled, lbrackIndex[eqCalled, index]] = solution; // MathFunc output solution is recorded to bracket numerical values for other operators
                                Debug.Log("//BRACKETS DONE)");
                                Debug.Log("post brack =" + solution);
                            }
                        }
                    }
                }
           
                // EXPONENTS
                for (int expcheck = startpos; expcheck <= endpos; expcheck++) // check each possible position within bounds for rootstart AND power
                {
                    if (rootstartIndex[eqCalled, expcheck] > 0 || pwrIndex[eqCalled, expcheck] > 0) // if either of the two exponents are found
                    {
                        if (rootstartIndex[eqCalled, expcheck] > pwrIndex[eqCalled, expcheck]) // statement fufilled if root found first, falls though to if else statement if power found first
                        {
                            Debug.Log("Root Found");
                            if (rootstartIndex[eqCalled, expcheck] > 0 && rootstartRecIndex[eqCalled, expcheck] == false) // ROOT FIRST
                            {
                                for (int index2 = startpos; index2 <= endpos; index2++)
                                {
                                    Debug.Log("index = "+index2);
                                    if (rootendIndex[eqCalled, index2] > 0 && rootendRecIndex[eqCalled, expcheck] == false)
                                    {
                                        rootstartRecIndex[eqCalled, expcheck] = true; //record found rootstart into recusion index
                                        rootendRecIndex[eqCalled, index2] = true; //record found rootend into recusion index
                                        if (expcheck + 1 != index2 - 1) // check if root start and end are NOT 1 slot apart
                                        {
                                            Mathfunc(rootstartIndex[eqCalled, expcheck] + 1, rootendIndex[eqCalled, index2] - 1, true, eqCalled); //call calculator recursively with the bounds within the root.
                                        }
                                        else // falls through to this if they are 1 slot apart
                                        {
                                            solution = slotpos[eqCalled, expcheck + 1]; // gets value of slot in-between
                                        }
                                        solution = Math.Sqrt(solution); // once solved
                                        slotpos[eqCalled, rootendIndex[eqCalled, index2]] = slotpos[eqCalled, rootstartIndex[eqCalled, expcheck]] = solution; // surrounding rootstart and rootend also assigned solution to allow other symbols to calculate off of this solution
                                        Debug.Log("//SQUARE ROOT DONE");
                                        Debug.Log(solution);
                                    }
                                }
                            }
                        }
                        else if (rootstartIndex[eqCalled, expcheck] < pwrIndex[eqCalled, expcheck]) // POWER FIRST
                        {
                            for (int index = startpos; index < endpos; index++) // check each slot within bounds for power symbol
                            {
                                if (pwrIndex[eqCalled, index] > 0 && pwrIndex[eqCalled, index] > startpos && pwrIndex[eqCalled, index] < endpos && pwrRecIndex[eqCalled, index] == false) // if power found and within bounds and not already done in recursion index
                                {
                                    Debug.Log("exponentiation: [" + eqCalled + ", " + pwrIndex[eqCalled, index] + "]");
                                    Debug.Log(slotpos[eqCalled, pwrIndex[eqCalled, index] - 1] + " ^ " + slotpos[eqCalled, pwrIndex[eqCalled, index] + 1]);
                                    pwrRecIndex[eqCalled, index] = true;
                                    pwrvar1 = slotpos[eqCalled, pwrIndex[eqCalled, index] - 1]; // pwrvar1 = slot to the left of pwr symbol
                                    pwrvar2 = slotpos[eqCalled, pwrIndex[eqCalled, index] + 1]; // above but to the right
                                    solution = slotpos[eqCalled, pwrIndex[eqCalled, index] - 1] = slotpos[eqCalled, pwrIndex[eqCalled, index]] = slotpos[eqCalled, pwrIndex[eqCalled, index] + 1] = Math.Pow(pwrvar1, pwrvar2); //sets all 3 involved slots to solution after solving
                                    Debug.Log("exponentiation: " + solution);
                                    Debug.Log("POWER DONE");
                                }
                            }
                        }
                    }
                }
                // BASIC OPERATORS: since they are identical, I will only comment division
                // DIVISION
                for (int index = startpos; index < endpos; index++) // check every slot inbounds for division
                {
                    if (divIndex[eqCalled, index] > 0 && divIndex[eqCalled, index] > startpos && divIndex[eqCalled, index] < endpos && divRecIndex[eqCalled, index] == false) // if checked position has one, is more than startpos and less than endpos and recursion matrix says false, then it returns true
                    {
                        Debug.Log("division: [" + eqCalled + ", " + divIndex[eqCalled, index] + "]");
                        Debug.Log(slotpos[eqCalled, divIndex[eqCalled, index] - 1] + " / " + slotpos[eqCalled, divIndex[eqCalled, index] + 1]);
                        divRecIndex[eqCalled, index] = true; // record division slot to recursion index
                        divar1 = slotpos[eqCalled, divIndex[eqCalled, index] - 1]; // find slotval to the left of division symbol
                        divar2 = slotpos[eqCalled, divIndex[eqCalled, index] + 1]; // find slotval to the right of division symbol
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
                    if (addIndex[eqCalled, index] > 0 && addIndex[eqCalled, index] > startpos && addIndex[eqCalled, index] < endpos && addRecIndex[eqCalled, index] == false)
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
                if (IsRecCall) // end of function if called recursively
                {
                    Debug.Log("      \\\\--- Recursion Done ---//");
                }
                if (IsRecCall == false) // ran if eq solved and all recursion done
                {
                    if (double.IsInfinity(solution) || double.IsNaN(solution)) // error-handling for NaNs and Infinities
                    {
                        solution = 0;
                    }
                    solution = (eqCalled < 6) ? solutionIndex2[eqCalled] = solution : solutionIndex3[eqCalled] = solution; // error handling for broken array
                    Debug.Log("                 \\\\---------- Mathfunc Done ----------//");
                    solution = (solutionIndex2[1] + solutionIndex2[2] + solutionIndex2[3] + solutionIndex2[4] + solutionIndex2[5] + solutionIndex3[6] + solutionIndex3[7] + solutionIndex3[8] + solutionIndex3[9]);
                    Debug.Log("solution: " + solution);
                    solutionText.text = "" + solution;
                    slotposUpd(); //updates debug menu
                    gameProgression(); //gives player symbols
                    EqReset(eqCalled); //resets slot values, must be done last
                }
                break;
            }
            case 1:
            {
                Debug.Log("no equation found");
                break;
            }
        }
        if ((eqCalled + 1) < 10 && IsRecCall == false) // calls next equation row
        {
            Debug.Log("//////////////////////////////////////////////////////////////// Next Equation Start");
            Mathfunc(1, 10, false, eqCalled + 1);
        }              
    }
   
    // tutorial messages, triggered by holding [h], probably a better way to do this but it doesnt really matter
    public void TutorialControl(bool state)
    {
        if (state)
        {
            tutText.gameObject.SetActive(true);
            switch (TutorialDesc)
            {
                case 1: // Addition
                    tutText.text = "Addition:\nThe next symbol is the amount of times the previous symbol is incremented.";
                    break;
                   
                case 2: // Multiplication
                    tutText.text = "Multiplication:\nThe next symbol is the amount of times the previous symbol is added to itself.";
                    break;
                   
                case 3: // Division
                    tutText.text = "Division:\nReverse of multiplication; outputs the amount of times the next symbol is contained in the previous symbol.";
                    break;
                   
                case 4: // Subtraction
                    tutText.text = "Subtraction:\nThe next symbol is the amount of times the previous symbol is decremented.";
                    break;
                   
                case 5: // Exponentiation
                    tutText.text = "Exponentiation:\nThe next symbol is the amount of times the previous symbol is multiplied by itself.";
                    break;
                   
                case 6: // Square Root
                    tutText.text = "Square root:\nOutputs the number that when multiplied by itself equals the solution to the slots between this symbol and the root end.";
                    break;
                   
                case 7: // Right Bracket
                    tutText.text = "Right bracket:\nThe slots between this symbol and the left bracket are solved first";
                    break;
                   
                case 8: // Left Bracket
                    tutText.text = "Left bracket:\nThe slots between this symbol and the right bracket are solved first";
                    break;
                   
                default:
                    break;
            }
        }
        else
        {    
            tutText.gameObject.SetActive(false);
        }
    }
   
    // quits to desktop
    public void QuitToDesktop()
    {
        Debug.Log("!!! QUIT CALLED !!!");
        PlayerStats.playTime = playTime;
        Application.Quit();
    }
    
    // debug menu controls
    public void DebugControl(bool TildaDown)
    {
        debugMenu.gameObject.SetActive(TildaDown);
        slotposUpd();
    }
   
    // pause menu controls
    public void PauseControl(bool EscToggle)
    {
        pausescreen.gameObject.SetActive(EscToggle);
    }

    // Update is called once per frame :3
    void Update()
    {
        playTime = Time.timeSinceLevelLoad + PlayerStats.playTime; // gets playtime
        SaveTimeText.SetText($"Play Time: {(System.TimeSpan.FromSeconds((int)playTime).ToString())}"); // updates playtime display
        if (Input.GetKeyDown(KeyCode.BackQuote)) {DebugControl(true);} // debug key down
        if (Input.GetKeyUp(KeyCode.BackQuote)) {DebugControl(false);} // debug key up
        if (Input.GetKeyDown(KeyCode.H)) { TutorialControl(true);} // tutorial key down
        if (Input.GetKeyUp(KeyCode.H)) { TutorialControl(false);} // tutorial key up
        if (Input.GetKeyDown(KeyCode.Return)) {Mathfunc(1, 10, false, 1);} // Solve hotkey
        if (Input.GetKeyDown(KeyCode.Escape)) // if odd amt of esc presses then it pauses
        {
            pauseToggle++;
            if(pauseToggle % 2 == 0)
            {
                PauseControl(false);
            }
            else
            {
                PauseControl(true);
            }
        }
        if (TrashSlot.transform.childCount == 1) // removes symbol from crafting slot
        {
            Destroy(TrashSlot.transform.GetChild(0).gameObject);
        }
    }
}