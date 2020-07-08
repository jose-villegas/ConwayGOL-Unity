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
	private int _showingOption = 0;

	void Start()
	{
		_cellColorButton.onClick.AddListener(OnCellColorPicked);
		_trail1ColorButton.onClick.AddListener(OnTrail1ColorPicker);
		_trail2ColoButton.onClick.AddListener(OnTrail2ColorPicker);

		_animator = _colorPicker.GetComponent<Animator>();
	}

	private void OnTrail2ColorPicker()
	{
		if (_showingOption != 3) _colorPickerShown = false;

		if (_animator != null)
		{
			_animator.SetTrigger(_colorPickerShown ? "Hide" : "Show");
			_colorPickerShown = !_colorPickerShown;
			_showingOption = 3;
		}

		_colorPicker.onValueChanged.RemoveAllListeners();
		_colorPicker.CurrentColor = _trail2Material.color;
		_colorPicker.onValueChanged.AddListener(color => { _trail2Material.color = color; });
	}

	private void OnTrail1ColorPicker()
	{
		if (_showingOption != 2) _colorPickerShown = false;

		if (_animator != null)
		{
			_animator.SetTrigger(_colorPickerShown ? "Hide" : "Show");
			_colorPickerShown = !_colorPickerShown;
			_showingOption = 2;
		}

		_colorPicker.onValueChanged.RemoveAllListeners();
		_colorPicker.CurrentColor = _trail1Material.color;
		_colorPicker.onValueChanged.AddListener(color => { _trail1Material.color = color; });
	}

	private void OnCellColorPicked()
	{
		if (_showingOption != 1) _colorPickerShown = false;

		if (_animator != null)
		{
			_animator.SetTrigger(_colorPickerShown ? "Hide" : "Show");
			_colorPickerShown = !_colorPickerShown;
			_showingOption = 1;
		}

		_colorPicker.onValueChanged.RemoveAllListeners();
		_colorPicker.CurrentColor = _cellMaterial.color;
		_colorPicker.onValueChanged.AddListener(color => { _cellMaterial.color = color; });
	}
}