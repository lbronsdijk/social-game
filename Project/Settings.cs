using System;
using System.Diagnostics;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Project {

	public class Settings {

		public Settings () {

			var doc = XElement.Load("Content/config.xml");

			foreach (XElement element in doc.Elements()) {

				Debug.WriteLine(element.Name + ": " + element.Value);
			}
		}
	}
}