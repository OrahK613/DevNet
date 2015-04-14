using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DevNet.Models
{
    public class RSSFeedModel
    {
        public static List<SyndicationItem> RssFeed { get; set; }
        public static string RssFeedName { get; set; }

        public List<SyndicationItem> GetRSSFeed(string strRecommendedRSSFeed)
        {
            List<SyndicationItem> lastNews = new List<SyndicationItem>();

            string url = "";

            switch (strRecommendedRSSFeed)
            {
                case "C/C++":
                    url = "http://www.drdobbs.com/rss/cpp";
                    break;
                case "Embedded Systems":
                    url = "http://www.drdobbs.com/rss/embedded-systems";
                    break;
                case "JVM":
                    url = "http://www.drdobbs.com/rss/jvm";
                    break;
                case "Mobile":
                    url = "http://www.drdobbs.com/rss/mobile";
                    break;
                case "Open Source":
                    url = "http://www.drdobbs.com/rss/open-source";
                    break;
                case "Parallel":
                    url = "http://www.drdobbs.com/rss/parallel";
                    break;
                case "Web Development":
                    url = "http://www.drdobbs.com/rss/web-development";
                    break;
                case "Windows/.NET":
                    url = "http://www.drdobbs.com/rss/windows";
                    break;
                default:
                    Console.WriteLine("Invalid RSS Feed.");
                    break;
            }

            lastNews.AddRange(this.GetItemsFromUrl(url));

            lastNews.Sort(delegate(SyndicationItem x, SyndicationItem y) { return y.PublishDate.CompareTo(x.PublishDate); });

            return lastNews;
        }

        private List<SyndicationItem> GetItemsFromUrl(string url)
        {
            List<SyndicationItem> list = new List<SyndicationItem>();
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (SyndicationItem item in feed.Items)
            {
                item.Copyright = (feed.Copyright == null) ? new TextSyndicationContent(feed.Generator) : new TextSyndicationContent(feed.Copyright.Text);
                item.Id = (item.Id == null) ? item.Links[0].Uri.ToString() : item.Id;
                list.Add(item);
            }

            return list;
        }
    }
}