using UnityEngine;

/// <summary>
/// Provides the references to the unity components for SingleComponentInstanceLocator
/// </summary>
public class SingleComponentInstanceReferences : MonoBehaviour
{
    // Component instances
    public AISpawnComponent aISpawnComponent;
    public AudioLevelComponent audioLevelComponent;
    public SoundEffectPoolComponent soundEffectPoolComponent;
    public CameraDirectorComponent cameraDirectorComponent;
    public ParticlePoolComponent particlePoolComponent;
    public SpriteEffectsComponent spriteEffectsComponent;
    public TimeScalesComponent timeScalesComponent;
    public MenuStartComponent menuStartComponent;
    public PlayerScoreComponent playerScoreComponent;
    public PlayerIdentifierComponent playerIdentifierComponent;
    public ShopItemPanelComponent shopItemPanelComponent;
    public ShopItemLibraryComponent shopItemLibraryComponent;
    public GameObject IAPShopCanvas;
    public FireButtonSetupComponent fireButtonSetupComponent;

    private void Start()
    {
        SingleObjectInstanceLocator.RegisterComponentReferences(this);
    }
}