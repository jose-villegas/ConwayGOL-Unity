using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField] private Material _cellMaterial;
	[SerializeField] private Material _trail1Material;
	[SerializeField] private Material _trail2Material;

	[SerializeField] private Button _cellColorButton;
	[SerializeField] private Button _trail1ColorButton;
	[SerializeField] private Button _trail2ColoButton;

	[SerializeField] private ColorPicker _colorPicker;
	private Animator _animator;
	private bool _colorPickerShown;

	void Start()
	{
		_cellColorButton.onClick.AddListener(OnCellColorPicked);
		_trail1ColorButton.onClick.AddListener(OnTrail1ColorPicker);
		_trail2ColoButton.onClick.AddListener(OnTrail2ColorPicker);

		_animator = _colorPicker.GetComponent<Animator>();
	}

	private void OnTrail2ColorPicker()
	{
		if (_animator != null)
		{
			_animator.SetTrigger(_colorPickerShown ? "Hide" : "Show");
			_colorPickerShown = true;
		}

		_colorPicker.onValueChanged.RemoveAllListeners();
		_colorPicker.onValueChanged.AddListener(color => { _trail2Material.color = color; });
	}

	private void OnTrail1ColorPicker()
	{
		if (_animator != null)
		{
			_animator.SetTrigger(_colorPickerShown ? "Hide" : "Show");
			_colorPickerShown = true;
		}

		_colorPicker.onValueChanged.RemoveAllListeners();
		_colorPicker.onValueChanged.AddListener(color => { _trail1Material.color = color; });
	}

	private void OnCellColorPicked()
	{
		if (_animator != null)
		{
			_animator.SetTrigger(_colorPickerShown ? "Hide" : "Show");
			_colorPickerShown = true;
		}

		_colorPicker.onValueChanged.RemoveAllListeners();
		_colorPicker.onValueChanged.AddListener(color => { _cellMaterial.color = color; });
	}
}