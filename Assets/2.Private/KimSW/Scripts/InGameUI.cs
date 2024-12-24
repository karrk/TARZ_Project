public class InGameUI : BindUI
{
    private InventoryPanel inventoryPanel;
    private InventorySetPanel inventorySetPanel;
    private ItemInformationPanel itemInformationPanel;
    private PlayerStatusBarPanel playerStatusBarPanel;
    private StatusInformationPanel statusInformationPanel;
    private InGameMenuPanel inGameMenuPanel;
  

    public InventoryPanel InventoryPanel { get { return inventoryPanel; } }
    public InventorySetPanel InventorySetPanel { get { return inventorySetPanel; } }

    public ItemInformationPanel ItemInformationPanel { get { return itemInformationPanel; } }

    public PlayerStatusBarPanel PlayerStatusBarPanel { get { return playerStatusBarPanel; } }

    public StatusInformationPanel StatusInformationPanel { get { return statusInformationPanel; } }

    public InGameMenuPanel InGameMenuPanel { get { return inGameMenuPanel; } }


    private IOpenCloseMenu currentMenu;

    public IOpenCloseMenu CurrentMenu { get { return currentMenu; } set { currentMenu = value; } }

    private void Awake()
    {
        Bind();
        inventoryPanel = GetUI<InventoryPanel>("InventoryPanel");
        inventorySetPanel = GetUI<InventorySetPanel>("InventorySetPanel");
        itemInformationPanel = GetUI<ItemInformationPanel>("ItemInformationPanel");
        playerStatusBarPanel = GetUI<PlayerStatusBarPanel>("PlayerStatusBarPanel");
        statusInformationPanel = GetUI<StatusInformationPanel>("StatusInformationPanel");
        inGameMenuPanel = GetUI<InGameMenuPanel>("InGameMenuPanel");

    }
    private void Start()
    {

        inventorySetPanel.gameObject.SetActive(false);
        currentMenu = playerStatusBarPanel;

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
