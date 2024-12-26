public class InGameUI : BindUI
{
    private InventoryPanel inventoryPanel;
    private InventorySetPanel inventorySetPanel;
    private ItemInformationPanel itemInformationPanel;
    private StatusBarPanel statusBarPanel;
    private StatusInformationPanel statusInformationPanel;
    private InGameMenuPanel inGameMenuPanel;
    private EnemyStatusPanel enemyStatusPanel;


    public InventoryPanel InventoryPanel { get { return inventoryPanel; } }
    public InventorySetPanel InventorySetPanel { get { return inventorySetPanel; } }

    public ItemInformationPanel ItemInformationPanel { get { return itemInformationPanel; } }

    public StatusBarPanel StatusBarPanel { get { return statusBarPanel; } }

    public StatusInformationPanel StatusInformationPanel { get { return statusInformationPanel; } }

    public InGameMenuPanel InGameMenuPanel { get { return inGameMenuPanel; } }

    public EnemyStatusPanel EnemyStatusPanel { get { return enemyStatusPanel; } }

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

    }
    private void Start()
    {

        inventorySetPanel.gameObject.SetActive(false);
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
        if (InGameMenuPanel.gameObject.activeSelf)
            return;

        CurrentMenu = inventorySetPanel;
        CurrentMenu.OpenUIPanel();


    }



}
