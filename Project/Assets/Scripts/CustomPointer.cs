using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Pointers;
using Liminal.Core;
public class CustomPointer : MonoBehaviour
{
    public CustomReticule ReticulePrefab;
    public float DefaultDrawDistance;
    public LayerMask InteractionLayers;

    private IVRInputDevice _primaryInput;
    private IVRPointer _pointer;
    private CustomReticule _reticule;

    // Start is called before the first frame update
    void Start()
    {
        _reticule = Instantiate(ReticulePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (_primaryInput == null) {
            _primaryInput = VRDevice.Device.PrimaryInputDevice;
            _pointer = _primaryInput.Pointer;
            return;
        }

        if (_reticule == null)
            return;

        _reticule.transform.position = GetReticuleDrawPosition();
    }

    private Vector3 GetReticuleDrawPosition() {

        var hitPoint = _pointer.Transform.position + (_pointer.Transform.forward * DefaultDrawDistance);

        if (Physics.Raycast(_pointer.Transform.position, _pointer.Transform.forward, out RaycastHit hitInfo, Mathf.Infinity, InteractionLayers)) {
            hitPoint = hitInfo.point;
            _reticule.SetRotation(hitInfo.normal);
        } else {
            _reticule.SetRotation(_reticule.transform.position - Camera.main.transform.position);
        }

        Debug.DrawRay(_pointer.Transform.position, _pointer.Transform.forward * 5f, Color.red);



        return hitPoint;
    }
}
