using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Dictionaries;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using Asa.Common.GUI.ListEditor;

namespace Asa.Common.GUI.Common
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

		public override string EditorTypeName => RepositoryItemComboBoxList.EditorName;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemComboBoxList Properties => base.Properties as RepositoryItemComboBoxList;
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

		public override string EditorTypeName => EditorName;

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
				if (OwnerEdit.Parent != null && OwnerEdit.Parent.InvokeRequired)
					OwnerEdit.Parent.Invoke(new MethodInvoker(() =>
					{
						Items.Clear();
						Items.AddRange(ListManager.Instance.Advertisers.Items);
					}));
				else
				{
					Items.Clear();
					Items.AddRange(ListManager.Instance.Advertisers.Items);
				}
			};
			ListManager.Instance.DecisionMakers.ListChanged += (o, e) =>
			{
				if (ListType != ListType.DecisionMakers) return;
				if (OwnerEdit.Parent != null && OwnerEdit.Parent.InvokeRequired)
					OwnerEdit.Parent.Invoke(new MethodInvoker(() =>
					{
						Items.Clear();
						Items.AddRange(ListManager.Instance.DecisionMakers.Items);
					}));
				else
				{
					Items.Clear();
					Items.AddRange(ListManager.Instance.DecisionMakers.Items);
				}
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