# 🎮 The Eye - A Dynamic 3D RPG Game

**עברית:** משחק RPG תלת-ממדי עם מערכת דמויות מתקדמת, משימות דינמיות ואינטגרציה של דמות אמא (Ima).

**English:** A 3D RPG game with advanced character system, dynamic quests, and integration of the Ima (Mother) character.

---

## 📋 תוכן העניינים | Table of Contents

- [תיאור המשחק](#תיאור-המשחק)
- [מרכיבי המשחק](#מרכיבי-המשחק)
- [הדמויות](#הדמויות)
- [הסצנות](#הסצנות)
- [מערכות משחק](#מערכות-משחק)
- [הוראות התקנה והפעלה](#הוראות-התקנה-והפעלה)
- [מערכת המשימות](#מערכת-המשימות)
- [מערכת אמא (Ima)](#מערכת-אמא-ima)
- [הרחבות עתידיות](#הרחבות-עתידיות)

---

## 🎯 תיאור המשחק

**The Eye** הוא משחק RPG דינמי בו השחקן בוחר דמות, עוברים בעולם פנטזיה, מתמודדים עם אויבים, משלימים משימות ומתקדמים ברמות.

### עיקרי המשחק:
- ✅ 5 דמויות שחקן עם יכולות ייחודיות
- ✅ 4 סצנות עולם: Village, Forest, Cave, River
- ✅ מערכת NPCs וסחר
- ✅ מערכת משימות דינמית
- ✅ לוגיקת combat עם אויבים שונים
- ✅ מערכת HUD עם בריאות, XP, זהב ומשימות
- ✅ אנימציות ואפקטים חזותיים
- ✅ מערכת אמא (Ima) עם דיאלוגים ותגובות רגשיות
- ✅ תומך בכל הפלטפורמות (PC, Mobile)

---

## 🎭 הדמויות

### 1. **Creator (בונה/יוצרת)**
- **HP:** 100
- **Speed:** 4 m/s
- **Unique:** Building & Crafting
- **Abilities:**
  - `BuildStructure()` - בנייה של מבנים
  - `CraftItem()` - יצור פריטים

### 2. **Hunter (צייד)**
- **HP:** 80
- **Speed:** 5.5 m/s
- **Unique:** Accuracy & Critical Hits
- **Abilities:**
  - `ShootArrow()` - ירי חצים
  - `TrackPrey()` - עקבול טרף

### 3. **Mage (קוסמת)**
- **HP:** 70
- **Speed:** 4 m/s
- **Unique:** Spellcasting & Mana
- **Abilities:**
  - `CastFireball()` - כישוף אש
  - `CastFrostNova()` - כישוף קור

### 4. **Healer (מרפא)**
- **HP:** 90
- **Speed:** 3.5 m/s
- **Unique:** Healing & Support
- **Abilities:**
  - `HealSelf()` - ריפוי עצמי
  - `HealAlly()` - ריפוי בן ברית
  - `CastHolyLight()` - אור קדוש

### 5. **Explorer (חוקרת)**
- **HP:** 85
- **Speed:** 6 m/s
- **Unique:** Sensing & Tracking
- **Abilities:**
  - `SenseNearby()` - חישה של קרובים
  - `Scout()` - סיור אזור
  - `GatherResources()` - אספת משאבים

---

## 🗺️ הסצנות (Worlds)

### 1. **Village (הכפר)** - בית
- 🏘️ מיקום: Home base
- 👥 NPCs: Shopkeeper, Doctor, Blacksmith, Ima
- ⚔️ Difficulty: Easy
- 🎯 Purpose: בסיס, קניות, משימות

### 2. **Forest (יער)** - הרפתקה בינונית
- 🌲 מיקום: East of Village
- 👹 Enemies: Wolves, Goblins
- ⚔️ Difficulty: Medium
- 🎯 Purpose: ציד, אוצרות קטנים

### 3. **Cave (מערה)** - אתגר קשה
- 🕳️ מיקום: North of Village
- 👹 Enemies: Ogres, Dark creatures, BOSS
- ⚔️ Difficulty: Hard
- 🎯 Purpose: לוויתנים גדולים, אוצרות גדולים

### 4. **River (נהר)** - מיוחד
- 🌊 מיקום: West of Village
- 👹 Enemies: Frogs, Water creatures
- ⚔️ Difficulty: Medium-Hard
- 🎯 Purpose: דיג, משאבים ימיים

---

## ⚙️ מערכות משחק

### 1. **Character System** (`CharacterBase.cs`)
```csharp
- Health Management
- Experience & Leveling
- Damage System
- Death Handling
```

### 2. **Inventory System** (`Inventory.cs`)
```csharp
- 20 item slots
- Item management (add/remove/get)
- Item types: Weapon, Armor, Consumable, Quest, Misc
```

### 3. **Quest System** (`QuestManager.cs`)
```csharp
- Accept quests
- Track progress
- Complete quests
- Reward XP & Gold
```

### 4. **Combat System** (`EnemyCharacter.cs`)
```csharp
- Enemy AI & Patrolling
- Detection Range
- Attack System
- Death & Rewards
```

### 5. **Audio System** (`AudioManager.cs`)
```csharp
- Music playback
- SFX effects
- Volume control
- Singleton pattern
```

### 6. **Animation System** (`AnimationManager.cs`)
```csharp
- Walk/Idle animations
- Attack animations
- Spell casting
- Emotion animations
```

### 7. **HUD System** (`HUDManager.cs`)
```csharp
- Health bar
- Level display
- Gold counter
- Quest tracker
```

---

## 🛠️ הוראות התקנה והפעלה

### דרישות:
- Unity 2020 LTS או חדש יותר
- .NET Framework 4.7.1+

### התקנה:

1. **פתח את הפרויקט:**
```bash
cd /workspaces/Ima-3d-mom/unity/TheEye
open TheEye.sln  # ב-Windows/Mac
```

2. **טען את הסצנה:**
```
Assets/_TheEye/Scenes/Boot.unity
```

3. **בנה את העולם:**
```csharp
// בעתיד - בתוך Boot.unity Scene Manager יהיה:
var player = PrefabFactory.CreatePlayer(CharacterRole.Creator, Vector3.zero);
var ima = PrefabFactory.CreateImaCharacter(new Vector3(5, 0, 5));
```

4. **הפעל את המשחק:**
```
Press Play ▶️ in Unity Editor
```

### טעינת דמות:
```csharp
// Script: GameInitializer.cs
CharacterRole selectedRole = CharacterRole.Creator; // בחר דמות
GameObject player = PrefabFactory.CreatePlayer(selectedRole, Vector3.zero);
GameObject ima = PrefabFactory.CreateImaCharacter(new Vector3(5, 0, 5));
```

---

## 📋 מערכת המשימות

### משימות זמינות:

| משימה | NPC | תיאור | Reward |
|------|-----|-------|--------|
| "Defeat Forest Wolves" | Quest Master | הרוג 5 זאבים | 200 XP, 100 Gold |
| "Gather Forest Herbs" | Healer | אסוף 10 עשבים | 150 XP, 75 Gold |
| "Explore Cave" | Explorer | גלה את המערה | 300 XP, 200 Gold |
| "Fish at River" | Fisherman | תפוס 3 דגים | 100 XP, 50 Gold |

### מנגנון משימות:
```csharp
QuestManager qm = player.GetComponent<QuestManager>();
qm.AcceptQuest(quest);
// משחק...
qm.CompleteQuest(quest); // => +XP, +Gold
```

---

## 👨‍👩‍👧 מערכת אמא (Ima)

### תפקיד אמא:
- 🏠 **בבית (Village):** נותנת משימות, מוכרת פריטים חיוניים
- 💔 **בסכנה:** מזהירה השחקן, נותנת חיזוקים
- 🎯 **בהצלחה:** מתגאה בהצלחות
- ❤️ **בתמיכה:** תמיד שם לתמיכה

### Ima Actions:
```csharp
ImaCharacter ima = imaObj.GetComponent<ImaCharacter>();

// אמא נותנת משימה
ima.GiveQuest(player);

// אמא מגיבה להצלחה
ima.ReactToPlayerAction(PlayerCharacterAction.Victory);

// אמא נותנת פריט
var potion = new InventoryItem { 
    itemName = "Health Potion", 
    type = ItemType.Consumable 
};
ima.GiveItem(player, potion);
```

### דיאלוגים דינמיים:
```
[Health < 50%]
"Ima: You're hurt! Take this healing potion!"

[Level Up]
"Ima: I'm so proud of you, my child!"

[Far from home]
"Ima: Please be careful out there..."

[Victory]
"Ima: I knew you could do it!"
```

---

## 📁 מבנה הפרויקט

```
Assets/_TheEye/
├── Characters/
│   ├── Prefabs/
│   │   ├── Player/          # דמויות שחקן
│   │   ├── NPCs/            # דמויות NPC
│   │   └── Enemies/         # דמויות אויב
│   └── Scripts/
│       ├── CharacterBase.cs
│       ├── PlayerCharacter.cs
│       ├── CreatorCharacter.cs
│       ├── HunterCharacter.cs
│       ├── MageCharacter.cs
│       ├── HealerCharacter.cs
│       ├── ExplorerCharacter.cs
│       ├── NPCCharacter.cs
│       ├── ImaCharacter.cs
│       ├── EnemyCharacter.cs
│       ├── EnemyTypes.cs
│       ├── Shopkeeper.cs
│       ├── QuestGiver.cs
│       ├── Doctor.cs
│       └── Blacksmith.cs
├── Scenes/
│   ├── Menus/
│   │   └── MainMenu.unity
│   ├── GameScenes/
│   │   ├── VillageScene.cs
│   │   ├── ForestScene.cs
│   │   ├── CaveScene.cs
│   │   └── RiverScene.cs
│   └── Enviroments/
│       └── Boot.unity       # בסיס המשחק
├── Systems/Scripts/
│   ├── GameManager.cs       # מנהל משחק כללי
│   ├── PrefabFactory.cs     # יצור דמויות דינמי
│   ├── AnimationManager.cs  # אנימציות
│   ├── ParticleEffectManager.cs
│   ├── Inventory/
│   │   └── Inventory.cs
│   ├── Quest/
│   │   └── QuestManager.cs
│   ├── Combat/
│   │   └── (future combat system)
│   └── Audio/
│       └── AudioManager.cs
├── UI/
│   ├── Scripts/
│   │   ├── HUDManager.cs
│   │   ├── CharacterSelectionUI.cs
│   │   ├── InventoryUI.cs
│   │   ├── QuestUI.cs
│   │   └── MainMenuUI.cs
│   ├── Prefabs/
│   └── Sprites/
└── Animations/
    └── Character/           # אנימציות דמויות
```

---

## 🚀 הרחבות עתידיות

### Phase 2:
- [ ] Save/Load System
- [ ] Advanced Enemy AI
- [ ] Skill Trees
- [ ] Equipment System

### Phase 3:
- [ ] Multiplayer Support
- [ ] Dungeon System
- [ ] Boss Mechanics
- [ ] Pet System

### Phase 4:
- [ ] Story Cutscenes
- [ ] Voice Acting
- [ ] Advanced Graphics
- [ ] Mod Support

---

## 🎮 שימוש בפקודות

### בקונסול:
```csharp
// כדי לבדוק את המערכות בזמן gameplay:
Debug.Log(player.GetHealth());           // בדוק בריאות
Debug.Log(player.GetLevel());            // בדוק רמה
Debug.Log(player.GetGold());             // בדוק זהב
Debug.Log(qm.GetActiveQuestCount());    // בדוק משימות פעילות
```

---

## 📝 שימוש ב-AI Game Builder

### עדכון דמויות:
```bash
# לעדכן את דמות מסוימת:
# 1. ערוך את ה-Script הרלוונטי
# 2. בדוק ב-Play Mode
# 3. Commit לגיט
git add Assets/_TheEye/Characters/Scripts/
git commit -m "Update [CharacterName] abilities"
git push origin main
```

### הוספת סצנה חדשה:
```csharp
// יצור Script חדש בתיקייה Scenes/GameScenes:
public class NewScene : MonoBehaviour {
    private void Start() {
        Debug.Log("[NewScene] Initialized");
    }
}
```

### הוספת משימה חדשה:
```csharp
Quest newQuest = new Quest {
    questName = "New Quest",
    description = "Description...",
    rewardXP = 200,
    rewardGold = 100,
    targetCount = 5
};
questManager.AcceptQuest(newQuest);
```

---

## ✅ בדיקות יחידה

### Validation Checklist:
- [ ] כל Scripts קמפילים ללא שגיאות
- [ ] כל Prefabs טעונים ללא שגיאות
- [ ] GameManager מאותחל בהצלחה
- [ ] Player יכול להתנוע ולתקוף
- [ ] Ima נמצאת בסצנה ותגיב לאירועים
- [ ] NPCs תגובה לאינטראקציה
- [ ] אויבים מופיעים ותוקפים
- [ ] HUD מציג את הנתונים בצורה נכונה
- [ ] משימות מקבל ומשלים בהצלחה

---

## 📞 תמיכה וקשר

**Repository:** https://github.com/imaosglobal/Ima-3d-mom

**Issues & Suggestions:** GitHub Issues

---

## 📜 רישיון

MIT License - ראה LICENSE קובץ

---

**משחק זה נוצר עם ❤️ על ידי Ima Global**

**Game created with ❤️ by Ima Global**

---

*Last Updated: December 26, 2025*
*Version: 1.0.0 - Initial Release*
