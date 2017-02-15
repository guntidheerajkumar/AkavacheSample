//
// ViewController.cs
//
// Author: Dheeraj Kumar Gunti <guntidheerajkumar@gmail.com>
//
// Copyright (c) 2017 (c) Dheeraj Kumar Gunti
//
using System;
using System.Reactive.Linq;
using Akavache;
using Foundation;
using System.Text;
using System.Collections.Generic;
using UIKit;

namespace SampleApp
{
	public class CacheData
	{
		public string Key {
			get;
			set;
		}

		public string CacheValue {
			get;
			set;
		}

		public DateTimeOffset? CacheDate {
			get;
			set;
		}
	}

	public partial class ViewController : UIViewController
	{
		List<CacheData> lstCacheData = new List<CacheData>();

		protected ViewController(IntPtr handle) : base(handle)
		{

		}

		public async override void ViewDidLoad()
		{
			int i = 1;
			base.ViewDidLoad();
			BlobCache.ApplicationName = "SampleAkavanche";
			var a = await BlobCache.LocalMachine.GetAllKeys();
			foreach (var item in a) {
				var aa = await BlobCache.LocalMachine.GetObject<string>(item);
				var aaTime = await BlobCache.LocalMachine.GetCreatedAt(item);
				lstCacheData.Add(new CacheData() { Key = item, CacheValue = aa, CacheDate = aaTime });
			}

			BtnSaveLocalCache.TouchUpInside += async (sender, e) => {

				string key = "EntryValue" + i.ToString();
				await BlobCache.LocalMachine.InsertObject(key, TxtEntryfield.Text);
				TxtEntryfield.Text = string.Empty;
				var aa = await BlobCache.LocalMachine.GetObject<string>(key);
				var aaTime = await BlobCache.LocalMachine.GetCreatedAt(key);
				lstCacheData.Add(new CacheData() { Key = key, CacheValue = aa, CacheDate = aaTime });
				TblCacheData.ReloadData();
				i++;
			};

			BtnClear.TouchUpInside += async (sender, e) => {
				await BlobCache.LocalMachine.InvalidateAll();
				lstCacheData.Clear();
				TblCacheData.ReloadData();
			};

			TblCacheData.Source = new CacheDataTableSource(lstCacheData);
			TblCacheData.TableFooterView = new UIView();
			TblCacheData.ReloadData();
		}
	}

	public class CacheDataTableSource : UITableViewSource
	{

		List<CacheData> TableItems;
		string CellIdentifier = "TableCell";

		public CacheDataTableSource(List<CacheData> items)
		{
			TableItems = items;
		}
		
		public override string TitleForHeader(UITableView tableView, nint section)
		{
			if (TableItems.Count > 0)
				return $"{TableItems.Count} record(s) in Cache";
			else 
				return "No Data in the Cache";
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return TableItems.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			CacheData item = TableItems[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null) { cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier); }

			cell.TextLabel.Text = item.Key + " - " + item.CacheValue;
			cell.DetailTextLabel.Text = item.CacheDate.GetValueOrDefault(DateTimeOffset.Now).ToString();

			return cell;
		}
	}
}
