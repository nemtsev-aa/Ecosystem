using UnityEngine;

public class EcosystemLifeTimePanel : UIPanel {
    [SerializeField] private TimerBar _timeBar;

    public void Init(TimeCounter counter) {
        _timeBar.Init(counter);
    }

    public override void Reset() {
        _timeBar.Reset();
    }
}
