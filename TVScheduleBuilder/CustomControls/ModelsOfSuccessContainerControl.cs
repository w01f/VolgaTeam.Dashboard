using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TVScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ModelsOfSuccessContainerControl : UserControl
    {
        private static ModelsOfSuccessContainerControl _instance = null;
        private List<ModelOfSuccessControl> _successModelControls = new List<ModelOfSuccessControl>();

        public static ModelsOfSuccessContainerControl Instance
        {
            get
            { 
                if(_instance == null)
                    _instance = new ModelsOfSuccessContainerControl();
                return _instance;
            }
        }

        public static void RemoveInstance()
        {
            try
            {
                _instance.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }
        
        private ModelsOfSuccessContainerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void ModelsOfSuccessContainerControl_Load(object sender, EventArgs e)
        {
            _successModelControls.Clear();
            xtraScrollableControl.Controls.Clear();
            foreach (BusinessClasses.SuccessModel model in BusinessClasses.SuccessModelsManager.Instance.SuccessModels)
            {
                ModelOfSuccessControl successModelControl = new ModelOfSuccessControl(model);
                _successModelControls.Add(successModelControl);
                xtraScrollableControl.Controls.Add(successModelControl);
                successModelControl.BringToFront();
                successModelControl.UpdateHeight();
            }

        }

        public void UpdateSuccessModels()
        {
            foreach (ModelOfSuccessControl successModelControl in _successModelControls)
                successModelControl.UpdateHeight();
        }

        private void ModelsOfSuccessContainerControl_Resize(object sender, System.EventArgs e)
        {
            UpdateSuccessModels();
        }

        public void buttonItemSuccessModelsHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("mos");
        }
    }
}
