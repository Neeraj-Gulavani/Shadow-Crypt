using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Pathfinding;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

    public enum AbilityType
{
    None,
    Lightning,
    SoulDrain,
    Rogue,
    Slash360
}


public class PlayerAbilityManager : MonoBehaviour
{
    PlayerControls controls;
    // Soul Energy Bar

    public float maxEnergy = 100f;
    public static float energy, targetFill;
    public Image fillBar;
    public float fillSpeed = 2f;
    public bool canUseAbility = true;
    public bool UseEnergy(float amt)
    {
        if (amt <= energy)
        {
            energy -= amt;
            UpdateEnergyBar();
            return true;
        }
        return false;
    }

    public bool CheckEnergy(float amt)
    {
        if (amt <= energy)
        {
            return true;
        }
        return false;
    }

    public void RestoreEnergy(float amt)
    {
        energy += amt;
        if (energy > 100)
        {
            energy = 100;
        }
        UpdateEnergyBar();
    }

    public void UpdateEnergyBar()
    {
        targetFill = energy / maxEnergy;
    }


    //end soul energy

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 5f); // radius
    }
    public GameObject lightningVfx;
    public GameObject soulVfx;
    public GameObject ZapVfx;
    public GameObject slash360vfx;
    public GameObject magiccircle;
    public GameObject magiccirclePurple;
    public GameObject rogueVfx;
    private List<Ability> abilities = new List<Ability>();
    public GameObject effPanel;
    private Rogue rogueAbility;
    private SoulDrain soulAbility;

    // Start is called before the first frame update

    public void DeactivateRogue()
    {
        if (rogueAbility != null)
        {
            rogueAbility.Deactivate();
        }
    }

    public void UnlockLightning()
    {

        abilities.Add(new Lightning(5f, lightningVfx, magiccircle, ZapVfx, 30f));
        abilityDisplayManager.lightningEnabled = true;
    }
    public void Unlockslash360()
    {

        abilities.Add(new slash360(slash360vfx, 20f));
        abilityDisplayManager.slash360Enabled = true;
    }

    public void UnlockSoulDrain()
    {
        soulAbility = new SoulDrain(5f, soulVfx, magiccirclePurple, 30f);
        abilities.Add(soulAbility);
        abilityDisplayManager.soulDrainEnabled = true;
    }

    public void UnlockRogue()
    {
        rogueAbility = new Rogue(effPanel, rogueVfx, 70f);
        abilities.Add(rogueAbility);
        abilityDisplayManager.rogueEnabled = true;
    }
   
    public AbilityType currentAbility;

    void Awake()
    {
        controls = new PlayerControls();

        // Activate ability button pressed
        controls.Gameplay.ActivateAbility.started += ctx =>
        {
            var ability = abilities.Find(a => a.Type == currentAbility);
            ability?.OnHoldStart();
            
        };

        // Activate ability button released
        controls.Gameplay.ActivateAbility.canceled += ctx =>
        {
            var ability = abilities.Find(a => a.Type == currentAbility);
            ability?.OnHoldEnd();
            if (ability!=null)
            PlayerVibration.instance.Vibrate(0.3f, 0.7f, 0.15f);
        };
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void Start()
    {
        currentAbility = AbilityType.None;
        //Unlockslash360();
        energy = 0;
        targetFill = energy;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!canUseAbility) return;

        if (fillBar == null) return;
        fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, targetFill, fillSpeed * Time.deltaTime);
    }




    #region ABILITIES

    public interface Ability
    {
        AbilityType Type { get; }
        public void OnHoldStart();
        public void OnHoldEnd();

    }

    //LIGHTNING ABILITY
    class Lightning : Ability
    {
        public AbilityType Type => AbilityType.Lightning;
        public GameObject vfx;

        public KeyCode key;
        public GameObject strikeHeight;
        public GameObject ZapVfx;
        public float radius = 10f;
        public GameObject circle;
        public float energyCost;
        public Lightning(float rad, GameObject vfx, GameObject circle, GameObject ZapVfx, float energyCost)
        {
            this.radius = rad;
            this.vfx = vfx;
            this.circle = circle;
            this.ZapVfx = ZapVfx;
            this.energyCost = energyCost;
        }
        public void OnHoldStart()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.CheckEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            if (!pam.canUseAbility) return;
            pam.canUseAbility = false;
            circle.SetActive(true);
        }
        public void OnHoldEnd()
        {
            circle.SetActive(false);
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.UseEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            Vector2 playerPos = player.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, radius);
            GameObject zapp = Instantiate(ZapVfx, playerPos, Quaternion.identity);
            Destroy(zapp, 1f);
            foreach (Collider2D c in colliders)
            {
                if (!c.CompareTag("Enemy")) continue;
                Vector3 spawnPos = c.transform.position + Vector3.up * 0.1f;

                GameObject theVfx = Instantiate(vfx, spawnPos, Quaternion.identity);


                c.GetComponent<EnemyHealth>().TakeDamage(50, theVfx.transform.position, 0.1f);

                var ai = c.GetComponentInParent<AIPath>();
                if (ai != null)
                {
                    ai.enabled = false;
                }
                else
                {
                    Debug.LogWarning("No AIPath found on " + c.name);
                }

            }
            player.GetComponent<PlayerAbilityManager>().canUseAbility = true;
        }




    }


    class SoulDrain : Ability
    {
        public AbilityType Type => AbilityType.SoulDrain;
        public GameObject vfx;

        public KeyCode key;
        public GameObject strikeHeight;
        public GameObject ZapVfx;
        public float radius = 10f;
        public GameObject circle;
        public Material mat;
        private float cooldown = 5f;
        private float lastUsedTime = -Mathf.Infinity;
        public float energyCost;

        public SoulDrain(float rad, GameObject vfx, GameObject circle, float energyCost)
        {
            this.radius = rad;
            this.vfx = vfx;
            this.circle = circle;
            this.energyCost = energyCost;
        }

        public void OnHoldStart()
        {
            if (Time.time - lastUsedTime < cooldown)
            {
                Debug.Log("Rogue on cooldown!");
                return;
            }
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.CheckEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            if (!pam.canUseAbility) return;
            pam.canUseAbility = false;
            circle.SetActive(true);




            lastUsedTime = Time.time;
        }

        public void OnHoldEnd()
        {
            circle.SetActive(false);
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.UseEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            Vector2 playerPos = player.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, radius);
            foreach (Collider2D c in colliders)
            {
                if (!c.CompareTag("Enemy")) continue;
                Vector3 spawnPos = c.transform.position + Vector3.up * 0.000001f;

                GameObject theVfx = Instantiate(vfx, spawnPos, Quaternion.identity);
                theVfx.transform.SetParent(c.transform);


                c.GetComponent<EnemyHealth>().TakeDamage(10, theVfx.transform.position, 0.0001f);
                player.GetComponent<PlayerHealth>().Heal(10);

                var ai = c.GetComponentInParent<AIPath>();
                if (ai != null)
                {
                    /*
                     PlayerAbilityManager manager = GameObject.FindObjectOfType<PlayerAbilityManager>();
    if (manager != null)
    {
        manager.StartCoroutine(manager.disableAI(ai));
    }
    */
                    ai.enabled = false;
                }
                else
                {
                    Debug.LogWarning("No AIPath found on " + c.name);
                }
            }
            player.GetComponent<PlayerAbilityManager>().canUseAbility = true;
        }

    }




    class Rogue : Ability
    {
        public AbilityType Type => AbilityType.Rogue;
        public GameObject vfx;

        public KeyCode key;
        public float radius = 10f;
        public GameObject circle;
        public GameObject effPanel;
        public float defaultCool;
        public float defaultMoveSpeed;
        public float energyCost;
        public float ogHealth;
        public Rogue(GameObject effPanel, GameObject vfx, float energyCost)
        {
            this.effPanel = effPanel;
            this.vfx = vfx;
            this.energyCost = energyCost;
        }

        public void OnHoldStart()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.CheckEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            if (!pam.canUseAbility) return;
            pam.canUseAbility = false;
            Activate();
        }
        public void Activate()
        {

            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.UseEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            effPanel.SetActive(true);
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            ogHealth = ph.health;
            if (ph.health > 10)
            {
                ph.setHealth(10);
            }
            GameObject theVfx = Instantiate(vfx, player.position, Quaternion.identity);
            Destroy(theVfx, 1.5f);
            defaultMoveSpeed = PlayerMovement.moveSpeed;
            PlayerMovement.moveSpeed += (3f / 4f) * defaultMoveSpeed;
            defaultCool = DirectionalAttack.cooldown;
            DirectionalAttack.cooldown -= defaultCool / 2f;
            SpriteRenderer playerSprite = playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

            PlayerAbilityManager manager = GameObject.FindObjectOfType<PlayerAbilityManager>();
            // 00E9FF
            if (manager != null)
            {
                manager.Invoke("DeactivateRogue", 5f);
            }
        }

        public void Deactivate()
        {
            PlayerMovement.moveSpeed = defaultMoveSpeed;
            DirectionalAttack.cooldown = defaultCool;

            effPanel.SetActive(false);
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            ph.setHealth(ogHealth);
            player.GetComponent<PlayerAbilityManager>().canUseAbility = true;
        }

        public void OnHoldEnd()
        {
            return;
        }

    }


    class slash360 : Ability
    {
        public AbilityType Type => AbilityType.Slash360;
        public GameObject vfx;

        public KeyCode key;
        public float radius = 3f;


        public float energyCost;
        public slash360(GameObject vfx, float energyCost)
        {
            this.vfx = vfx;
            this.energyCost = energyCost;
        }

        public void OnHoldStart()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.CheckEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            player.GetComponent<PlayerAbilityManager>().canUseAbility = false;
            Activate();
        }

        public void Activate()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerAbilityManager pam = player.GetComponent<PlayerAbilityManager>();
            if (!pam.UseEnergy(energyCost))
            {
                Debug.Log("Not enough Soul Energy!");
                return;
            }
            Vector2 playerPos = player.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, radius);
            GameObject theVfx = Instantiate(vfx, playerPos, Quaternion.identity);
            theVfx.transform.SetParent(player);
            foreach (Collider2D c in colliders)
            {
                if (!c.CompareTag("Enemy")) continue;




                c.GetComponent<EnemyHealth>().TakeDamage(50, theVfx.transform.position, 10f);

                var ai = c.GetComponentInParent<AIPath>();
                if (ai != null)
                {
                    ai.enabled = false;
                }
                else
                {
                    Debug.LogWarning("No AIPath found on " + c.name);
                }
            }
            player.GetComponent<PlayerAbilityManager>().canUseAbility = true;

            // 00E9FF
        }

        public void OnHoldEnd()
        {

        }
    }




}





 
#endregion