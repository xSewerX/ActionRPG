using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction toggleStatsAction;
    private InputAction toggleSkillTreeAction;
    private InputAction rageAction;
    private InputAction coneOfColdAction;
    private InputAction interaction;
    public static bool AttackPressed;
    public static bool ToggleStats;
    public static bool ToggleSkillTree;
    public static bool ActivateRage;
    public static bool UseConeOfCold;
    public static bool InteractPressed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        attackAction = playerInput.actions["Attack"];
        toggleStatsAction = playerInput.actions["ToggleStats"];
        toggleSkillTreeAction = playerInput.actions["ToggleSkillTree"];
        rageAction = playerInput.actions["AbilityRage"];
        coneOfColdAction = playerInput.actions["AbilityConeOfCold"];
        interaction = playerInput.actions["Interact"];

    }
    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        AttackPressed = attackAction.WasPressedThisFrame();
        ToggleStats = toggleStatsAction.WasPressedThisFrame();
        ToggleSkillTree = toggleSkillTreeAction.WasPressedThisFrame();
        ActivateRage = rageAction.WasPressedThisFrame();
        UseConeOfCold = coneOfColdAction.WasPressedThisFrame();
        InteractPressed = interaction.WasPressedThisFrame();

    }
}
