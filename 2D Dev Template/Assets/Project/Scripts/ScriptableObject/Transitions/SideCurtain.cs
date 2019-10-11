using UnityEngine;

[CreateAssetMenu(fileName = "SideCurtain", menuName = "MovingTransion/SideCurtainTransition")]
public class SideCurtain : Transition {

    public float startingX = 550.0f;
    [Range(0.0f, 50.0f)]
    public float maxHidePosition = 15.0f;

    public override (Vector3, Vector3) GetStartingAndFinalPosition(Vector3 rectTransformPos) {

        float y = rectTransformPos.y;

        return (new Vector3(startingX, y), new Vector3(0f, y));
    }

    protected override bool ReadyToDisable(RectTransform rectTransform, Vector3 targetPosition) {

        float x = Mathf.Abs(rectTransform.anchoredPosition.x - targetPosition.x);

        return (x < maxHidePosition);
    }
}
