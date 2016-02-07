using System;
using System.Drawing;
using System.Drawing.Imaging;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class ProgramStrategyItem
	{
		private StrategySummaryContent _parent;

		public bool Enabled { get; set; }
		public string Name { get; set; }
		public string Station { get; set; }
		public string Description { get; set; }
		public decimal Order { get; set; }

		private ImageSource _logo;
		[JsonIgnore]
		public ImageSource Logo
		{
			get
			{
				if (!Enabled)
					return DisabledLogo;
				return _logo.ContainsData ? _logo : MediaMetaData.Instance.ListManager.DefaultStrategyLogo;
			}
			set
			{
				if (!Enabled) return;
				_logo = value;
				_disabledLogo = null;
			}
		}

		private ImageSource _disabledLogo;
		private ImageSource DisabledLogo
		{
			get
			{
				if (_disabledLogo != null) return _disabledLogo;

				var sourceLogo = (_logo.BigImage ?? MediaMetaData.Instance.ListManager.DefaultStrategyLogo.BigImage).Clone() as Image;
				if (sourceLogo == null) return null;
				var disabledImage = new Bitmap(sourceLogo);
				using (var gr = Graphics.FromImage(disabledImage))
				using (var attributes = new ImageAttributes())
				{
					var matrix = new ColorMatrix();
					matrix.Matrix33 = 0.4f;
					attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
					gr.Clear(Color.FromKnownColor(KnownColor.ButtonFace));
					gr.DrawImage(sourceLogo, new Rectangle(0, 0, disabledImage.Width, disabledImage.Height), 0, 0, disabledImage.Width, disabledImage.Height, GraphicsUnit.Pixel, attributes);
					_disabledLogo = ImageSource.FromImage(disabledImage);
					return _disabledLogo;
				}
			}
		}

		public bool IsDefaultLogo
		{
			get { return _logo == null; }
		}

		public string CompiledName
		{
			get { return _parent.ShowStation ? String.Format("{0}{2}({1})", Name, Station, Environment.NewLine) : Name; }
		}

		[JsonConstructor]
		private ProgramStrategyItem() { }

		public ProgramStrategyItem(StrategySummaryContent programStrategy)
		{
			_parent = programStrategy;
			_logo = new ImageSource();
		}

		public void Dispose()
		{
			if (_logo != null)
				_logo.Dispose();
			_parent = null;
		}
	}
}
