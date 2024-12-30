using UnityEngine;

public class InGameUI : BindUI
{
    private InventoryPanel inventoryPanel;
    private InventorySetPanel inventorySetPanel;
    private ItemInformationPanel itemInformationPanel;
    private StatusBarPanel statusBarPanel;
    private StatusInformationPanel statusInformationPanel;
    private InGameMenuPanel inGameMenuPanel;
    private EnemyStatusPanel enemyStatusPanel;
    private OptionPanel optionPanel;


    public InventoryPanel InventoryPanel { get { return inventoryPanel; } }
    public InventorySetPanel InventorySetPanel { get { return inventorySetPanel; } }

    public ItemInformationPanel ItemInformationPanel { get { return itemInformationPanel; } }

    public StatusBarPanel StatusBarPanel { get { return statusBarPanel; } }

    public StatusInformationPanel StatusInformationPanel { get { return statusInformationPanel; } }

    public InGameMenuPanel InGameMenuPanel { get { return inGameMenuPanel; } }

    public EnemyStatusPanel EnemyStatusPanel { get { return enemyStatusPanel; } }

    public OptionPanel OptionPanel { get { return optionPanel; } }

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
        optionPanel = GetUI<OptionPanel>("OptionPanel");
    }
    private void Start()
    {

   
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
