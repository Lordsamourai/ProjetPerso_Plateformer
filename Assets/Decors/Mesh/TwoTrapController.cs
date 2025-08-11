using System.Collections;
using UnityEngine;

public class TwoTrapController : MonoBehaviour
{
    [System.Serializable]
    public class TrapGroup
    {
        public Transform trapParent;         // TrapLeft ou TrapRight
        public Transform[] individualTraps;  // Trap1 à Trap9 (ordre croissant)
    }

    public TrapGroup rightTrap;
    public TrapGroup leftTrap;

    public float moveSpeed = 0.1f;
    public float maxDistance = 0.6f;
    private float currentDistance = 0f;

    private Vector3 leftStartPos;
    private Vector3 rightStartPos;

    private bool opening = true;
    private bool isReturning = false;

    void Start()
    {
        leftStartPos = leftTrap.trapParent.localPosition;
        rightStartPos = rightTrap.trapParent.localPosition;
    }

    void Update()
    {
        if (opening && currentDistance < maxDistance)
        {
            float delta = moveSpeed * Time.deltaTime;
            currentDistance = Mathf.Min(currentDistance + delta, maxDistance);

            MoveTraps(currentDistance);
            UpdateTrapVisibility(leftTrap, currentDistance);
            UpdateTrapVisibility(rightTrap, currentDistance);

            if (Mathf.Approximately(currentDistance, maxDistance))
            {
                opening = false;
                StartCoroutine(StartClosingAfterDelay(3f));
            }
        }
        else if (isReturning && currentDistance > 0f)
        {
            float delta = moveSpeed * Time.deltaTime;
            currentDistance = Mathf.Max(currentDistance - delta, 0f);

            MoveTraps(currentDistance);
            UpdateTrapVisibility(leftTrap, currentDistance);
            UpdateTrapVisibility(rightTrap, currentDistance);

            if (Mathf.Approximately(currentDistance, 0f))
            {
                isReturning = false;
            }
        }
    }

    void MoveTraps(float distance)
    {
        leftTrap.trapParent.localPosition = leftStartPos + new Vector3(0, 0, -distance);
        rightTrap.trapParent.localPosition = rightStartPos + new Vector3(0, 0, distance);
    }

    void UpdateTrapVisibility(TrapGroup trapGroup, float distance)
    {
        int totalTraps = trapGroup.individualTraps.Length;

        for (int i = 0; i < totalTraps; i++)
        {
            // Les deux dernières trappes (Trap8 et Trap9) ne disparaissent jamais
            if (i >= totalTraps - 2)
            {
                trapGroup.individualTraps[i].gameObject.SetActive(true);
            }
            else
            {
                float hideThreshold = (i + 1) * 0.0875f;
                bool shouldBeActive = distance < hideThreshold;
                trapGroup.individualTraps[i].gameObject.SetActive(shouldBeActive);
            }
        }
    }

    IEnumerator StartClosingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReturning = true;
    }
}
