﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShowBot.Services;
using ShowBot.Model;
using Argotic.Syndication;
using log4net;

namespace ShowBot.DefaultServices {

	public class ShowRssNewShowProvider : INewShowsProvider {
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private readonly string feedUrl;
		private readonly int hoursOffset;

		public ShowRssNewShowProvider(IConfig config) {
			var settings = config.GetConfigurationSettings();
			feedUrl = settings.FeedUrl;
			hoursOffset = Convert.ToInt32(settings.HoursOffset);
		}

		public IEnumerable<Show> GetNewShowsSince(DateTime sinceDate) {

			RssFeed feed = RssFeed.Create(
				new Uri(feedUrl), 
				new Argotic.Common.SyndicationResourceLoadSettings {
					RetrievalLimit = 10,
					AutoDetectExtensions = true,
			});

			foreach (var item in feed.Channel.Items) {
				Log.DebugFormat("{0}: {1}", item.Title, item.PublicationDate);
			}

			var newShows = from item in feed.Channel.Items
						   where item.PublicationDate >= sinceDate.AddHours(-1 * hoursOffset)
						   select new Show { Title = item.Title, TorrentFile = item.Link.ToString() };

			return newShows;
		}
	}
}
