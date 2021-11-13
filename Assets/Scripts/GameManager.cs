using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   private void Awake() {

       if(GameManager.instance != null) {
           Destroy(gameObject);
           Destroy(player.gameObject);
           Destroy(floatingTextManager.gameObject);
           Destroy(hud);
           Destroy(menu);
           return;
       }
    //    PlayerPrefs.DeleteAll();

       instance = this;
    //    SceneManager.sceneLoaded += LoadState;
       SceneManager.sceneLoaded += OnSceneLoaded;

   }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;


    public int pesos;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //upgrade weapon
    public bool TryUpgradWeapon() {
        //is the weapon max level
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if(pesos >= weaponPrices[weapon.weaponLevel]) {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    //health bar
    public void OnHitPointChange() {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }
    public void OnHitPointChangeUn() {
        float ratioUn = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratioUn, 1);
    }

    //Exprience System
    public int GetCurrentLevel() {
        int r = 0;
        int add = 0;

        while(experience >= add) {
            add += xpTable[r];
            r++;
            if(r == xpTable.Count) //max level
                return r;
        }
        return r;
    }
   
    public int GetXpToLevel(int level) {
        int r = 0;
        int xp = 0;

        while(r < level) {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp) {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if(currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp() {
        Debug.Log("Level Up!!");
        player.OnLevelUp();
        OnHitPointChange();
    }
    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode) {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //deathmenu and respawn
    public void Respawn() {
        deathMenuAnim.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
        player.Respawn();
    } 
    public void RespawnHeal() {
        deathMenuAnim.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
        Respawn();
    } 

    public void SaveState() {

        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {

        SceneManager.sceneLoaded -= LoadState;

        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // "0 | 10 | 15 | 2"
        //Change Player skin
        pesos =  int.Parse(data[1]);

        //experience
        experience =  int.Parse(data[2]);
        if(GetCurrentLevel() != 1) 
            player.SetLevel(GetCurrentLevel());

        //change the weapon lavel
        weapon.SetWeaponLevel(int.Parse(data[3]));

    }

}
