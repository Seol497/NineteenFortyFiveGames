using System.Collections;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float scrollSpeed = 0.02f;
    float newOffsetY;
    Material myMaterial;


    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        newOffsetY = myMaterial.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        
        Vector2 newOffset = new Vector2 (myMaterial.mainTextureOffset.x, newOffsetY);

        myMaterial.mainTextureOffset = newOffset;
    }

    public IEnumerator TranslateBackGround(float elapsedTime, float duration)
    {
        yield return new WaitForSeconds (1.3f);
        Vector2 backScale = myMaterial.mainTextureScale;
        Vector2 backOffset = myMaterial.mainTextureOffset;
        Vector2 targetScale = new Vector2(1f, 0.13f);
        Vector2 targetOffset = new Vector2(1f, 0);
        while (elapsedTime < duration)
        {
            scrollSpeed = Mathf.Lerp(scrollSpeed, 0.1f, elapsedTime / duration);
            targetOffset = new Vector2(1f, 0);
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            Vector2 newScale = Vector2.Lerp(backScale, targetScale, t);
            Vector2 newOffsetx = Vector2.Lerp(backOffset, targetOffset, t);
            Vector2 newOffset = new Vector2(newOffsetx.x, myMaterial.mainTextureOffset.y);

            myMaterial.mainTextureScale = newScale;
            myMaterial.mainTextureOffset = newOffset;
            yield return null;
        }

        myMaterial.mainTextureScale = targetScale;
        myMaterial.mainTextureOffset = targetOffset;
    }

}
