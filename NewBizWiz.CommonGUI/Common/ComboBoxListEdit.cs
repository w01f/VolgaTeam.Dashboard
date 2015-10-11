using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using NewBizWiz.CommonGUI.ListEditor;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.CommonGUI.Common
{
	public class ComboBoxListEdit : TabbedCombobox
	{
		static ComboBoxListEdit()
		{
			RepositoryItemComboBoxList.RegisterComboBoxList();
		}

		public ComboBoxListEdit()
		{
			Properties.CaseSensitiveSearch = true;
		}

		public override string EditorTypeName
		{
			get { return RepositoryItemComboBoxList.EditorName; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemComboBoxList Properties
		{
			get { return base.Properties as RepositoryItemComboBoxList; }
		}
	}

	[UserRepositoryItem("RegisterComboBoxList")]
	public class RepositoryItemComboBoxList : RepositoryItemComboBox
	{
		public const string EditorName = "ComboBoxListEdit";

		static RepositoryItemComboBoxList()
		{
			RegisterComboBoxList();
		}

		public static void RegisterComboBoxList()
		{
			EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(ComboBoxListEdit), typeof(RepositoryItemComboBoxList), typeof(ComboBoxViewInfo), new ButtonEditPainter(), true));
		}

		public override string EditorTypeName
		{
			get { return EditorName; }
		}

		private ListType _listType;
		[Browsable(true), Description("List Type"), Category("Misc")]
		public ListType ListType
		{
			get
			{
				return _listType;
			}
			set
			{
				_listType = value;
				switch (_listType)
				{
					case ListType.Advertisers:
						Items.Clear();
						Items.AddRange(ListManager.Instance.Advertisers.Items);
						break;
					case ListType.DecisionMakers:
						Items.Clear();
						Items.AddRange(ListManager.Instance.DecisionMakers.Items);
						break;
				}
			}
		}

		public RepositoryItemComboBoxList()
		{
			ButtonClick += RepositoryItemComboBoxList_ButtonClick;
			ListManager.Instance.Advertisers.ListChanged += (o, e) =>
			{
				if (ListType != ListType.Advertisers) return;
				//Items.Clear();
				//Items.AddRange(ListManager.Instance.Advertisers.Items);
			};
			ListManager.Instance.DecisionMakers.ListChanged += (o, e) =>
			{
				if (ListType != ListType.DecisionMakers) return;
				//Items.Clear();
				//Items.AddRange(ListManager.Instance.DecisionMakers.Items);
			};
		}

		private void RepositoryItemComboBoxList_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Kind)
			{
				case ButtonPredefines.Ellipsis:
					using (var form = new FormCommonListEditor())
					{
						form.ShowDialog();
					}
					break;
			}
		}

		public override void CreateDefaultButton()
		{
			Buttons.AddRange(new[]
			{
				new EditorButton(ButtonPredefines.Combo),
				new EditorButton(ButtonPredefines.Ellipsis)
			});
		}
	}

	public enum ListType
	{
		Advertisers,
		DecisionMakers
	}
}