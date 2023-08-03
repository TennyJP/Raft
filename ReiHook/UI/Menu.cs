using UnityEngine;
using ReiHook.Utilities;
using UltimateWater.Internal;
namespace ReiHook.UI {
    public class Menu : MonoBehaviour {
        private Rect MainWindow;
        private Rect PlayerWindow;
        private Rect VisualWindow;
        private Rect MiscellaneousWindow;
        private Rect SpawnerWindow;

        private static bool IsInLobby = LoadSceneManager.IsLoadingLobbyScene;
        private static bool IsInGame = LoadSceneManager.IsGameSceneLoaded;

        Vector2 ScrollPosition;
        GUIStyle WatermarkStyle = new GUIStyle();
        GUIStyle LabelStyle = new GUIStyle();

        private bool bGUI = false;
        private bool bPlayerWindow = false;
        private bool bVisualWindow = false;
        private bool bMiscellaneousWindow = false;
        private bool bSpawnerWindow = false;
        private void Start() {
            MainWindow = new Rect(20f, 60f, 250f, 150f);
            WatermarkStyle.normal.textColor = Color.yellow; LabelStyle.normal.textColor = Color.white;
        }

        private void Update() {
            ToggleMenu();
            if(Input.GetKeyDown(KeyCode.Delete)) { ReiLoader.ReiUnload(); }
            IsInLobby = LoadSceneManager.IsLoadingLobbyScene;
            IsInGame = LoadSceneManager.IsGameSceneLoaded;
        }

        private void ToggleMenu() {
            if (Input.GetKeyDown(KeyCode.F4)) {
                bGUI = !bGUI;
                if (!IsInLobby) { if (!IsInGame) return; }
                if (bGUI) {
                    Helper.SetCursorLockState(CursorLockMode.Confined);
                    Helper.SetCursorVisible(true);
                    ComponentManager<Network_Player>.Value.PlayerScript.SetLockMouseLook(true);
                }
                else {
                    Helper.SetCursorLockState(CursorLockMode.Locked);
                    Helper.SetCursorVisible(false);
                    ComponentManager<Network_Player>.Value.PlayerScript.SetLockMouseLook(false);
                }
            }
        }

        private void OnGUI() {
            GUI.Label(new Rect(20, 20, 200, 60), "UnKnoWnCheaTs.me | Tenny", WatermarkStyle); GUI.Label(new Rect(20, 40, 200, 60), "ReiHook for Raft v1.0", WatermarkStyle);
            if (!bGUI) return;
            GUI.backgroundColor = Color.black;
            MainWindow = GUILayout.Window(0, MainWindow, new GUI.WindowFunction(UI), "Menu", new GUILayoutOption[0]);
            if (bPlayerWindow) { PlayerWindow = GUILayout.Window(1, PlayerWindow, new GUI.WindowFunction(UI), "Player", new GUILayoutOption[0]); }
            if (bVisualWindow) { VisualWindow = GUILayout.Window(2, VisualWindow, new GUI.WindowFunction(UI), "Visual", new GUILayoutOption[0]); }
            if (bMiscellaneousWindow) { MiscellaneousWindow = GUILayout.Window(3, MiscellaneousWindow, new GUI.WindowFunction(UI), "Miscellaneous", new GUILayoutOption[0]); }
            if (bSpawnerWindow) { SpawnerWindow = GUILayout.Window(4, SpawnerWindow, new GUI.WindowFunction(UI), "Give Items", new GUILayoutOption[0]); }
        }

        private void UI(int pID) {
            GUI.backgroundColor = Color.black; GUI.contentColor = Color.white;
            switch (pID) {
                case 0:
                    GUILayout.Label("Press F4 for Menu", LabelStyle, new GUILayoutOption[0]);
                    GUILayout.Label("Delete to Unhook the Cheat", LabelStyle, new GUILayoutOption[0]);
                    if (GUILayout.Button("Player", new GUILayoutOption[0])) { bPlayerWindow = !bPlayerWindow; }
                    if (GUILayout.Button("Visual", new GUILayoutOption[0])) { bVisualWindow = !bVisualWindow; }
                    if (GUILayout.Button("Miscellaneous", new GUILayoutOption[0])) { bMiscellaneousWindow = !bMiscellaneousWindow; }
                    if (GUILayout.Button("Spawner", new GUILayoutOption[0])) { bSpawnerWindow = !bSpawnerWindow; }
                    break;
                case 1:
                    RaftSettings.UnlimitedHealth = GUILayout.Toggle(RaftSettings.UnlimitedHealth, "Unlimited Health", new GUILayoutOption[0]);
                    RaftSettings.NoThirst = GUILayout.Toggle(RaftSettings.NoThirst, "No Thirst", new GUILayoutOption[0]);
                    RaftSettings.NoHunger = GUILayout.Toggle(RaftSettings.NoHunger, "No Hunger", new GUILayoutOption[0]);
                    if (GUILayout.Button("Give Armor Set", new GUILayoutOption[0])) { Features.Player.Armor.GiveArmor(); }
                    if (GUILayout.Button("Give Treasure Hunter Set", new GUILayoutOption[0])) { Features.Player.TreasureHunter.GiveTreasureHunterItems(); }
                    if (GUILayout.Button("Give Resources", new GUILayoutOption[0])) { Features.Player.Resources.GiveResources(); }
                    if (GUILayout.Button("Give Cooked Food", new GUILayoutOption[0])) { Features.Player.Food.GiveFood(); }
                    if (GUILayout.Button("Give all Tiki's", new GUILayoutOption[0])) { Features.Player.Tiki.GiveTiki(); }
                    if (GUILayout.Button("Unlock All", new GUILayoutOption[0])) { Features.Player.Unlockables.UnlockAll(); }
                    break;
                case 2:
                    RaftSettings.EnableESP = GUILayout.Toggle(RaftSettings.EnableESP, "Enable ESP", new GUILayoutOption[0]);
                    RaftSettings.Landmark = GUILayout.Toggle(RaftSettings.Landmark, "Landmark" + " [" + Mathf.Round(RaftSettings.fLandmark) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fLandmark = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fLandmark, 1f, 1000f, new GUILayoutOption[0]) * 1000f) / 1000f;
                    RaftSettings.TradingPost = GUILayout.Toggle(RaftSettings.TradingPost, "Trading Post" + " [" + Mathf.Round(RaftSettings.fTradingPost) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fTradingPost = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fTradingPost, 1f, 500f, new GUILayoutOption[0]) * 500f) / 500f;
                    RaftSettings.Treasures = GUILayout.Toggle(RaftSettings.Treasures, "Treasures" + " [" + Mathf.Round(RaftSettings.fTreasures) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fTreasures = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fTreasures, 1f, 500f, new GUILayoutOption[0]) * 500f) / 500f;
                    RaftSettings.Item = GUILayout.Toggle(RaftSettings.Item, "Item ESP", new GUILayoutOption[0]);
                    RaftSettings.ItemDefault = GUILayout.Toggle(RaftSettings.ItemDefault, "Default Items" + " [" + Mathf.Round(RaftSettings.fItemDefault) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fItemDefault = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fItemDefault, 1f, 150f, new GUILayoutOption[0]) * 150f) / 150f;
                    RaftSettings.ItemDomesticAnimal = GUILayout.Toggle(RaftSettings.ItemDomesticAnimal, "Domestic Animal Items" + " [" + Mathf.Round(RaftSettings.fItemDomesticAnimal) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fItemDomesticAnimal = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fItemDomesticAnimal, 1f, 150f, new GUILayoutOption[0]) * 150f) / 150f;
                    RaftSettings.ItemNoteBookNote = GUILayout.Toggle(RaftSettings.ItemNoteBookNote, "Notebook Notes" + " [" + Mathf.Round(RaftSettings.fItemNoteBookNote) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fItemNoteBookNote = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fItemNoteBookNote, 1f, 250f, new GUILayoutOption[0]) * 250f) / 250f;
                    RaftSettings.ItemQuestItem = GUILayout.Toggle(RaftSettings.ItemQuestItem, "Quest Items" + " [" + Mathf.Round(RaftSettings.fItemQuestItem) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fItemQuestItem = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fItemQuestItem, 1f, 250f, new GUILayoutOption[0]) * 250f) / 250f;
                    RaftSettings.HostileAnimal = GUILayout.Toggle(RaftSettings.HostileAnimal, "Hostile Animals" + " [" + Mathf.Round(RaftSettings.fHostileAnimal) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fHostileAnimal = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fHostileAnimal, 1f, 250f, new GUILayoutOption[0]) * 250f) / 250f;
                    RaftSettings.FriendlyAnimal = GUILayout.Toggle(RaftSettings.FriendlyAnimal, "Animals" + " [" + Mathf.Round(RaftSettings.fFriendlyAnimal) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fFriendlyAnimal = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fFriendlyAnimal, 1f, 250f, new GUILayoutOption[0]) * 250f) / 250f;
                    RaftSettings.NPC = GUILayout.Toggle(RaftSettings.NPC, "NPC" + " [" + Mathf.Round(RaftSettings.fNPC) + "m]", new GUILayoutOption[0]);
                    RaftSettings.fNPC = Mathf.Round(GUILayout.HorizontalSlider(RaftSettings.fNPC, 1f, 200f, new GUILayoutOption[0]) * 200f) / 200f;
                    break;
                case 3:
                    RaftSettings.FlyMode = GUILayout.Toggle(RaftSettings.FlyMode, "Fly [F1]", new GUILayoutOption[0]);
                    if (GUILayout.Button("Force Anchor", new GUILayoutOption[0])) { Features.Miscellaneous.ForceAnchor.AddAnchor(); }
                    if (GUILayout.Button("Water all Plants", new GUILayoutOption[0])) { Features.Miscellaneous.Plants.WaterPlants(); }
                    if (GUILayout.Button("Grow Plants", new GUILayoutOption[0])) { Features.Miscellaneous.Plants.GrowPlants(); }
                    if (GUILayout.Button("Unlock Achievements", new GUILayoutOption[0])) { Features.Player.Unlockables.UnlockAchievements(); }
                    break;
                case 4:
                    ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, GUILayout.Width(350), GUILayout.Height(1000));
                    GUILayout.Label("Amount: " + RaftSettings.Amount, LabelStyle, new GUILayoutOption[0]);
                    RaftSettings.Amount = (int)GUILayout.HorizontalSlider(RaftSettings.Amount, 1, 100, new GUILayoutOption[0]) * 100 / 100;
                    GUILayout.Space(10);
                    for (var i = 0; i < Items.ItemList.Count; i++)
                    {
                        if (GUILayout.Button(Items.ItemList[i], new GUILayoutOption[0])) { ComponentManager<Network_Player>.Value.Inventory.AddItem(Items.ItemList[i], RaftSettings.Amount); }
                    }
                    GUILayout.EndScrollView();
                    break;
                default:
                    break;
            }
            GUI.DragWindow();
        }
    }
}
