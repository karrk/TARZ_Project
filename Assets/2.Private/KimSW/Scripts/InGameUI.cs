using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InGameUI : BindUI
{
    [SerializeField] LoadingScript loading;

    [Inject] private ItemInventory itemInventory;

    #region
    private InventoryPanel inventoryPanel;
    private InventorySetPanel inventorySetPanel;
    
    private ItemInformationPanel itemInformationPanel;
    
    private StatusBarPanel statusBarPanel;
    private StatusInformationPanel statusInformationPanel;
    private InGameMenuPanel inGameMenuPanel;
    private EnemyStatusPanel enemyStatusPanel;
    private OptionPanel optionPanel;
    private MenualPanel menualPanel;
    private AlertText alertText;
    private TargetIndicator targetIndicator;
    private EquipmentGetPanel equipmentGetPanel;
    private EquipmentSelectPanel equipmentSelectPanel;
    private BlueChipGetPanel blueChipGetPanel;
    private BlueChipSelectPanel blueChipSelectPanel;

    private PassiveShopPanel passiveShopPanel;

    private EquipmentManager equipmentManager;

    private EquipmentBackpackPanel equipmentBackpackPanel;

    private GameOverPanel gameOverPanel;

    public InventoryPanel InventoryPanel { get { return inventoryPanel; } }
    public InventorySetPanel InventorySetPanel { get { return inventorySetPanel; } }

    public ItemInformationPanel ItemInformationPanel { get { return itemInformationPanel; } }
    
    public StatusBarPanel StatusBarPanel { get { return statusBarPanel; } }

    public StatusInformationPanel StatusInformationPanel { get { return statusInformationPanel; } }

    public InGameMenuPanel InGameMenuPanel { get { return inGameMenuPanel; } }

    public EnemyStatusPanel EnemyStatusPanel { get { return enemyStatusPanel; } }

    public OptionPanel OptionPanel { get { return optionPanel; } }
    public MenualPanel MenualPanel { get { return menualPanel; } }
    public AlertText AlertText { get { return alertText; } }
    public TargetIndicator TargetIndicator { get { return targetIndicator; } }

    public EquipmentGetPanel EquipmentGetPanel { get { return equipmentGetPanel; } }

    public EquipmentSelectPanel EquipmentSelectPanel { get { return equipmentSelectPanel; } }

    public EquipmentManager EquipmentManager { get { return equipmentManager; } }

    public BlueChipGetPanel BlueChipGetPanel { get { return blueChipGetPanel; } }
    public BlueChipSelectPanel BlueChipSelectPanel { get { return blueChipSelectPanel; } }

    public PassiveShopPanel PassiveShopPanel { get { return passiveShopPanel; } }

    public EquipmentBackpackPanel EquipmentBackpackPanel { get { return equipmentBackpackPanel; } }

    public GameOverPanel GameOverPanel { get { return gameOverPanel; } }
    #endregion

    private IOpenCloseMenu currentMenu;

    public IOpenCloseMenu CurrentMenu { get { return currentMenu; } set { currentMenu = value; } }


    // 인풋
 
    public InputActionReference cancelRef;
    public InputActionReference interRef;
    public InputActionReference invenRef;
    public InputActionReference menuRef;

    private void OnEnable()
    {
        cancelRef.action.performed += CancelInput;
       
        invenRef.action.performed += InvenInput;

        menuRef.action.performed += MenuInput;
    }

    private void OnDisable()
    {
        cancelRef.action.performed -= CancelInput;
      
        invenRef.action.performed -= InvenInput;

        menuRef.action.performed -= MenuInput;
    }

    private void Awake()
    {
        Bind();
        InitPanel();
    }

    void InitPanel()
    {
        //inventoryPanel = GetUI<InventoryPanel>("InventoryPanel");
        //inventorySetPanel = GetUI<InventorySetPanel>("InventorySetPanel");
        //itemInformationPanel = GetUI<ItemInformationPanel>("ItemInformationPanel");
        statusBarPanel = GetUI<StatusBarPanel>("StatusBarPanel");
        statusInformationPanel = GetUI<StatusInformationPanel>("StatusInformationPanel");
        inGameMenuPanel = GetUI<InGameMenuPanel>("InGameMenuPanel");
        enemyStatusPanel = GetUI<EnemyStatusPanel>("EnemyStatusPanel");
        targetIndicator = GetUI<TargetIndicator>("TargetIndicator");
        optionPanel = GetUI<OptionPanel>("OptionPanel");
        menualPanel = GetUI<MenualPanel>("MenualPanel");
        alertText = GetUI<AlertText>("AlertText");
        equipmentGetPanel = GetUI<EquipmentGetPanel>("EquipmentGetPanel");
        equipmentSelectPanel = GetUI<EquipmentSelectPanel>("EquipmentSelectPanel");
        equipmentManager = GetUI<EquipmentManager>("EquipmentManager");
        blueChipGetPanel = GetUI<BlueChipGetPanel>("BlueChipGetPanel");
        blueChipSelectPanel = GetUI<BlueChipSelectPanel>("BlueChipSelectPanel");

        passiveShopPanel = GetUI<PassiveShopPanel>("PassiveShopPanel");

        equipmentBackpackPanel = GetUI<EquipmentBackpackPanel>("EquipmentBackpackPanel");
        gameOverPanel = GetUI<GameOverPanel>("GameOverPanel");
    }

    private void Start()
    {
       // itemInventory.OnGetItem += (num, sprite) => { InventoryPanel.GetItem(num, sprite); };
       // itemInventory.OnChangeStatusInfo += () => { StatusInformationPanel.UpdateStatusInfo(); };

        currentMenu = statusBarPanel;

    }

    public void MenuInput(InputAction.CallbackContext value)
    {
        if (currentMenu is not null)
        {
            if (CurrentMenu.Equals(StatusBarPanel)) { 
                CurrentMenu.CloseUIPanel();
            }
        }
    }

    public void CancelInput(InputAction.CallbackContext value)
    {
        if (currentMenu is not null)
        {
            if (!CurrentMenu.Equals(StatusBarPanel))
            {
                CurrentMenu.CloseUIPanel();
            }
        }
    }
  
    public void InvenInput(InputAction.CallbackContext value)
    {
        if (CurrentMenu.Equals(StatusBarPanel))
        {
            CurrentMenu = EquipmentBackpackPanel;
            CurrentMenu.OpenUIPanel();
        }
        else if(CurrentMenu.Equals(EquipmentBackpackPanel))
        {
            EquipmentBackpackPanel.ChangeSelect();
        }

    }

    public void OnGameOver(bool isWin)
    {
        

        if (!CurrentMenu.Equals(StatusBarPanel))
        {
            CurrentMenu.CloseUIPanel();

        }

        InGameMenuPanel.gameObject.SetActive(false);
        StatusBarPanel.gameObject.SetActive(false);

        CurrentMenu = GameOverPanel;
        GameOverPanel.SetGameoverText(isWin);
        CurrentMenu.OpenUIPanel();
    }

   

    /*
    public void OnInventory()
    {
        if (CurrentMenu.Equals(StatusBarPanel) == false)
            return;

     

        CurrentMenu = inventorySetPanel;
        CurrentMenu.OpenUIPanel();

    }
    */
    public void OnEnemyHP()
    {
        if (enemyStatusPanel.EnemyHpViewCheck())
        {
            enemyStatusPanel.gameObject.SetActive(true);
        }
    }
    public void OffEnemyHP()
    {
        enemyStatusPanel.gameObject.SetActive(false);

    }

    /// <summary>
    /// 적 체력 초기화
    /// </summary>
    /// <param name="hp"></param>
    public void InitEnemyHP(float hp)
    {
        enemyStatusPanel.SetEnemyHP(hp);
        OnEnemyHP();
    }




    public void GoToLobby()
    {
        loading.Loading("FixTestRobi");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    /*
    private void Update()
    {

    // 적 체력 초기화
        if (Input.GetKeyDown(KeyCode.C))
        {
            InitEnemyHP(100);
        }
    // 적에게 데미지 가하기
        if (Input.GetKeyDown(KeyCode.B))
        {
            EnemyStatusPanel.hpView.Hp.Value -= 30;
        }

    }
    */
}
