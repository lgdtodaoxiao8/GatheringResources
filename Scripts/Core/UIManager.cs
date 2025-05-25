using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public GameManager gm;
    public ResourceSpawner2D spawner;
    public Slider countSlider, speedSlider;
    public InputField intervalInput;
    public Toggle pathToggle;
    public Text redCountText, blueCountText;
    public BaseStation2D baseRed, baseBlue;
    int redCount, blueCount;
    void Start() {
        
        gm.perFaction = (int)countSlider.value;
        gm.SpawnDrones();
        countSlider.onValueChanged.AddListener(v => {
            gm.perFaction = (int)v; gm.SpawnDrones(); });
        speedSlider.onValueChanged.AddListener(v =>
        {
            gm.droneSpeed = v;
            foreach (var d in Object.FindObjectsByType<DroneAI2D>(FindObjectsSortMode.None)) d.speed = v;
        });
        intervalInput.onValueChanged.AddListener(s =>
        {
            if (float.TryParse(s, out var f)) spawner.SetInterval(f);
        });
        pathToggle.onValueChanged.AddListener(v =>
        {
            gm.drawPaths = v;
            foreach (var d in Object.FindObjectsByType<DroneAI2D>(FindObjectsSortMode.None)) d.drawPath = v;
        });
        baseRed.GetComponent<BaseStation2D>().onResourceDelivered.AddListener(() =>
        {
            redCount++; redCountText.text = redCount.ToString();
        });
        baseBlue.GetComponent<BaseStation2D>().onResourceDelivered.AddListener(() =>
        {
            blueCount++; blueCountText.text = blueCount.ToString();
        });
    }
}