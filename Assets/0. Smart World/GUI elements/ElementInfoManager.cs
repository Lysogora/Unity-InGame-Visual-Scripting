using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ElementInfoManager : MonoBehaviour {

	public static ElementInfoManager inst;

	public BaseElement processElement;
	public BaseActivityElement processActivityElement;

	public IwPropertyValue<PropertyType, int> processedProp;
	public IElementFunction<BaseElement> processedFunc;

	// info slots vars
	public bool showInfoSlots;
	public GameObject elInfoGO;
	public RectTransform propertiesInfoTable;
	public RectTransform functionsInfoTable;
	public List <BaseSlot> propInfoSlots = new List<BaseSlot> ();
	public List <BaseSlot> funcInfoSlots = new List<BaseSlot> ();

	//edit slots vars and data
	public bool showEditScreen;
	public GameObject elEditGO;
	public RectTransform propertiesEditTable;
	public RectTransform functionsEditTable;
	public List <BaseSlot> propEditSlots = new List<BaseSlot> ();
	public List <BaseSlot> funcEditSlots = new List<BaseSlot> ();
	public GameObject upValueButton;
	public GameObject downValueButton;
	public GameObject deleteButton;
	public GameObject addButton;

	public Image infoImg;
	public Text expInfoText;
	public Text descriptionText;

	// side function buttons
	public List <BaseSlot> funcButtons = new List<BaseSlot> ();


	//prefabs
	public GameObject propertyInfoPrefab;
	public GameObject functionInfoPrefab;
	public GameObject propertyEditPrefab;
	public GameObject functionEditPrefab;
	public GameObject propertyAddEditPrefab;
	public GameObject functionAddEditPrefab;

	public Sprite emptyButton;

	public Color baseSlotColor;
	public Color selectedSlotColor;
	public Color availableSlotColor;

	void Awake(){
		inst = this;
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (InfoUpdateCoroutine ());
	}

	public void ProcessElement(BaseElement _baseElement){
		if ((processElement == null) ||
		    (processElement != null && _baseElement != processElement)) {
			processElement = _baseElement;
			processActivityElement = null;
			ElementEditor.inst.processActivityElement = null;
			ShowInfoSlots();
		}
	}
	public void ProcessElement(BaseActivityElement _baseElement){
		if ((processActivityElement == null) ||
		    (processActivityElement != null && _baseElement != processActivityElement)) {
			processActivityElement = _baseElement;
			ElementEditor.inst.processActivityElement = _baseElement;
			processElement = _baseElement;
			ShowInfoSlots();
		}
	}

	//SET COMPOUND WE WILL WORK WITH
	public void EditElementCompound(IwPropertyValue<PropertyType, int> _prop){
		processedProp = null;
		processedFunc = null;
		processedProp = _prop;
		ShowEditedObjectInfo ();
	}
	public void EditElementCompound(IElementFunction<BaseElement> _func){
		processedProp = null;
		processedFunc = null;
		processedFunc = _func;
		ShowEditedObjectInfo ();
	}


	public void ShowDetailedInfo(){
		showInfoSlots = !showInfoSlots;
		if (!showInfoSlots) {
			elInfoGO.SetActive (false);
		} else {
			elInfoGO.SetActive (true);
			ShowInfoSlots();
		}
	}
	public void ShowElementEditorScreen(){
		showEditScreen = !showEditScreen;
		if (!showEditScreen) {
			elEditGO.SetActive (false);
		} else {
			elEditGO.SetActive (true);
			ShowElementEditor();
		}
	}

	public void ShowInfoSlots(){
		if (showInfoSlots && processElement != null) {
			PreparePropertyInfoSlots (processElement);
			if (processActivityElement != null) {
				PrepareFunctionsInfoSlots (processActivityElement);
				PrepareFunctionButtons (processActivityElement);
			}
		}
		if (!showInfoSlots && processActivityElement != null) {
			PrepareFunctionButtons (processActivityElement);
		}
	}

	public void ShowElementEditor(){
		if (showEditScreen && processElement != null) {
			PreparePropertyEditSlots (processElement);
			if (processActivityElement != null) {
				PrepareFunctionsEditSlots (processActivityElement);
			}
		}
	}

	public IEnumerator InfoUpdateCoroutine(){
		while (true) {
			if(showInfoSlots){
				UpdateInfoSlotsValues();
			}
			if (showEditScreen){
				UpdateEditSlotsValues();
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	//OPERATIONS WITH PROPERTIES AND FUNCTION (DELETING, ADDING, CHANGING PROPERTY VALUES)

	public void AddSelectedComponent(){
		if(processedProp != null)
			ElementEditor.inst.AddProperty (processedProp.propType);
		if(processedFunc != null)
			ElementEditor.inst.AddFunction (processedFunc.functionType);
		ShowElementEditor ();
	}
	
	public void DeleteSelectedComponent(){
		if(processedProp != null)
			ElementEditor.inst.DeleteProperty (processedProp.propType);
		if(processedFunc != null)
			ElementEditor.inst.DeleteFunction (processedFunc.functionType);
		ShowElementEditor ();
	}
	
	public void IncreaseSelectedProppertyValue(){
		ElementEditor.inst.AddProppertyValue (processedProp);
		UpdateEditSlotsValues();
	}
	
	public void DecreaseSelectedPropertyValue(){
		ElementEditor.inst.DecreasePropertyValue (processedProp);
		UpdateEditSlotsValues();
	}

//-------------------------------------------------------------------------------

	void UpdateInfoSlotsValues(){
	for (int i = 0; i < propInfoSlots.Count; i++) {
			propInfoSlots[i].GetSlotElement(SlotElemetType.Value).text.text = propInfoSlots[i].objectIdentificator.GetProp().val.ToString() +" / "
				+propInfoSlots[i].objectIdentificator.GetProp().maxVal.ToString();
		}
	}

	void UpdateEditSlotsValues(){
		for (int i = 0; i < propEditSlots.Count; i++) {
			propEditSlots[i].GetSlotElement(SlotElemetType.Value).text.text = propEditSlots[i].objectIdentificator.GetProp().maxVal.ToString();
		}
	}





	void PreparePropertyInfoSlots(BaseElement _be){
		Transform[] tr = propertiesInfoTable.gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 1; i < tr.Length; i++) {
			Destroy(tr[i].gameObject);
		}
		propInfoSlots.Clear ();

		for (int i = 0; i < _be.propertiesList.Count; i++) {
			GameObject _slot = Instantiate(propertyInfoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			_slot.transform.SetParent(propertiesInfoTable.transform);
			_slot.GetComponent<RectTransform>().localScale = Vector3.one;
			BaseSlot _bs = _slot.GetComponent<BaseSlot>();
			propInfoSlots.Add(_bs);
			_bs.GetSlotElement(SlotElemetType.Title).text.text = GlobalData.inst.GetDataBlock(_be.propertiesList[i].propType).name;
			_bs.GetSlotElement(SlotElemetType.Value).text.text = _be.propertiesList[i].val.ToString() + " / " + _be.propertiesList[i].maxVal.ToString();

			EditedOnjectIdentificator editedObj = _slot.GetComponent<EditedOnjectIdentificator>();
			editedObj.SetIdentificator(_be.propertiesList[i]);
		}
	}
	void PrepareFunctionsInfoSlots(BaseActivityElement _be){
		Transform[] tr = functionsInfoTable.gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 1; i < tr.Length; i++) {
			Destroy(tr[i].gameObject);
		}
		funcInfoSlots.Clear ();
		
		for (int i = 0; i < _be.functions.Count; i++) {
			GameObject _slot = Instantiate(functionInfoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			_slot.transform.SetParent(functionsInfoTable.transform);
			_slot.GetComponent<RectTransform>().localScale = Vector3.one;
			BaseSlot _bs = _slot.GetComponent<BaseSlot>();
			funcInfoSlots.Add(_bs);
			_bs.GetSlotElement(SlotElemetType.Title).text.text = GlobalData.inst.GetDataBlock(_be.functions[i].functionType).name;

		}
	}
	void PreparePropertyEditSlots(BaseElement _be){
		Transform[] tr = propertiesEditTable.gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 1; i < tr.Length; i++) {
			Destroy(tr[i].gameObject);
		}
		propEditSlots.Clear ();
		//show already available properties
		for (int i = 0; i < _be.propertiesList.Count; i++) {
			GameObject _slot = Instantiate(propertyEditPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			_slot.transform.SetParent(propertiesEditTable.transform);
			_slot.GetComponent<RectTransform>().localScale = Vector3.one;
			BaseSlot _bs = _slot.GetComponent<BaseSlot>();
			propEditSlots.Add(_bs);
			_bs.GetSlotElement(SlotElemetType.Title).text.text = GlobalData.inst.GetDataBlock(_be.propertiesList[i].propType).name;
			_bs.GetSlotElement(SlotElemetType.Value).text.text = _be.propertiesList[i].val.ToString() + " / " + _be.propertiesList[i].maxVal.ToString();

			EditedOnjectIdentificator editedObj = _slot.GetComponent<EditedOnjectIdentificator>();
			editedObj.SetIdentificator(_be.propertiesList[i]);
		}
		//now list  other properties that can be edited
		PropertyType[] propTypes = (PropertyType[])System.Enum.GetValues (typeof(PropertyType));
		for (int i = 0; i < propTypes.Length; i++) {
			if(!processActivityElement.elProperties.ContainsKey(propTypes[i])){
				GameObject _slot = Instantiate(propertyAddEditPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				_slot.transform.SetParent(propertiesEditTable.transform);
				_slot.GetComponent<RectTransform>().localScale = Vector3.one;
				BaseSlot _bs = _slot.GetComponent<BaseSlot>();
				propEditSlots.Add(_bs);
				_bs.GetSlotElement(SlotElemetType.Title).text.text = GlobalData.inst.GetDataBlock(propTypes[i]).name;
				//_bs.GetSlotElement(SlotElemetType.Value).text.text = _be.propertiesList[i].val.ToString() + " / " + _be.propertiesList[i].maxVal.ToString();

				EditedOnjectIdentificator editedObj = _slot.GetComponent<EditedOnjectIdentificator>();
				BaseElementProperty newProp = new BaseElementProperty();
				newProp.propType = propTypes[i];
				editedObj.SetIdentificator(newProp);
			}
		}

	}
	void PrepareFunctionsEditSlots(BaseActivityElement _be){
		Transform[] tr = functionsEditTable.gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 1; i < tr.Length; i++) {
			Destroy(tr[i].gameObject);
		}
		funcEditSlots.Clear ();
		//show already available properties
		for (int i = 0; i < _be.functions.Count; i++) {
			GameObject _slot = Instantiate(functionEditPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			_slot.transform.SetParent(functionsEditTable.transform);
			_slot.GetComponent<RectTransform>().localScale = Vector3.one;
			BaseSlot _bs = _slot.GetComponent<BaseSlot>();
			funcEditSlots.Add(_bs);
			_bs.GetSlotElement(SlotElemetType.Title).text.text = GlobalData.inst.GetDataBlock(_be.functions[i].functionType).name;
			
		}
		//now list  other properties that can be edited
		FunctionType[] funcTypes = (FunctionType[])System.Enum.GetValues (typeof(FunctionType));
		for (int i = 0; i < funcTypes.Length; i++) {
			if(!processActivityElement.functionsDict.ContainsKey(funcTypes[i])){
				GameObject _slot = Instantiate(functionAddEditPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				_slot.transform.SetParent(functionsEditTable.transform);
				_slot.GetComponent<RectTransform>().localScale = Vector3.one;
				BaseSlot _bs = _slot.GetComponent<BaseSlot>();
				funcEditSlots.Add(_bs);
				_bs.GetSlotElement(SlotElemetType.Title).text.text = GlobalData.inst.GetDataBlock(funcTypes[i]).name;
				
				EditedOnjectIdentificator editedObj = _slot.GetComponent<EditedOnjectIdentificator>();
				BaseElementFunction newFunc = new BaseElementFunction();
				newFunc.functionType = funcTypes[i];
				editedObj.SetIdentificator(newFunc);
			}
		}
	}

	void PrepareFunctionButtons(BaseActivityElement _be){
	for (int i = 0; i < funcButtons.Count; i++) {
			if(i < _be.functions.Count){
				funcButtons[i].gameObject.SetActive(true);
				funcButtons[i].GetSlotElement(SlotElemetType.Icon).image.sprite = GlobalData.inst.GetDataBlock(_be.functions[i].functionType).icon;
				funcButtons[i].GetSlotElement(SlotElemetType.Icon).image.color = GlobalData.inst.GetDataBlock(_be.functions[i].functionType).color;
			}
			else{
				funcButtons[i].gameObject.SetActive(false);
			}
		}
	}

	//SHOW INFO ABOUT THE EDITED FUNCTION OR PROPERTY AT THE EDITOR SCREEN
	void ShowEditedObjectInfo(){
		upValueButton.SetActive(false);
		downValueButton.SetActive(false);
		deleteButton.SetActive(false);
		addButton.SetActive (false);
		//show property info
		if (processedProp != null) {
			upValueButton.SetActive(true);
			downValueButton.SetActive(true);
			if(processActivityElement.propertiesList.Contains(processedProp)){
				deleteButton.SetActive(true);
			}
			else{
				addButton.SetActive(true);
			}
			descriptionText.text = GlobalData.inst.GetDataBlock(processedProp.propType).name +"/"+ 
				GlobalData.inst.GetDataBlock(processedProp.propType).description;
			infoImg.sprite = GlobalData.inst.GetDataBlock(processedProp.propType).bigIcon;
		}
		//show function info
		if (processedFunc != null) {
			if(processActivityElement.functions.Contains(processedFunc)){
				deleteButton.SetActive(true);
			}
			else{
				addButton.SetActive(true);
			}
			descriptionText.text = GlobalData.inst.GetDataBlock(processedFunc.functionType).name +"/"+ 
				GlobalData.inst.GetDataBlock(processedFunc.functionType).description;
			infoImg.sprite = GlobalData.inst.GetDataBlock(processedFunc.functionType).bigIcon;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
