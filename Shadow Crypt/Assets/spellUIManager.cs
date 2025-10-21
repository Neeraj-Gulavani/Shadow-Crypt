using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class spellUIManager : MonoBehaviour
{
    PlayerControls controls;
    public GameObject spellUI;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.SpellWheelOpen.started += ctx => EnableSpellWheel();
    }

    public void EnableSpellWheel()
    {
        spellUI.SetActive(true);
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);

        }
        Time.timeScale = 0f;
    }

    public void DisableSpellWheel()
    {
        spellUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void SetActiveSpell(AbilityType type)
    {
        PlayerAbilityManager pam = FindObjectOfType<PlayerAbilityManager>();
        pam.currentAbility = type;

        DisableSpellWheel();
    }

public void SetLightning()
{
    SetActiveSpell(AbilityType.Lightning);
}

public void SetSoulDrain()
{
    SetActiveSpell(AbilityType.SoulDrain);
}

public void SetRogue()
{
    SetActiveSpell(AbilityType.Rogue);
}

public void SetSlash360()
{
    SetActiveSpell(AbilityType.Slash360);
}


}
