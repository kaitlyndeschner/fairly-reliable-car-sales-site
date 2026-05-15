using System.Collections.Generic;
using FairlyReliableCarSalesSite.Models;

namespace FairlyReliableCarSalesSite.ViewModels
{
    public class NewsEventsViewModel
    {
        public List<News> News { get; set; } = new List<News>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
