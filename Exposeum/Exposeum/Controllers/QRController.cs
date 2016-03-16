using System;

namespace Exposeum.Controllers
{
	public class QRController
	{
		private QRController _instance;

		public static QRController GetInstance(){
			if (_instance == null)
				_instance = new QRController ();
			return _instance;
		}

		public QRController ()
		{
		}
	}
}
