﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShowBot.Model;

namespace ShowBot.Services {
	public interface IDownloader {
		Download AddDownload(Show showToDownload);
		IEnumerable<Download> GetStatus();
		void PauseDownload(Download downloadToPause);
		void RemoveDownload(Download downloadToRemove);
		void AddTracker(Download download, string tracker);
	}
}
