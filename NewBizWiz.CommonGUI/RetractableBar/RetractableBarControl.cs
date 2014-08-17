using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NewBizWiz.CommonGUI.RetractableBar
{
	[Designer(typeof(RetractableBarDesigner))]
	public partial class RetractableBarControl : UserControl
	{
		private const int DefaultContentSize = 270;
		private const int DefaultAnimationDelay = 1500;

		public event EventHandler<StateChangedEventArgs> StateChanged;

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel Content
		{
			get { return pnContent; }
		}

		[Browsable(true), DefaultValue(DefaultContentSize), Category("Appearance")]
		public int ContentSize { get; set; }

		[Browsable(true), DefaultValue(1500), Category("Appearance")]
		public int AnimationDelay { get; set; }

		[Browsable(true), Category("Appearance")]
		
		public Image Logo
		{
			get { return pictureBoxImage.Image; }
			set { pictureBoxImage.Image = value; }
		}

		protected RetractableBarControl()
		{
			InitializeComponent();
			ContentSize = DefaultContentSize;
			AnimationDelay = DefaultAnimationDelay;
		}

		public void Collapse(bool silent = false)
		{
			if (silent || AnimationDelay == 0)
			{
				Width = pnClosed.Width;
				pnOpened.Visible = false;
				pnClosed.Visible = true;
			}
			else
			{
				var timer = new Timer();
				timer.Interval = AnimationDelay > ContentSize ? AnimationDelay / ContentSize : 100;
				timer.Tick += (o, e) =>
				{
					if (Width > (pnClosed.Width + 50))
						Width -= 50;
					else
					{
						Width = pnClosed.Width;
						pnOpened.Visible = false;
						pnClosed.Visible = true;
						timer.Stop();
						timer.Dispose();
						timer = null;
					}
					Application.DoEvents();
				};
				timer.Start();
			}
			if (StateChanged != null)
				StateChanged(this, new StateChangedEventArgs(false));
		}

		public void Expand(bool silent = false)
		{
			if (silent || AnimationDelay == 0)
			{
				Width = ContentSize;
				pnOpened.Visible = true;
				pnClosed.Visible = false;
			}
			else
			{
				var timer = new Timer();
				timer.Interval = AnimationDelay / ContentSize;
				timer.Tick += (o, e) =>
				{
					if (Width < (ContentSize - 50))
						Width += 50;
					else
					{
						Width = ContentSize;
						pnOpened.Visible = true;
						pnClosed.Visible = false;
						timer.Stop();
						timer.Dispose();
						timer = null;
					}
					Application.DoEvents();
				};
				timer.Start();
			}
			if (StateChanged != null)
				StateChanged(this, new StateChangedEventArgs(true));
		}

		private void simpleButtonExpand_Click(object sender, System.EventArgs e)
		{
			Expand();
		}

		private void simpleButtonCollapse_Click(object sender, EventArgs e)
		{
			Collapse();
		}
	}

	public class RetractableBarDesigner : ParentControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			if (Control is RetractableBarControl)
			{
				EnableDesignMode(((RetractableBarControl)Control).Content, "Content");
			}
		}
	}

	public class StateChangedEventArgs : EventArgs
	{
		public bool Expaned { get; private set; }

		public StateChangedEventArgs(bool expaned)
		{
			Expaned = expaned;
		}
	}
}