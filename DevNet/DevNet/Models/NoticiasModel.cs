using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DevNet.Models
{
    public class NoticiasModel
    {
        public List<SyndicationItem> GetUltimasNoticias()
        {
            List<SyndicationItem> lastNews = new List<SyndicationItem>();
            lastNews.AddRange(this.GetItemsFromUrl("http://www.drdobbs.com/rss/cpp"));

            lastNews.Sort(delegate(SyndicationItem x, SyndicationItem y) { return y.PublishDate.CompareTo(x.PublishDate); });

            return lastNews;
        }

        private List<SyndicationItem> GetItemsFromUrl(string url)
        {
            List<SyndicationItem> lista = new List<SyndicationItem>();
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (SyndicationItem item in feed.Items)
            {
                item.Copyright = (feed.Copyright == null) ? new TextSyndicationContent(feed.Generator) : new TextSyndicationContent(feed.Copyright.Text);
                item.Id = (item.Id == null) ? item.Links[0].Uri.ToString() : item.Id;
                lista.Add(item);
            }

            return lista;
        }
    }
}
