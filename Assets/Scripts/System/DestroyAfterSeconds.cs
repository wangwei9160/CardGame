using UnityEngine;

public class UIDestroyAfterSeconds : MonoBehaviour
{
    public float WaitForSeconds = 0.3f;
    public float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > WaitForSeconds)
        {
            UIManager.Instance.Close(gameObject.GetComponent<UIViewBase>().Name);
        }
    }

}
