using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LowPolyWaterv2Reflection : MonoBehaviour
{
    [SerializeField]
    private bool _disablePixelLights = true;
    [SerializeField]
    private int _textureSize = 256;
    [SerializeField]
    private float _clipPlaneOffset = 0.07f;
    [SerializeField]
    private LayerMask _reflectLayers = -1;

    private Hashtable _reflectionCameras = new Hashtable();

    private RenderTexture _reflectionTexture;
    private int _oldReflectionTextureSize;

    private static bool _insideRendering;

    public void OnWillRenderObject()
    {
        var rend = GetComponent<Renderer>();

        if (!enabled || !rend || !rend.sharedMaterial || !rend.enabled)
            return;

        var cam = Camera.current;

        if (!cam)
            return;
       
        if (_insideRendering)
            return;

        _insideRendering = true;

        Camera reflectionCamera;
        CreateMirrorObjects(cam, out reflectionCamera);

        var pos = transform.position;
        var normal = transform.up;
        var oldPixelLightCount = QualitySettings.pixelLightCount;

        if (_disablePixelLights)
        {
            QualitySettings.pixelLightCount = 0;
        }

        UpdateCameraModes(cam, reflectionCamera);

        var d = -Vector3.Dot(normal, pos) - _clipPlaneOffset;
        var reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);
        var reflection = Matrix4x4.zero;

        CalculateReflectionMatrix(ref reflection, reflectionPlane);

        var oldpos = cam.transform.position;
        var newpos = reflection.MultiplyPoint(oldpos);

        reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

        var clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
        var projection = cam.CalculateObliqueMatrix(clipPlane);

        reflectionCamera.projectionMatrix = projection;
        reflectionCamera.cullingMask = ~(1 << 4) & _reflectLayers.value;
        reflectionCamera.targetTexture = _reflectionTexture;
        GL.invertCulling = true;
        reflectionCamera.transform.position = newpos;
        var euler = cam.transform.eulerAngles;
        reflectionCamera.transform.eulerAngles = new Vector3(0, euler.y, euler.z);
        reflectionCamera.Render();
        reflectionCamera.transform.position = oldpos;
        GL.invertCulling = false;
        var materials = rend.sharedMaterials;

        foreach (var mat in materials)
        {
            if (mat.HasProperty("_ReflectionTex"))
                mat.SetTexture("_ReflectionTex", _reflectionTexture);
        }

        if (_disablePixelLights)
            QualitySettings.pixelLightCount = oldPixelLightCount;

        _insideRendering = false;
    }

    void OnDisable()
    {
        if (_reflectionTexture)
        {
            DestroyImmediate(_reflectionTexture);
            _reflectionTexture = null;
        }

        foreach (DictionaryEntry kvp in _reflectionCameras)
        {
            DestroyImmediate(((Camera) kvp.Value).gameObject);
        }

        _reflectionCameras.Clear();
    }

    private void UpdateCameraModes(Camera src, Camera dest)
    {
        if (dest == null)
            return;

        dest.clearFlags = src.clearFlags;
        dest.backgroundColor = src.backgroundColor;
        
        if (src.clearFlags == CameraClearFlags.Skybox)
        {
            var sky = src.GetComponent(typeof(Skybox)) as Skybox;
            var mysky = dest.GetComponent(typeof(Skybox)) as Skybox;

            if (!sky || !sky.material)
            {
                mysky.enabled = false;
            }
            else
            {
                mysky.enabled = true;
                mysky.material = sky.material;
            }
        }

        dest.farClipPlane = src.farClipPlane;
        dest.nearClipPlane = src.nearClipPlane;
        dest.orthographic = src.orthographic;
        dest.fieldOfView = src.fieldOfView;
        dest.aspect = src.aspect;
        dest.orthographicSize = src.orthographicSize;
    }

    private void CreateMirrorObjects(Camera currentCamera, out Camera reflectionCamera)
    {
        reflectionCamera = null;

        if (!_reflectionTexture || _oldReflectionTextureSize != _textureSize)
        {
            if (_reflectionTexture)
            {
                DestroyImmediate(_reflectionTexture);
            }

            _reflectionTexture = new RenderTexture(_textureSize, _textureSize, 16);
            _reflectionTexture.name = "__MirrorReflection" + GetInstanceID();
            _reflectionTexture.isPowerOfTwo = true;
            _reflectionTexture.hideFlags = HideFlags.DontSave;
            _oldReflectionTextureSize = _textureSize;
        }

        reflectionCamera = _reflectionCameras[currentCamera] as Camera;

        if (reflectionCamera) return;

        var go = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));

        reflectionCamera = go.GetComponent<Camera>();
        reflectionCamera.enabled = false;
        reflectionCamera.transform.position = transform.position;
        reflectionCamera.transform.rotation = transform.rotation;
        reflectionCamera.gameObject.AddComponent<FlareLayer>();
        go.hideFlags = HideFlags.HideAndDontSave;
        _reflectionCameras[currentCamera] = reflectionCamera;
    }

    private static float sgn(float a)
    {
        if (a > 0.0f)
            return 1.0f;
        if (a < 0.0f)
            return -1.0f;
        return 0.0f;
    }

    private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        var offsetPos = pos + normal * _clipPlaneOffset;
        var m = cam.worldToCameraMatrix;
        var cpos = m.MultiplyPoint(offsetPos);
        var cnormal = m.MultiplyVector(normal).normalized * sideSign;

        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
    }

    private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
    }
}