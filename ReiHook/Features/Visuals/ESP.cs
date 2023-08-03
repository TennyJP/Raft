using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ReiHook.Utilities;
using System.Text.RegularExpressions;
namespace ReiHook.Features.Visuals {
    public class ESP : MonoBehaviour {
        private static float ReiTimer = 0f;
        private static Camera ReiCamera;
        private static bool IsInGame = LoadSceneManager.IsGameSceneLoaded;
        private static List<TradingPost> TradingPosts = new List<TradingPost>();
        private static List<TreasurePoint> TreasurePoints = new List<TreasurePoint>();
        private static List<PickupItem> Item = new List<PickupItem>();
        private static List<AI_NetworkBehaviour_Animal> AI = new List<AI_NetworkBehaviour_Animal>();
        private static List<Seagull> Seagulls = new List<Seagull>();
        private void Update() {
            ReiCamera = Camera.main;
            ReiTimer += Time.deltaTime;
            IsInGame = LoadSceneManager.IsGameSceneLoaded;
            if (ReiTimer >= 5f) {
                ReiTimer = 0f;
                if (RaftSettings.TradingPost) { TradingPosts = FindObjectsOfType<TradingPost>().ToList(); }
                if (RaftSettings.Treasures) { TreasurePoints = FindObjectsOfType<TreasurePoint>().ToList(); }
                if (RaftSettings.Item) { Item = FindObjectsOfType<PickupItem>().ToList(); }
                if (RaftSettings.HostileAnimal || RaftSettings.FriendlyAnimal) { AI = FindObjectsOfType<AI_NetworkBehaviour_Animal>().ToList(); Seagulls = FindObjectsOfType<Seagull>().ToList(); }
            }
        }
        private void OnGUI() {
            if (!IsInGame) return;
            if (Event.current.type != EventType.Repaint) return;
            if (RaftSettings.EnableESP) {
                if (RaftSettings.Landmark) {
                    foreach (Landmark Landmarks in WorldManager.AllLandmarks) {
                        if (!Landmarks) continue;
                        Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(Landmarks.transform.position);
                        float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, Landmarks.transform.position);
                        if (Render.IsOnScreen(WorldToScreen)) {
                            if (ReiDistance < RaftSettings.fLandmark) {
                                Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), GetIslandNames(Landmarks.name), Color.magenta, true, 12, FontStyle.Normal);
                                Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                            }
                        }
                    }
                }

                if (RaftSettings.TradingPost) {
                    if (TradingPosts.Count > 0) {
                        foreach (TradingPost TradingPosts in TradingPosts) {
                            if (!TradingPosts) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(TradingPosts.transform.position);
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, TradingPosts.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (ReiDistance < RaftSettings.fTradingPost) {
                                    Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), "Trading Post", Color.blue, true, 12, FontStyle.Normal);
                                    Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                }
                            }
                        }
                    }
                }

                if (RaftSettings.Treasures) {
                    if (TreasurePoints.Count > 0) {
                        foreach (TreasurePoint TreasurePoints in TreasurePoints) {
                            if (!TreasurePoints) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(TreasurePoints.transform.position);
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, TreasurePoints.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (ReiDistance < RaftSettings.fTreasures && TreasurePoints.IsBuried) {
                                    Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), GetTreasureNames(TreasurePoints.name), Color.cyan, true, 12, FontStyle.Normal);
                                    Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                }
                            }
                        }
                    }
                }

                if (RaftSettings.Item) {
                    if (Item.Count > 0) {
                        foreach (PickupItem Item in Item) {
                            if (!Item) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(Item.transform.position);
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, Item.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (Item.canBePickedUp) {
                                    if (RaftSettings.ItemDefault && ReiDistance < RaftSettings.fItemDefault) {
                                        if (Item.pickupItemType == PickupItemType.Default) {
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), Item.PickupName, Color.white, true, 12, FontStyle.Normal);
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                        }
                                    }
                                    if (RaftSettings.ItemDomesticAnimal && ReiDistance < RaftSettings.fItemDomesticAnimal) {
                                        if (Item.pickupItemType == PickupItemType.DomesticAnimal) {
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), Item.PickupName, Color.green, true, 12, FontStyle.Normal);
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                        }
                                    }
                                    if (RaftSettings.ItemNoteBookNote && ReiDistance < RaftSettings.fItemNoteBookNote) {
                                        if (Item.pickupItemType == PickupItemType.NoteBookNote) {
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), Item.PickupName, Color.cyan, true, 12, FontStyle.Normal);
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                        }
                                    }
                                    if (RaftSettings.ItemQuestItem && ReiDistance < RaftSettings.fItemQuestItem) {
                                        if (Item.pickupItemType == PickupItemType.QuestItem) {
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), Item.PickupName, Color.cyan, true, 12, FontStyle.Normal);
                                            Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (RaftSettings.HostileAnimal) {
                    if (AI.Count > 0) {
                        foreach (AI_NetworkBehaviour_Animal HostileAnimals in AI) {
                            if (!HostileAnimals) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(HostileAnimals.transform.position);
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, HostileAnimals.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (ReiDistance < RaftSettings.fHostileAnimal) {
                                    if (HostileCheck.Contains(HostileAnimals.behaviourType)) {
                                        Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), GetAnimalNames_Hostile(HostileAnimals.name), Color.red, true, 12, FontStyle.Normal);
                                        Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                    }
                                }
                            }
                        }
                    }
                }

                if (RaftSettings.HostileAnimal) {
                    if (Seagulls.Count > 0) {
                        foreach (Seagull Seagulls in Seagulls) {
                            if (!Seagulls) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(Seagulls.transform.position) + Vector3.up * 1f;
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, Seagulls.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (ReiDistance < RaftSettings.fHostileAnimal) {
                                    Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), "Seagull", Color.red, true, 12, FontStyle.Normal);
                                    Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                }
                            }
                        }
                    }
                }

                if (RaftSettings.FriendlyAnimal) {
                    if (AI.Count > 0) {
                        foreach (AI_NetworkBehaviour_Animal FriendlyAnimals in AI) {
                            if (!FriendlyAnimals) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(FriendlyAnimals.transform.position);
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, FriendlyAnimals.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (ReiDistance < RaftSettings.fFriendlyAnimal) {
                                    if (FriendlyCheck.Contains(FriendlyAnimals.behaviourType)) {
                                        Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), GetAnimalNames_Friendly(FriendlyAnimals.name), Color.green, true, 12, FontStyle.Normal);
                                        Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                    }
                                }
                            }
                        }
                    }
                }

                if (RaftSettings.NPC) {
                    if (AI.Count > 0) {
                        foreach (AI_NetworkBehaviour_Animal NPC in AI) {
                            if (!NPC) continue;
                            Vector3 WorldToScreen = ReiCamera.WorldToScreenPoint(NPC.transform.position);
                            float ReiDistance = Vector3.Distance(SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().transform.position, NPC.transform.position);
                            if (Render.IsOnScreen(WorldToScreen)) {
                                if (ReiDistance < RaftSettings.fFriendlyAnimal) {
                                    if (NPCCheck.Contains(NPC.behaviourType)) {
                                        Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y), GetNPCNames(NPC.name), Color.cyan, true, 12, FontStyle.Normal);
                                        Render.DrawString(new Vector3(WorldToScreen.x, Screen.height - WorldToScreen.y + 12), Mathf.Round(ReiDistance) + "m", Color.yellow, true, 12, FontStyle.Normal);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string GetTreasureNames(string pText) {
            pText = pText.Replace("Pickup_Base_Treasure_Index0_Tier1(Clone)", "Buried Pile of Junk").Replace("Pickup_Base_Treasure_Index1_Tier2(Clone)", "Buried Briefcase").Replace("Pickup_Base_Treasure_Index2_Tier3(Clone)", "Buried Safe").Replace("Pickup_Base_Treasure_Index3_TikiPiece1(Clone)", "Buried Tiki Piece 1").Replace("Pickup_Base_Treasure_Index4_TikiPiece2(Clone)", "Buried Tiki Piece 2").Replace("Pickup_Base_Treasure_Index5_TikiPiece3(Clone)", "Buried Tiki Piece 3").Replace("Pickup_Base_Treasure_Index6_TikiPiece4(Clone)", "Buried Tiki Piece 4");
            return new Regex(@"\([\d-]\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled).Replace(pText, string.Empty);
        }

        public static string GetAnimalNames_Hostile(string pText) {
            pText = pText.Replace("AI_StoneBird(Clone)", "Screecher").Replace("AI_PufferFish(Clone)", "Poison Puffer").Replace("AI_Boar(Clone)", "Warthog").Replace("AI_Rat(Clone)", "Lurker").Replace("AI_Shark(Clone)", "Shark").Replace("AI_Bear(Clone)", "Bear").Replace("AI_MamaBear(Clone)", "Mama Bear").Replace("AI_BeeSwarm(Clone)", "Bee Swarm").Replace("AI_Pig(Clone)", "Mudhog").Replace("AI_StoneBird_Caravan(Clone)", "White Screecher").Replace("AI_ButlerBot(Clone)", "Butler Bot").Replace("AI_RatTangaroa(Clone)", "Tangaroa Lurker").Replace("AI_Boss_Varuna(Clone)", "Rhino Shark").Replace("AI_AnglerFish(Clone)", "Angler Fish").Replace("AI_PolarBear(Clone)", "Polar Bear").Replace("AI_Roach(Clone)", "Scuttler").Replace("AI_BirdPack(Clone)", "Seagull").Replace("AI_Hyena(Clone)", "Hyena").Replace("AI_HyenaBoss(Clone)", "Hyena Boss");
            return new Regex(@"\([\d-]\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled).Replace(pText, string.Empty);
        }

        public static string GetAnimalNames_Friendly(string pText) {
            pText = pText.Replace("AI_Llama(Clone)", "Llama").Replace("AI_Goat(Clone)", "Goat").Replace("AI_Chicken(Clone)", "Clucker").Replace("AI_Puffin(Clone)", "Clucker").Replace("AI_Dolphin(Clone)", "Dolphin").Replace("AI_Whale(Clone)", "Whale").Replace("AI_Turtle(Clone)", "Turtle").Replace("AI_Stingray(Clone)", "Stingray");
            return new Regex(@"\([\d-]\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled).Replace(pText, string.Empty);
        }

        public static string GetNPCNames(string pText) {
            pText = pText.Replace("AI_NPC_Female_Annisa(Clone)", "Annisa").Replace("AI_NPC_Female_Citra(Clone)", "Citra").Replace("AI_NPC_Female_Ika(Clone)", "Ika").Replace("AI_NPC_Male_Isac(Clone)", "Isac").Replace("AI_NPC_Male_Johan(Clone)", "Johan").Replace("AI_NPC_Female_Kartika(Clone)", "Kartika").Replace("AI_NPC_Male_Larry(Clone)", "Larry").Replace("AI_NPC_Male_Max(Clone)", "Max").Replace("AI_NPC_Male_Noah(Clone)", "Noah").Replace("AI_NPC_Male_Oliver(Clone)", "Oliver").Replace("AI_NPC_Male_Timur(Clone)", "Timur").Replace("AI_NPC_Male_Toshiro(Clone)", "Toshiro").Replace("AI_NPC_Female_Ulla(Clone)", "Ulla").Replace("AI_NPC_Female_Zayana(Clone)", "Zayana").Replace("AI_NPC_Female_Vanessa(Clone)", "Vanessa");
            return new Regex(@"\([\d-]\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled).Replace(pText, string.Empty);
        }

        public static string GetIslandNames(string pText) {
            pText = pText.Replace("15#Landmark_Raft#Floating raft", "Floating Raft").Replace("16#Landmark_Raft#Floating raft", "Floating Raft").Replace("17#Landmark_Raft#Floating raft", "Floating Raft").Replace("18#Landmark_Raft#Floating raft", "Floating Raft").Replace("19#Landmark_Radar#Big radio tower", "Big Radio Tower").Replace("21#Landmark_Raft#Floating raft", "Floating Raft").Replace("28#Landmark_Big#OG", "Big Island Original").Replace("29#Landmark_Big#Cresent", "Big Island Cresent").Replace("30#Landmark_Big#Twin peak", "Big Island Twin Peak").Replace("31#Landmark_Pilot#", "Pilot Island").Replace("32#Landmark_Big#", "Big Island").Replace("33#Landmark_Boat#", "Boat Island").Replace("34#Landmark_Small#1", "Small Island").Replace("35#Landmark_Small#2", "Small Island").Replace("36#Landmark_Small#3", "Small Island").Replace("37#Landmark_Small#4", "Small Island").Replace("38#Landmark_Small#5", "Small Island").Replace("39#Landmark_Small#6", "Small Island").Replace("40#Landmark_Small#7", "Small Island").Replace("41#Landmark_Small#8", "Small Island").Replace("42#Landmark_Small#9", "Small Island").Replace("43#Landmark_Small#10", "Small Island").Replace("44#Landmark_Vasagatan", "Vasagatan").Replace("45#Landmark_BalboaIsland", "Balboa Island").Replace("49#Landmark_CaravanIsland#RealDeal", "Caravan Island").Replace("50#Landmark_Tangaroa#", "Tangaroa").Replace("54#Landmark_VarunaPoint#", "Varuna Point").Replace("55#Landmark_Temperance#", "Temperance").Replace("56#Landmark_Utopia#", "Utopia");
            return new Regex(@"\([\d-]\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled).Replace(pText, string.Empty);
        }

        List<AI_NetworkBehaviourType> HostileCheck = new List<AI_NetworkBehaviourType>() {
            AI_NetworkBehaviourType.StoneBird,
            AI_NetworkBehaviourType.PufferFish,
            AI_NetworkBehaviourType.Boar,
            AI_NetworkBehaviourType.Rat,
            AI_NetworkBehaviourType.Shark,
            AI_NetworkBehaviourType.Bear,
            AI_NetworkBehaviourType.MamaBear,
            AI_NetworkBehaviourType.BugSwarm_Bee,
            AI_NetworkBehaviourType.Pig,
            AI_NetworkBehaviourType.StoneBird_Caravan,
            AI_NetworkBehaviourType.ButlerBot,
            AI_NetworkBehaviourType.Rat_Tangaroa,
            AI_NetworkBehaviourType.Boss_Varuna,
            AI_NetworkBehaviourType.AnglerFish,
            AI_NetworkBehaviourType.PolarBear,
            AI_NetworkBehaviourType.Roach,
            AI_NetworkBehaviourType.BirdPack,
            AI_NetworkBehaviourType.Hyena,
            AI_NetworkBehaviourType.HyenaBoss
        };

        List<AI_NetworkBehaviourType> FriendlyCheck = new List<AI_NetworkBehaviourType>() {
            AI_NetworkBehaviourType.Llama,
            AI_NetworkBehaviourType.Goat,
            AI_NetworkBehaviourType.Chicken,
            AI_NetworkBehaviourType.Puffin,
            AI_NetworkBehaviourType.Dolphin,
            AI_NetworkBehaviourType.Whale,
            AI_NetworkBehaviourType.Turtle,
            AI_NetworkBehaviourType.Stingray
        };

        List<AI_NetworkBehaviourType> NPCCheck = new List<AI_NetworkBehaviourType>() {
            AI_NetworkBehaviourType.NPC_Annisa,
            AI_NetworkBehaviourType.NPC_Citra,
            AI_NetworkBehaviourType.NPC_Ika,
            AI_NetworkBehaviourType.NPC_Isac,
            AI_NetworkBehaviourType.NPC_Johan,
            AI_NetworkBehaviourType.NPC_Kartika,
            AI_NetworkBehaviourType.NPC_Larry,
            AI_NetworkBehaviourType.NPC_Max,
            AI_NetworkBehaviourType.NPC_Noah,
            AI_NetworkBehaviourType.NPC_Oliver,
            AI_NetworkBehaviourType.NPC_Timur,
            AI_NetworkBehaviourType.NPC_Toshiro,
            AI_NetworkBehaviourType.NPC_Ulla,
            AI_NetworkBehaviourType.NPC_Zayana,
            AI_NetworkBehaviourType.NPC_Vanessa
        };
    }
}
