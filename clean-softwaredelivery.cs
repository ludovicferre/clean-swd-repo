using System;
using System.IO;

namespace Symantec.CWoC {
    class CheckPkgDelivery {
		public static void Main(string [] args) {
		
			int days = 60;
			
			if (args.Length == 1) {
				int _days = 0;
				if(int.TryParse(args[0], out _days))
					days = _days;
			}
			
			if (Math.Abs(days) < 30) {
				days = 30;
			}

			// Assume we are running from a Managed Delivery Folder
			string base_folder = "..\\";
			
			// Check delivery folder against base
			string [] swd_flist = Directory.GetDirectories(base_folder);
			
			DateTime time_thresh = DateTime.UtcNow.AddDays(-Math.Abs(days));
			foreach(string swd in swd_flist) {
				DirectoryInfo i = new DirectoryInfo(swd);
								
				if (i.LastWriteTimeUtc < time_thresh) {
					Console.WriteLine("del /s /f {0}\\cache", swd); //"{0} was last modified on {1} utc.", swd, i.LastWriteTimeUtc.ToString());
				}
			}
		}
	}
}
