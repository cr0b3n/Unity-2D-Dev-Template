using UnityEngine;

[CreateAssetMenu(fileName = "TopDownCurtain", menuName = "MovingTransion/TopDownCurtainTransition")]
public class TopDownCurtain : Transition {

    public float startingY = 550.0f;
    [Range(0.0f, 50.0f)]
    public float maxHidePosition = 15.0f;

    public override (Vector3, Vector3) GetStartingAndFinalPosition(Vector3 rectTransformPos) {

        float x = rectTransformPos.x;

        return (new Vector3(x, startingY), new Vector3(x, 0f));
    }

    protected override bool ReadyToDisable(RectTransform rectTransform, Vector3 targetPosition) {

        float y = Mathf.Abs(rectTransform.anchoredPosition.y - targetPosition.y);

        return (y < maxHidePosition);
    }
}
