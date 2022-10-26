using RedPoc.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPoc.Entity.Viewmodels
{
    public class OrderViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public OrderType OrderType { get; set; }
    }
}
