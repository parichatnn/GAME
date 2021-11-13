using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
   // public GameObject effect;
    private Animator anim;

   private SpriteRenderer spriteRenderer;
   private bool isAlive = true;
   protected override void Start()
   {
      base.Start();
      spriteRenderer = GetComponent<SpriteRenderer>();
      anim = GetComponent<Animator>();
   }
   protected override void ReceiveDamage(Damage dmg)
   {
      if(!isAlive) {
         return;
      }
         
      base.ReceiveDamage(dmg);
      GameManager.instance.OnHitPointChange();
   }
   protected override void ReceiveDamage_boss(Damage dmg)
   {
      if(!isAlive) {
         return;
      }
      base.ReceiveDamage_boss(dmg);
      GameManager.instance.OnHitPointChange();
   }
   protected override void Death() {
      isAlive = false;
      GameManager.instance.deathMenuAnim.SetTrigger("Show");
   }
   private void FixedUpdate() 
   {
      float x = Input.GetAxisRaw("Horizontal");
      float y = Input.GetAxisRaw("Vertical");

      if(isAlive) {
         UpdateMotor(new Vector3(x,y,0));      
      }
   }
   public void SwapSprite(int skinId)
   {
      spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
   }
   public void OnLevelUp() {
      maxHitpoint++;
      hitpoint = maxHitpoint;
   }
   public void SetLevel(int level) {
      for (int i = 0; i < level; i++)
      {
         OnLevelUp();
      }
   }
   public void Heal(int healingAmount) {   
      if(hitpoint == maxHitpoint)
         return;
      
      hitpoint += healingAmount;
      if(hitpoint > maxHitpoint)
         hitpoint = maxHitpoint;

      // Instantiate(effect, transform.position, Quaternion.identity); //effect
      GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp" ,25, Color.green, transform.position, Vector3.up * 35, 1.0f);
      GameManager.instance.OnHitPointChange();
   }

   public void Respawn() {
      Heal(maxHitpoint);
      isAlive = true;
      lastImmune = Time.time;
      pushDirection = Vector3.zero;
   }
    public void RespawnHeal() {
      Heal(maxHitpoint);
      isAlive = true;
      lastImmune = Time.time;
      pushDirection = Vector3.zero;
   }
   public void Unheal(int healingAmount) {

      hitpoint -= healingAmount;
      if(hitpoint == 0) {
         Death();
      }
      else if(hitpoint < 0){
         return;
      }

      GameManager.instance.ShowText("-" + healingAmount.ToString() + " hp" ,25, Color.red, transform.position, Vector3.up * 35, 1.0f);
      GameManager.instance.OnHitPointChangeUn();
   }
   public float slowdown = 2f;

    void Update() {
        Time.timeScale += (1f / slowdown) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale,0f,1.0f);
    }
   public void Slow() {   
      
      GameManager.instance.ShowText("Slime!!!",25, Color.red, transform.position, Vector3.up * 40, 1.0f);
      Time.timeScale = 0.05f;
      Time.fixedDeltaTime = Time.timeScale * 0.02f;
      // timeSlow -= 1;
      // if( timeSlow == 0) {
      //    xSpeed = 1.0f;
      //    ySpeed = 0.75f;
      //    timeSlow = 5;
      // }
   }
    private void Walk()
    {
        anim.SetTrigger("Walk");
    }

   
} 
