using EO.WebBrowser;
using Vintasoft.Imaging;

namespace NewBizWiz.CommonGUI
{
	public static class LicenseHelper
	{
		public static void Register()
		{
			ImagingGlobalSettings.RegisterImaging("William Byrd",
				"billy@newlocaldirect.com",
				"IhdrSwyenp4CiNJnDzdnhXPg2TUsJa0Rykz4Gpmc11FMGFz5vEt0bFKIrMR6VziCH7YfvX8Ofnznh9z7WuhhZCFCp4mtvRBq1OJhX2M/1PS2GifwTp2aQlioVFHW7VhIgKdFerrBs6YDG6M15DKyiWqFJo4Ks6wyqEH5ANvmj5EI");
			Runtime.AddLicense("6qfv6MQV2ZututoQ9oPi7vL3wo3n+vjovHWm9/oS7Zrr+QMQvUaBwMAX6Jzc" +
							   "8gQQvUaBdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW9x9+yb6+2yPa1a6q1" +
							   "yd2xerOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unWouPw+gzsWbn9Aw+7" +
							   "aOPt9BDtrNzpz7iJWZeksefgpePzCOmMQ5ekscufWZekzQzjnZf4ChvkdpnJ" +
							   "4NnCoenz/hChWe3pAx7oqOXBs92taZmkwOmMQ5ekscu7aNjw/Rr2d4SOscuf" +
							   "WbPzAw/kq8Dy9xqfndj49uihbKa2wdqxaai4s8v1nun3+hrtdpm2s8uud4SO" +
							   "scufWbP3+hLtmuv5AxC9");
		}
	}
}
