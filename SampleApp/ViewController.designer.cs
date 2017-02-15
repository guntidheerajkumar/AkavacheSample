// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SampleApp
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton BtnClear { get; set; }

		[Outlet]
		UIKit.UIButton BtnSaveLocalCache { get; set; }

		[Outlet]
		UIKit.UITableView TblCacheData { get; set; }

		[Outlet]
		UIKit.UITextField TxtEntryfield { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnClear != null) {
				BtnClear.Dispose ();
				BtnClear = null;
			}

			if (BtnSaveLocalCache != null) {
				BtnSaveLocalCache.Dispose ();
				BtnSaveLocalCache = null;
			}

			if (TxtEntryfield != null) {
				TxtEntryfield.Dispose ();
				TxtEntryfield = null;
			}

			if (TblCacheData != null) {
				TblCacheData.Dispose ();
				TblCacheData = null;
			}
		}
	}
}
