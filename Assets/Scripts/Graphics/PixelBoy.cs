//PIXELBOY BY @WTFMIG EAT A BUTT WORLD BAHAHAHAHA POOP MY PANTS

using UnityEngine;
 
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/PixelBoy")]
public class PixelBoy : MonoBehaviour
{
    private Camera m_camera;
    
    private int m_width;
    [SerializeField] private int m_height = 240;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Update()
    {
        var ratio = ((float)m_camera.pixelWidth / m_camera.pixelHeight);
        m_width = Mathf.RoundToInt(m_height * ratio);
    }
    
    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        _source.filterMode = FilterMode.Point;
        
        var buffer = RenderTexture.GetTemporary(m_width, m_height, -1);
        buffer.filterMode = FilterMode.Point;
        
        Graphics.Blit(_source, buffer);
        Graphics.Blit(buffer, _destination);
        
        RenderTexture.ReleaseTemporary(buffer);
    }
}