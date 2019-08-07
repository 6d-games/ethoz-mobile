using UnityEngine;
using UnityEditor;
using System.Reflection;

[RequireComponent(typeof(MeshFilter), typeof(Renderer))]
public class LowPolyWaterv2 : MonoBehaviour
{
    [SerializeField]
    private WaveModel _waveModel = null;

    [SerializeField, Tooltip("Use this for testing wave models.")]
    private bool _updateMaterialPerFrame = false;

    [Header("Quad Detail"), SerializeField, InspectorButton("SubdivideMesh")]
    private bool _subdivideMesh;

    private Material _lowPolyWater;
	private Vector4[] _wave0;
	private Vector4[] _wave1;

    private void Start()
    {
        if (_waveModel == null)
        {
            enabled = false;
            return;
        }

        _lowPolyWater = GetComponent<Renderer>().sharedMaterial;

        if (!_lowPolyWater)
        {
            enabled = false;
            return;
        }
			
        _lowPolyWater.SetInt("_Waves", _waveModel.Length);
        _lowPolyWater.SetFloat("_TimeScale", _waveModel.Timescale);
		_wave0 = new Vector4[_waveModel.Length];
		_wave1 = new Vector4[_waveModel.Length];

        for (var i = 0; i < _waveModel.Length; i++)
        {
            var a = _waveModel[i].amplitude;
            var f = 2.0f * Mathf.PI / _waveModel[i].waveLength;
            var p = _waveModel[i].speed * f;
            var radA = _waveModel[i].travelAngle * Mathf.Deg2Rad;
            var d = new Vector2(Mathf.Sin(radA), Mathf.Cos(radA));
            var s = _waveModel[i].sharpness;
			_wave0[i] = new Vector4(a, f, p, 0);
			_wave1[i] = new Vector4(d.x, d.y, s, 0);
        }

		_lowPolyWater.SetVectorArray("_SineWave0", _wave0);
		_lowPolyWater.SetVectorArray("_SineWave1", _wave1);
    }

    private void Update()
    {
        if (!_updateMaterialPerFrame) return;

        _lowPolyWater.SetInt("_Waves", _waveModel.Length);
        _lowPolyWater.SetFloat("_TimeScale", _waveModel.Timescale);

        for (var i = 0; i < _waveModel.Length; i++)
        {
            var a = _waveModel[i].amplitude;
            var f = 2.0f * Mathf.PI / _waveModel[i].waveLength;
            var p = _waveModel[i].speed * f;
            var radA = _waveModel[i].travelAngle * Mathf.Deg2Rad;
            var d = new Vector2(Mathf.Sin(radA), Mathf.Cos(radA));
            var s = _waveModel[i].sharpness;
            _wave0[i].Set(a, f, p, 0);
            _wave1[i].Set(d.x, d.y, s, 0);
        }

        _lowPolyWater.SetVectorArray("_SineWave0", _wave0);
        _lowPolyWater.SetVectorArray("_SineWave1", _wave1);
    }

    private void SubdivideMesh()
    {
        var current = GetComponent<MeshFilter>().sharedMesh;
        var newMesh = Instantiate(current);
        newMesh.name = current.name;
        MeshSubdivider.Subdivide4(newMesh);
        GetComponent<MeshFilter>().sharedMesh = newMesh;
    }
}

[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
    public readonly string MethodName;

    public InspectorButtonAttribute(string MethodName)
    {
        this.MethodName = MethodName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        var inspectorButtonAttribute = (InspectorButtonAttribute)attribute;
        var buttonRect = new Rect(position.x + (position.width - Screen.width + 10) * 0.5f, position.y, Screen.width - 20, position.height);
        if (GUI.Button(buttonRect, label.text))
        {
            var eventOwnerType = prop.serializedObject.targetObject.GetType();
            var eventName = inspectorButtonAttribute.MethodName;

            if (_eventMethodInfo == null)
                _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (_eventMethodInfo != null)
                _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
            else
                Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
        }
    }
}
#endif

