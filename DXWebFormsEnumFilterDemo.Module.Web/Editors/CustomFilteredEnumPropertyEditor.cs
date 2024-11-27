using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.Web.Internal.XmlProcessor;
using DXWebFormsEnumFilterDemo.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DXWebFormsEnumFilterDemo.Module.Web.Editors
{
    [PropertyEditor(typeof(ProductCategory), true)]
    public class CustomFilteredEnumPropertyEditor : WebPropertyEditor
    {
        public CustomFilteredEnumPropertyEditor(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }
        protected override WebControl CreateViewModeControlCore()
        {
            Label control = new Label();
            control.ID = "editor";            
            return control;
        }
        protected override WebControl CreateEditModeControlCore()
        {
            var enumValues = Enum.GetNames(typeof(ProductCategory)).ToList();
            DropDownList control = new DropDownList();
            control.ID = "editor";
            enumValues.ForEach(x =>
            {
                if(x.StartsWith("C"))
                control.Items.Add(x);
            });            
            control.SelectedIndexChanged += control_SelectedIndexChanged;
            return control;
        }

        void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditValueChangedHandler(sender, e);
        }
        protected override object GetControlValueCore()
        {            
            ProductCategory result = (ProductCategory)Enum.Parse(typeof(ProductCategory), ((DropDownList)Editor).SelectedValue);
            return result;           
        }
        protected override void ReadEditModeValueCore()
        {
            ((DropDownList)Editor).SelectedValue = ((ProductCategory)PropertyValue).ToString();
        }
        protected override void ReadViewModeValueCore()
        {
            ((Label)InplaceViewModeEditor).Text = ((ProductCategory)PropertyValue).ToString();
        }
    }
}
