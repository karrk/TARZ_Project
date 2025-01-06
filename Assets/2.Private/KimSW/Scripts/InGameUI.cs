using UnityEngine;
using Zenject;

public class InGameUI : BindUI
{
    [Inject] private ItemInventory itemInventory;

    private InventoryPanel inventoryPanel;
    private InventorySetPanel inventorySetPanel;
    private ItemInformationPanel itemInformationPanel;
    private StatusBarPanel statusBarPanel;
    private StatusInformationPanel statusInformationPanel;
    private InGameMenuPanel inGameMenuPanel;
    private EnemyStatusPanel enemyStatusPanel;
    private OptionPanel optionPanel;
    private TargetIndicator targetIndicator;
    private EquipmentGetPanel equipmentGetPanel;
    private EquipmentSelectPanel equipmentSelectPanel;


    public InventoryPanel InventoryPanel { get { return inventoryPanel; } }
    public InventorySetPanel InventorySetPanel { get { return inventorySetPanel; } }

    public ItemInformationPanel ItemInformationPanel { get { return itemInformationPanel; } }

    public StatusBarPanel StatusBarPanel { get { return statusBarPanel; } }

    public StatusInformationPanel StatusInformationPanel { get { return statusInformationPanel; } }

    public InGameMenuPanel InGameMenuPanel { get { return inGameMenuPanel; } }

    public EnemyStatusPanel EnemyStatusPanel { get { return enemyStatusPanel; } }

    public OptionPanel OptionPanel { get { return optionPanel; } }

    public TargetIndicator TargetIndicator { get { return targetIndicator; } }

    public EquipmentGetPanel EquipmentGetPanel { get { return equipmentGetPanel; } }

    public EquipmentSelectPanel EquipmentSelectPanel { get { return equipmentSelectPanel; } }


    private IOpenCloseMenu currentMenu;

    public IOpenCloseMenu CurrentMenu { get { return currentMenu; } set { currentMenu = value; } }

    private void Awake()
    {
        Bind();
        inventoryPanel = GetUI<InventoryPanel>("InventoryPanel");
        inventorySetPanel = GetUI<InventorySetPanel>("InventorySetPanel");
        itemInformationPanel = GetUI<ItemInformationPanel>("ItemInformationPanel");
        statusBarPanel = GetUI<StatusBarPanel>("StatusBarPanel");
        statusInformationPanel = GetUI<StatusInformationPanel>("StatusInformationPanel");
        inGameMenuPanel = GetUI<InGameMenuPanel>("InGameMenuPanel");
        enemyStatusPanel = GetUI<EnemyStatusPanel>("EnemyStatusPanel");
        targetIndicator = GetUI<TargetIndicator>("TargetIndicator");
        optionPanel = GetUI<OptionPanel>("OptionPanel");
        equipmentGetPanel = GetUI<EquipmentGetPanel>("EquipmentGetPanel");
        equipmentSelectPanel = GetUI<EquipmentSelectPanel>("EquipmentSelectPanel");
    }
    private void Start()
    {
        itemInventory.OnGetItem += (num, sprite) => { InventoryPanel.GetItem(num, sprite); };
        itemInventory.OnChangeStatusInfo += () => { StatusInformationPanel.UpdateStatusInfo(); };

        currentMenu = statusBarPanel;

    }


    public void InputCancel()
    {
        if (currentMenu is not null)
        {
            CurrentMenu.CloseUIPanel();

        }
    }

    public void OnInventory()
    {
        if (CurrentMenu.Equals(StatusBarPanel) == false)
            return;

     

        CurrentMenu = inventorySetPanel;
        CurrentMenu.OpenUIPanel();



    }

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
    public void InitEnemyHP(int hp)
    {
        enemyStatusPanel.SetEnemyHP(hp);
        OnEnemyHP();
    }

    /*
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            InitEnemyHP(100);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            enemyStatusPanel.hpView.Hp.Value -= 30;
        }

    }
    */
}
